using IMDB_API.Application.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace IMDB_API.Infrastructure.Repositories;

public class AspPasswordHasher : IPasswordHasher
{
    public string HashPassword(string email, string password)
    {
        var passwordHasher = new PasswordHasher<string>();
        var hash = passwordHasher.HashPassword(email, password);

        return hash;
    }

    public void VerifyHashedPassword(
        string email,
        string passwordHash,
        string password)
    {
        var passwordHasher = new PasswordHasher<string>();

        var hashResult = passwordHasher.VerifyHashedPassword(
            email,
            passwordHash,
            password);

        // Check hash
        if (hashResult == PasswordVerificationResult.Failed)
            throw new UnauthorizedAccessException();
    }
}