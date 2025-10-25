using IMDB_API.Application.DTOs;

namespace IMDB_API.Application.Services;

public interface ICastService
{
    Task<List<CastDto>> GetCast(string tconst);
}