using IMDB_API.Models;

namespace IMDB_API.Application.Interfaces;

public interface IActorsRepository
{
    Task<int> GetCount();

    IQueryable<Name> GetActors();
    Task<Name?> GetActor(string nconst);
}