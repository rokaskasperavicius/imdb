using IMDB_API.Application.Interfaces;
using IMDB_API.Models;
using Microsoft.EntityFrameworkCore;

namespace IMDB_API.Infrastructure.Repositories;

public class ActorsRepository : IActorsRepository
{
    private readonly ImdbDbContext _imdbDbContext;

    public ActorsRepository(ImdbDbContext imdbDbContext)
    {
        _imdbDbContext = imdbDbContext;
    }

    public async Task<Name?> GetActor(string nconst)
    {
        var actor = await _imdbDbContext.Names
            .FindAsync(nconst);
        return actor;
    }

    public IQueryable<Name> GetActors()
    {
        return _imdbDbContext.Names.AsNoTracking();
    }

    public async Task<int> GetCount()
    {
        return await _imdbDbContext.Names.CountAsync();
    }
}