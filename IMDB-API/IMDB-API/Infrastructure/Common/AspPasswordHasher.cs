using IMDB_API.Application.Interfaces;
using IMDB_API.Domain;
using Microsoft.AspNetCore.Identity;

namespace IMDB_API.Infrastructure.Repositories;

public class AspPasswordHasher : IPasswordHasher
{
    public string HashPassword(User user, string password)
    {
        var passwordHasher = new PasswordHasher<User>();
        var hash = passwordHasher.HashPassword(user, password);

        return hash;
    }

    public void VerifyHashedPassword(
        User user,
        string password)
    {
        var passwordHasher = new PasswordHasher<User>();

        var hashResult = passwordHasher.VerifyHashedPassword(
            user,
            user.PasswordHash,
            password);

        // Check hash
        if (hashResult == PasswordVerificationResult.Failed)
            throw new UnauthorizedAccessException();
    }
}