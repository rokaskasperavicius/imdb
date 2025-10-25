using IMDB_API.Application.DTOs;

namespace IMDB_API.Application.Services;

public interface IUsersService
{
    UserDto RegisterUser(string name, string email, string password);
    UserWithTokenDto LoginUser(string email, string password);
}