using IMDB_API.Domain;

namespace IMDB_API.Application.Interfaces;

public interface IBookmarksRepository
{
    void CreateBookmark(Bookmark bookmark);
    void DeleteBookmark(Bookmark bookmark);
    Task<List<Bookmark>> GetBookmarks(int userId);
}