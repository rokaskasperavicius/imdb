using IMDB_API.Domain;

namespace IMDB_API.Application.Interfaces;

public interface IBookmarksRepository
{
    void CreateBookmark(int userId, string tconst);
    void DeleteBookmark(int userId, string tconst);
    IQueryable<Bookmark> GetBookmarks(int userId);
}