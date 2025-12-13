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

    public async Task Rate(int userId, string tconst, int rating)
    {
        var userRating = new Rating
        {
            UserId = userId,
            TitleId = tconst,
            TitleRating = rating
        };

        var ratings = await _ratingRepository.GetRatings(userId);

        if (ratings.Exists(r => r.TitleId.Trim() == tconst.Trim()))
            throw new InvalidOperationException(
                $"Rating on {tconst} already exists.");

        try
        {
            _ratingRepository.RateTitle(userRating);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException(
                $"Could not rate {tconst}.", ex);
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
            CreatedAt = rating.CreatedAt,
            Rating = rating.TitleRating
        };
    }
}