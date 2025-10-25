namespace IMDB_API.Application.Interfaces;

public interface IPasswordHasher
{
    string HashPassword(string email, string password);

    void VerifyHashedPassword(string email, string passwordHash,
        string password);
}