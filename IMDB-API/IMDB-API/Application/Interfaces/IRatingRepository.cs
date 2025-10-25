using IMDB_API.Domain;

namespace IMDB_API.Application.Interfaces;

public interface IRatingRepository
{
    void RateTitle(Rating rating);
    Task<List<Rating>> GetRatings(int userId);
}