using IMDB_API.Contracts.DTOs;
using IMDB_API.Contracts.Response;

namespace IMDB_API.Application.Services;

public interface IActorsService
{
    Task<PagedResults<List<ActorDTO>>> GetActors(int page, int pageSize);
    Task<ActorDTO?> GetActor(string nconst);
}