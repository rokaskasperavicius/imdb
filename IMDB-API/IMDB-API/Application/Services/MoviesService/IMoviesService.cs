using IMDB_API.Application.Common;
using IMDB_API.Application.DTOs;

namespace IMDB_API.Application.Services;

public interface IMoviesService
{
    Task<MovieDto?> GetMovie(string tconst);

    Task<PagedResults<List<MovieDto>>> GetMovies(
        int page,
        int pageSize);

    Task<List<MovieDto>> GetRelatedMovies(string tconst);
}