using System.Linq.Expressions;
using IMDB_API.Application.Interfaces;
using IMDB_API.Domain;
using IMDB_API.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace IMDB_API.Infrastructure.Repositories;

public class MoviesRepository : IMoviesRepository
{
    private static readonly Expression<Func<Basic, Movie>> MovieProjection =
        b => new Movie
        {
            Id = b.Tconst,
            Title = b.Primarytitle,
            IsAdult = b.Isadult,
            Year = b.Startyear,
            Plot = b.Plot,
            Poster = b.Poster,
            RunTimeInMinutes = b.Runtimeminutes,
            AverageRating = b.Rating != null ? b.Rating.Averagerating : 0,
            NumberOfVotes = b.Rating != null ? b.Rating.Numvotes : 0,
            Genres = b.Genres.Select(g => new MovieGenre { Name = g.Name })
                .ToList()
        };

    private readonly ImdbDbContext _imdbDbContext;

    public MoviesRepository(ImdbDbContext imdbDbContext)
    {
        _imdbDbContext = imdbDbContext;
    }

    public async Task<int> GetCount()
    {
        return await _imdbDbContext.Basics
            .AsNoTracking()
            .Where(b => b.Titletype == "movie")
            .CountAsync();
    }

    public async Task<Movie?> GetMovie(string tconst)
    {
        var movie = await _imdbDbContext.Basics
            .Where(b => b.Titletype == "movie")
            .Where(b => b.Tconst == tconst)
            .Select(MovieProjection)
            .FirstOrDefaultAsync();

        return movie;
    }

    public async Task<List<Movie>> GetMovies(int skip, int take)
    {
        var movies = await _imdbDbContext.Basics
            .AsNoTracking()
            .Where(b => b.Titletype == "movie")
            .OrderByDescending(b =>
                b.Rating != null
                    // Simple formula to include both columns in the calculation
                    ? b.Rating.Averagerating *
                      (decimal)Math.Log10(b.Rating.Numvotes + 1)
                    : 0)
            .ThenBy(b => b.Primarytitle)
            .Skip(skip)
            .Take(take)
            .Select(MovieProjection)
            .ToListAsync();

        return movies;
    }

    public async Task<List<Movie>> GetRelatedMovies(string tconst)
    {
        var movie = new NpgsqlParameter("movie", tconst);
        var rows = await _imdbDbContext.Database
            .SqlQueryRaw<SimilarMoviesRow>(
                "select * from f_similar_movies({0}, 'movie')", movie)
            .ToListAsync();

        // Needed to maintain sorting
        var freqDic = rows
            .Select(r => new { r.Tconst, r.GenresCount })
            .ToDictionary(r => r.Tconst, r => r.GenresCount);

        var movies = await _imdbDbContext.Basics
            .AsNoTracking()
            .Where(b => b.Titletype == "movie")
            .Where(b => freqDic.Keys.Contains(b.Tconst))
            .Select(MovieProjection)
            .ToListAsync();

        return movies
            .OrderByDescending(m => freqDic[m.Id])
            .ThenBy(m => m.Title)
            .ToList();
    }

    public async Task<List<Movie>> GetMoviesBySearch(int userId, string search)
    {
        //PARAMETERS
        var rows = await _imdbDbContext.Database
            .SqlQueryRaw<NameSearchRow>(
                "select * from f_string_search({0}, {1})", userId, search)
            .ToListAsync();

        return new List<Movie>();
    }
}