using IMDB_API.Application.Interfaces;
using IMDB_API.Models;
using Microsoft.EntityFrameworkCore;

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
        _imdbDbContext.Database.ExecuteSql(
            $"CALL p_create_user({user.Name}, {user.Email}, {user.PasswordHash})");

        var createdUser =
            _imdbDbContext.Users.Single(u => u.Email == user.Email);

        return createdUser;
    }

    public User GetUserByEmail(string email)
    {
        return _imdbDbContext.Users.Single(u => u.Email == email);
    }
}