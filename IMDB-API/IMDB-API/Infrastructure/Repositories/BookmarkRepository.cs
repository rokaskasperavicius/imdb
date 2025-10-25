using IMDB_API.Application.Interfaces;
using IMDB_API.Domain;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace IMDB_API.Infrastructure.Repositories;

public class BookmarkRepository : IBookmarksRepository
{
    private readonly ImdbDbContext _imdbDbContext;

    public BookmarkRepository(ImdbDbContext imdbDbContext)
    {
        _imdbDbContext = imdbDbContext;
    }

    public async Task<List<Bookmark>> GetBookmarks(int userId)
    {
        var bookmarks = await _imdbDbContext.UserTitleBookmarks
            .AsNoTracking()
            .Where(b => b.UserId == userId)
            .Select(utb => new Bookmark
            {
                UserId = userId
            }).ToListAsync();

        return bookmarks;
    }

    public void CreateBookmark(Bookmark bookmark)
    {
        var user = new NpgsqlParameter("user", bookmark.UserId);
        var title = new NpgsqlParameter("title", bookmark.TitleId);

        _imdbDbContext.Database.ExecuteSqlRaw(
            "CALL p_bookmark_title({0}, {1})",
            user,
            title
        );
    }

    public void DeleteBookmark(Bookmark bookmark)
    {
        var user = new NpgsqlParameter("user", bookmark.UserId);
        var title = new NpgsqlParameter("title", bookmark.TitleId);

        _imdbDbContext.Database.ExecuteSqlRaw(
            "CALL p_remove_bookmark_title({0}, {1})",
            user,
            title
        );
    }
}