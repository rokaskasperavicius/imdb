using IMDB_API.Application.DTOs;

namespace IMDB_API.Application.Services;

public interface IRatingsService
{
    Task Rate(int userId, string tconst, int rating);
    Task<List<RatingDto>> GetRatings(int userId);
}