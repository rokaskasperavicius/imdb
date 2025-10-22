using IMDB_API.Domain;

namespace IMDB_API.Application.Interfaces;

public interface IUserTokenService
{
    string CreateToken(User user);
}