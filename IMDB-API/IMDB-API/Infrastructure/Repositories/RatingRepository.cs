using System.Linq.Expressions;
using IMDB_API.Application.Interfaces;
using IMDB_API.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using Rating = IMDB_API.Domain.Rating;

namespace IMDB_API.Infrastructure.Repositories;

public class RatingRepository : IRatingRepository
{
    private static readonly Expression<Func<UserTitleRating, Rating>>
        RatingProjection =
            n => new Rating
            {
                UserId = n.UserId,
                TitleId = n.BasicTconst,
                TitleRating = n.Rating,
                CreatedAt = n.CreatedAt
            };

    private readonly ImdbDbContext _imdbDbContext;

    public RatingRepository(ImdbDbContext imdbDbContext)
    {
        _imdbDbContext = imdbDbContext;
    }

    public async Task<List<Rating>> GetRatings(int userId)
    {
        var ratings = await _imdbDbContext.UserTitleRatings
            .AsNoTracking()
            .Where(utr => utr.UserId == userId)
            .Select(RatingProjection)
            .ToListAsync();

        return ratings;
    }

    public void RateTitle(Rating rating)
    {
        var user = new NpgsqlParameter("user", rating.UserId);
        var title = new NpgsqlParameter("title", rating.TitleId);
        var score = new NpgsqlParameter("score", rating.TitleRating);

        _imdbDbContext.Database.ExecuteSqlRaw(
            "CALL p_rate({0}, {1}, {2})",
            user,
            title,
            score
        );
    }
}