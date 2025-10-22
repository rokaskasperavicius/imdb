using IMDB_API.Domain;

namespace IMDB_API.Application.Interfaces;

public interface IPasswordHasher
{
    string HashPassword(User user, string password);

    void VerifyHashedPassword(User user, string password);
}