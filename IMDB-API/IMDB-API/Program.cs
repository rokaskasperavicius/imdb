using System.ComponentModel.DataAnnotations;
using IMDB_API.Context;
using IMDB_API.DTOs;
using IMDB_API.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<MyDbContext>(opt =>
    opt.UseNpgsql(
        builder.Configuration.GetConnectionString("MyDbContext")));

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.MapGet("/api/movies", async (MyDbContext db) =>
{
    return await db.Basics.Take(50).ToListAsync();
});

app.MapPost("/api/users/register", async (RegisterUser user, MyDbContext db) =>
{
    var isEmailValid = new EmailAddressAttribute().IsValid(user.Email);

    if (!isEmailValid || user.Name == null || user.Password == null)
    {
        return Results.BadRequest();
    }
    
    var newUser = new User();
    var hasher = new PasswordHasher<RegisterUser>();
    
    newUser.Name = user.Name;
    newUser.Email = user.Email;
    newUser.PasswordHash = hasher.HashPassword(user, user.Password);
    
    db.Users.Add(newUser);
    
    await db.SaveChangesAsync();
    return Results.Created();
});

app.Run();