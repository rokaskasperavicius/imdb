using IMDB_API.Application.Interfaces;
using IMDB_API.Domain;
using Microsoft.EntityFrameworkCore;

namespace IMDB_API.Infrastructure.Repositories;

public class BookmarkRepository : IBookmarksRepository
{
    private readonly ImdbDbContext _imdbDbContext;

    public BookmarkRepository(ImdbDbContext imdbDbContext)
    {
        _imdbDbContext = imdbDbContext;
    }

    public void CreateBookmark(int userId, string tconst)
    {
        _imdbDbContext.Database.ExecuteSql(
            $"CALL p_bookmark_title({userId}, {tconst})");
    }

    public void DeleteBookmark(int userId, string tconst)
    {
        _imdbDbContext.Database.ExecuteSql(
            $"CALL p_remove_bookmark_title({userId}, {tconst})");
    }

    public async Task<List<Bookmark>> GetBookmarks(int userId)
    {
        var bookmarks = await _imdbDbContext.UserTitleBookmarks
            .AsNoTracking()
            .Where(b => b.UserId == userId)
            .Select(utb => new Bookmark
            {
                Id = utb.BasicTconst
            }).ToListAsync();

        return bookmarks;
    }
}