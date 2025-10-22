using System.Text;
using IMDB_API.Application.Interfaces;
using IMDB_API.Application.Services;
using IMDB_API.Infrastructure;
using IMDB_API.Infrastructure.Repositories;
using IMDB_API.Web;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);
var jwt = builder.Configuration["JwtSecretKey"];

builder.Services.AddDbContext<ImdbDbContext>(opt =>
    opt.UseNpgsql(
        builder.Configuration.GetConnectionString("ImdbDbContext")));

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
            IssuerSigningKey =
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes($"{jwt}"))
        };
    });
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<ICurrentUser, CurrentUser>();

// Repositories
builder.Services.AddScoped<IPeopleRepository, PeopleRepository>();
builder.Services.AddScoped<IUsersRepository, UsersRepository>();
builder.Services.AddScoped<IBookmarksRepository, BookmarkRepository>();

// Services
builder.Services.AddScoped<IPeopleService, PeopleService>();
builder.Services.AddScoped<IUsersService, UsersService>();
builder.Services.AddScoped<IBookmarksService, BookmarksService>();

// Common
builder.Services.AddScoped<IUserTokenService, UserTokenService>();
builder.Services.AddScoped<IPasswordHasher, AspPasswordHasher>();


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

// app.MapDelete("/api/bookmarks/{tconst}",
//     [Authorize](HttpRequest request, ICurrentUser me, ImdbDbContext db) =>
//     {
//         var tconst = request.RouteValues["tconst"];
//
//         db.Database.ExecuteSql(
//             $"CALL p_remove_bookmark_title({me.Id}, {tconst})");
//
//         return Results.NoContent();
//     });

app.Run();