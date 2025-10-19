namespace IMDB_API.Application.Services;

public interface IBookmarksService
{
    void CreateBookmark(int userId, string tconst);
    void DeleteBookmark(int userId, string tconst);
}