using IMDB_API.Application.DTOs;
using IMDB_API.Application.Interfaces;
using IMDB_API.Domain;

namespace IMDB_API.Application.Services;

public class BookmarksService : IBookmarksService
{
    private readonly IBookmarksRepository _bookmarksRepository;

    public BookmarksService(IBookmarksRepository bookmarksRepository)
    {
        _bookmarksRepository = bookmarksRepository;
    }

    public void CreateBookmark(int userId, string tconst)
    {
        try
        {
            var bookmark = new Bookmark
            {
                UserId = userId,
                TitleId = tconst
            };

            _bookmarksRepository.CreateBookmark(bookmark);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("Bookmark already exists", ex);
        }
    }

    public void DeleteBookmark(int userId, string tconst)
    {
        var bookmark = new Bookmark
        {
            UserId = userId,
            TitleId = tconst
        };

        _bookmarksRepository.DeleteBookmark(bookmark);
    }

    public async Task<List<BookmarkDto>> GetBookmarks(int userId)
    {
        var bookmarks = await _bookmarksRepository.GetBookmarks(userId);
        var mapped =
            bookmarks.Select(b => new BookmarkDto
            {
                Id = b.TitleId.Trim()
            }).ToList();

        return mapped;
    }
}