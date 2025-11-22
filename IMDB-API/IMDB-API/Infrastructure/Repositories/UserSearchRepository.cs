using IMDB_API.Application.Interfaces;
using IMDB_API.Domain;
using Microsoft.EntityFrameworkCore;

namespace IMDB_API.Infrastructure.Repositories;

public class UserSearchRepository : IUserSearchRepository
{
    private readonly ImdbDbContext _imdbDbContext;

    public UserSearchRepository(ImdbDbContext imdbDbContext)
    {
        _imdbDbContext = imdbDbContext;
    }

    public async Task<List<UserSearch>> GetUserSearches(int userId)
    {
        var userSearches = await _imdbDbContext.UserSearchHistories
            .AsNoTracking()
            .Where(b => b.UserId == userId)
            .Select(ush => new UserSearch
            {
                UserId = userId,
                Query = ush.SearchQuery,
                CreatedAt = ush.CreatedAt
            }).ToListAsync();

        return userSearches;
    }
}