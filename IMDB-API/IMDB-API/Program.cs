using System.Text;
using IMDB_API.Application.Interfaces;
using IMDB_API.Application.Services;
using IMDB_API.Infrastructure;
using IMDB_API.Infrastructure.Common;
using IMDB_API.Infrastructure.Repositories;
using IMDB_API.Web.Common;
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
builder.Services.AddScoped<IMoviesRepository, MoviesRepository>();
builder.Services.AddScoped<IRatingRepository, RatingRepository>();
builder.Services.AddScoped<ICastRepository, CastRepository>();

// Services
builder.Services.AddScoped<IPeopleService, PeopleService>();
builder.Services.AddScoped<IUsersService, UsersService>();
builder.Services.AddScoped<IBookmarksService, BookmarksService>();
builder.Services.AddScoped<IMoviesService, MoviesService>();
builder.Services.AddScoped<IRatingsService, RatingsService>();
builder.Services.AddScoped<ICastService, CastService>();
builder.Services.AddScoped<ISearchService, SearchService>();

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

app.Run();

// For UI Tests
public partial class Program
{
}