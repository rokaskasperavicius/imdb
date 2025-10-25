using IMDB_API.Application.Interfaces;
using IMDB_API.Domain;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace IMDB_API.Infrastructure.Repositories;

public class UsersRepository : IUsersRepository
{
    private readonly ImdbDbContext _imdbDbContext;

    public UsersRepository(ImdbDbContext imdbDbContext)
    {
        _imdbDbContext = imdbDbContext;
    }

    public User CreateUser(User user)
    {
        var name = new NpgsqlParameter("user", user.Name);
        var email = new NpgsqlParameter("email", user.Email);
        var hash = new NpgsqlParameter("hash", user.PasswordHash);

        _imdbDbContext.Database.ExecuteSqlRaw(
            "CALL p_create_user({0}, {1}, {2})",
            name,
            email,
            hash
        );

        var createdUser =
            _imdbDbContext.Users.Single(u => u.Email == user.Email);

        user.Id = createdUser.Id;

        return user;
    }

    public User GetUserByEmail(string email)
    {
        var user = _imdbDbContext.Users.Single(u => u.Email == email);

        return new User
        {
            Id = user.Id,
            Email = user.Email,
            Name = user.Name,
            PasswordHash = user.PasswordHash
        };
    }
}