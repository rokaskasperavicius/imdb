using IMDB_API.Application.DTOs;
using IMDB_API.Application.Interfaces;
using Microsoft.EntityFrameworkCore;

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
            _bookmarksRepository.CreateBookmark(userId, tconst);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("Bookmark already exists", ex);
        }
    }

    public void DeleteBookmark(int userId, string tconst)
    {
        _bookmarksRepository.DeleteBookmark(userId, tconst);
    }

    public async Task<List<BookmarkDTO>> GetBookmarks(int userId)
    {
        var bookmarks = await _bookmarksRepository
            .GetBookmarks(userId)
            .Select(b => new BookmarkDTO
            {
                Id = b.Id.Trim()
            }).ToListAsync();

        return bookmarks;
    }
}