using IMDB_API.Contracts.DTOs;

namespace IMDB_API.Application.Services;

public interface IUsersService
{
    UserDTO RegisterUser(string name, string email, string password);
    UserWithTokenDTO LoginUser(string email, string password);
}