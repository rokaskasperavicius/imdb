using IMDB_API.Application.DTOs;

namespace IMDB_API.Application.Services;

public interface ISearchService
{
    Task<List<SearchDto>> GetUserSearches(int userId);
}