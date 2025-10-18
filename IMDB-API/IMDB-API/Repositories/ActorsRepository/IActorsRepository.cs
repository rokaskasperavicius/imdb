using IMDB_API.DTOs;

namespace IMDB_API.Repositories;

public interface IActorsRepository
{
    Task<int> GetCountAsync();

    Task<List<ActorsDto>> GetActorsAsync(int pageSize, int skip);
    Task<ActorsDto?> GetActorAsync(string nconst);
}