using IMDB_API.Application.DTOs;
using IMDB_API.Application.Interfaces;
using IMDB_API.Domain;

namespace IMDB_API.Application.Services;

public class RatingsService : IRatingsService
{
    private readonly IRatingRepository _ratingRepository;

    public RatingsService(IRatingRepository ratingRepository)
    {
        _ratingRepository = ratingRepository;
    }

    public void Rate(int userId, string tconst, int rating)
    {
        var userRating = new Rating
        {
            UserId = userId,
            TitleId = tconst,
            TitleRating = rating
        };

        try
        {
            _ratingRepository.RateTitle(userRating);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException($"Could not rate {tconst}", ex);
        }
    }

    public async Task<List<RatingDto>> GetRatings(int userId)
    {
        var ratings = await _ratingRepository.GetRatings(userId);

        return ratings.Select(RatingToDto).ToList();
    }

    private static RatingDto RatingToDto(Rating rating)
    {
        return new RatingDto
        {
            Id = rating.TitleId.Trim(),
            Rating = rating.TitleRating ?? 0
        };
    }
}