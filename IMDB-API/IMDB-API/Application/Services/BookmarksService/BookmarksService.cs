using IMDB_API.Application.Interfaces;

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
}