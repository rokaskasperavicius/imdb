using IMDB_API.Models;

namespace IMDB_API.Application.Interfaces;

public interface IUsersRepository
{
    User CreateUser(User user);
    User GetUserByEmail(string email);
}