using IMDB_API.Application.DTOs;
using IMDB_API.Application.Interfaces;
using IMDB_API.Domain;

namespace IMDB_API.Application.Services;

public class CastService : ICastService
{
    private readonly ICastRepository _castRepository;

    public CastService(ICastRepository castRepository)
    {
        _castRepository = castRepository;
    }

    public async Task<List<CastDto>> GetCast(string tconst)
    {
        var cast = await _castRepository.GetCast(tconst);

        return cast.Select(CastToDto).ToList();
    }

    private static CastDto CastToDto(Cast cast)
    {
        return new CastDto
        {
            Ordering = cast.Ordering,
            PersonId = cast.PersonId?.Trim(),
            Category = cast.Category,
            Character = cast.Character,
            Job = cast.Job
        };
    }
}