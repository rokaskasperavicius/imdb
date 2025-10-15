using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using IMDB_API.Context;
using IMDB_API.DTOs;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);
var jwt = builder.Configuration["JwtSecretKey"];

builder.Services.AddDbContext<MyDbContext>(opt =>
    opt.UseNpgsql(
        builder.Configuration.GetConnectionString("MyDbContext")));

builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(jwtOptions =>
    {
        jwtOptions.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = "imdb",
            ValidAudience = "imdb",
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes($"{jwt}"))
        };
    });
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<ICurrentUser, CurrentUser>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/openapi/v1.json", "v1");
    });
}

app.UseHttpsRedirection();

app.MapControllers();
app.MapGet("/api/movies", async (MyDbContext db) =>
{
    return await db.Basics.Take(50).ToListAsync();
});

app.MapPost("/api/bookmarks/{tconst}",
    [Authorize] (HttpRequest request, ICurrentUser me, MyDbContext db) => 
    { 
        var tconst = request.RouteValues["tconst"];
        
        db.Database.ExecuteSql($"CALL p_bookmark_title({me.Id}, {tconst})");
        
        return Results.Created($"/bookmarks/{tconst}", new { tconst }); 
    });

app.MapDelete("/api/bookmarks/{tconst}",
    [Authorize] (HttpRequest request, ICurrentUser me, MyDbContext db) =>
    {
        var tconst = request.RouteValues["tconst"];
    
        db.Database.ExecuteSql($"CALL p_remove_bookmark_title({me.Id}, {tconst})");
    
        return Results.NoContent();
    });

app.MapPost("/api/users/register", (RegisterUser user, MyDbContext db) =>
{
    if (user.Email == null || user.Name == null || user.Password == null)
    {
        return Results.BadRequest();
    }
    
    var isEmailValid = new EmailAddressAttribute().IsValid(user.Email);

    if (!isEmailValid)
    {
        return Results.BadRequest();
    }
    
    var hasher = new PasswordHasher<RegisterUser>();
    var hash = hasher.HashPassword(user, user.Password);

    try
    {
        db.Database.ExecuteSql($"CALL p_create_user({user.Name}, {user.Email}, {hash})");
    }
    catch
    {
        return Results.Conflict();
    }

    // Email is unique
    var createdUser = db.Users.Single(u => u.Email == user.Email);
    
    return Results.Created($"users/{createdUser.Id}",
        new { createdUser.Id, createdUser.Email, createdUser.Name });
});

app.MapPost("/api/users/login", (LoginUser user, MyDbContext db) =>
{
    var dbUser = db.Users.SingleOrDefault(u => u.Email == user.Email);

    if (dbUser == null)
    {
        return Results.Unauthorized();
    }

    var hasher = new PasswordHasher<LoginUser>();
    var result = hasher.VerifyHashedPassword(user, dbUser.PasswordHash, user.Password);

    if (result == PasswordVerificationResult.Failed)
    {
        return Results.Unauthorized();
    }

    var claims = new List<Claim>
    {
        new (ClaimTypes.NameIdentifier, dbUser.Id.ToString())
    };

    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes($"{jwt}"));
    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

    var token = new JwtSecurityToken(
        issuer: "imdb",
        audience: "imdb",
        claims: claims,
        expires: DateTime.UtcNow.AddMinutes(30),
        signingCredentials: creds);

    var access = new JwtSecurityTokenHandler().WriteToken(token);

    return Results.Ok(new { AccessToken = access });
});

app.Run();