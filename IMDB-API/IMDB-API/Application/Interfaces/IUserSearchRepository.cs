using IMDB_API.Domain;

namespace IMDB_API.Application.Interfaces;

public interface IUserSearchRepository
{
    Task<List<UserSearch>> GetUserSearches(int userId);
}