using IMDB_API.Application.DTOs;

namespace IMDB_API.Application.Services;

public interface IBookmarksService
{
    void CreateBookmark(int userId, string tconst);
    void DeleteBookmark(int userId, string tconst);
    Task<List<BookmarkDto>> GetBookmarks(int userId);
}