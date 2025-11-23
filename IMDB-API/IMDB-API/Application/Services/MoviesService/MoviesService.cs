using IMDB_API.Application.Common;
using IMDB_API.Application.DTOs;
using IMDB_API.Application.Interfaces;
using IMDB_API.Domain;

namespace IMDB_API.Application.Services;

public class MoviesService : IMoviesService
{
    private readonly IMoviesRepository _moviesRepository;

    public MoviesService(IMoviesRepository moviesRepository)
    {
        _moviesRepository = moviesRepository;
    }

    public async Task<MovieDto?> GetMovie(string tconst)
    {
        var movie = await _moviesRepository.GetMovie(tconst);

        return movie != null ? MovieToDto(movie) : null;
    }

    public async Task<PagedResults<List<MovieDto>>> GetMovies(
        int page,
        int pageSize)
    {
        var skip = (page - 1) * pageSize;

        var movieCount = await _moviesRepository.GetCount();
        var movies = await _moviesRepository
            .GetMovies(skip, pageSize);

        var mapped = movies.Select(MovieToDto).ToList();

        return new PagedResults<List<MovieDto>>
        {
            Data = mapped,
            Page = page,
            PageSize = pageSize,
            TotalCount = movieCount
        };
    }

    public async Task<List<MovieDto>> GetRelatedMovies(string tconst)
    {
        var movies = await _moviesRepository.GetRelatedMovies(tconst);

        return movies.Select(MovieToDto).ToList();
    }

    public async Task<List<MovieDto>> GetMoviesBatch(List<string> ids)
    {
        var movies = await _moviesRepository.GetMoviesByIds(ids);

        return movies.Select(MovieToDto).ToList();
    }

    public async Task<List<MovieDto>> GetMoviesBySearch(int userId,
        string searchQuery)
    {
        var movies =
            await _moviesRepository.GetMoviesBySearch(userId, searchQuery);

        return movies.Select(MovieToDto).ToList();
    }

    private static MovieDto MovieToDto(Movie movie)
    {
        return new MovieDto
        {
            Id = movie.Id.Trim(),
            Title = movie.Title,
            IsAdult = movie.IsAdult,
            Year = movie.Year,
            Plot = movie.Plot,
            Poster = movie.Poster,
            RunTimeInMinutes = movie.RunTimeInMinutes,
            AverageRating = movie.AverageRating,
            NumberOfVotes = movie.NumberOfVotes,
            Genres = movie.Genres.Select(g => g.Name).ToList()
        };
    }
}