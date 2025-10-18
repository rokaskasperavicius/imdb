using IMDB_API.Context;
using IMDB_API.DTOs;
using Microsoft.EntityFrameworkCore;

namespace IMDB_API.Repositories;

public class ActorsRepository : IActorsRepository
{
    private readonly ImdbDbContext _imdbDbContext;

    public ActorsRepository(ImdbDbContext imdbDbContext)
    {
        _imdbDbContext = imdbDbContext;
    }

    public async Task<int> GetCountAsync()
    {
        return await _imdbDbContext.Names.CountAsync();
    }

    public async Task<List<ActorsDto>> GetActorsAsync(int pageSize, int skip)
    {
        return await _imdbDbContext
            .Names
            .OrderBy(n => n.Primaryname)
            .Skip(skip)
            .Take(pageSize)
            .Select(n =>
                new ActorsDto(
                    n.Primaryname,
                    n.Birthyear,
                    n.Deathyear,
                    n.Rating))
            .ToListAsync();
    }

    public async Task<ActorsDto?> GetActorAsync(string nconst)
    {
        var actor = await _imdbDbContext
            .Names
            .FindAsync(nconst);

        if (actor == null) return null;

        return new ActorsDto(
            actor.Primaryname,
            actor.Birthyear,
            actor.Deathyear,
            actor.Rating);
    }
}