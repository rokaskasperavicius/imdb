using IMDB_API.Domain;

namespace IMDB_API.Application.Interfaces;

public interface IMoviesRepository
{
    Task<int> GetCount();
    Task<Movie?> GetMovie(string tconst);
    Task<List<Movie>> GetMovies(int skip, int take);

    Task<List<Movie>> GetRelatedMovies(string tconst);
}