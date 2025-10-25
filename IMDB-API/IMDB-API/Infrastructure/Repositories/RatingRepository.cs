using IMDB_API.Application.Interfaces;
using IMDB_API.Domain;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace IMDB_API.Infrastructure.Repositories;

public class RatingRepository : IRatingRepository
{
    private readonly ImdbDbContext _imdbDbContext;

    public RatingRepository(ImdbDbContext imdbDbContext)
    {
        _imdbDbContext = imdbDbContext;
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