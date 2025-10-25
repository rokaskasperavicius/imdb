using IMDB_API.Application.DTOs;
using IMDB_API.Application.Interfaces;

namespace IMDB_API.Application.Services;

public class SearchService : ISearchService
{
    private readonly IMoviesRepository _moviesRepository;

    public SearchService(IMoviesRepository moviesRepository)
    {
        _moviesRepository = moviesRepository;
    }

    public async Task<SearchDto> GetSearchResults(int userId,
        string searchQuery)
    {
        var movies =
            await _moviesRepository.GetMoviesBySearch(userId, searchQuery);

        return new SearchDto
        {
            Movies = movies.Select(movie => new MovieDto
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
            }).ToList()
        };
    }
}