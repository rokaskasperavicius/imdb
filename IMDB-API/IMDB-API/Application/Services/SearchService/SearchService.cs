using IMDB_API.Application.DTOs;
using IMDB_API.Application.Interfaces;

namespace IMDB_API.Application.Services;

public class SearchService : ISearchService
{
    private readonly IUserSearchRepository _userSearchRepository;

    public SearchService(IUserSearchRepository userSearchRepository)
    {
        _userSearchRepository = userSearchRepository;
    }

    public async Task<List<SearchDto>> GetUserSearches(int userId)
    {
        var userSearches =
            await _userSearchRepository.GetUserSearches(userId);

        var mapped =
            userSearches.Select(us => new SearchDto
            {
                Query = us.Query,
                CreatedAt = us.CreatedAt
            }).ToList();

        return mapped;
    }
}