using IMDB_API.Domain;

namespace IMDB_API.Application.Interfaces;

public interface ICastRepository
{
    Task<List<Cast>> GetCast(string tconst);
}