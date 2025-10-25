namespace IMDB_API.Application.Services;

public interface IRatingsService
{
    void Rate(int userId, string tconst, int rating);
}