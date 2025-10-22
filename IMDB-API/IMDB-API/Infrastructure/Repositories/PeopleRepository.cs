using IMDB_API.Application.Interfaces;
using IMDB_API.Domain;
using Microsoft.EntityFrameworkCore;

namespace IMDB_API.Infrastructure.Repositories;

public class PeopleRepository : IPeopleRepository
{
    private readonly ImdbDbContext _imdbDbContext;

    public PeopleRepository(ImdbDbContext imdbDbContext)
    {
        _imdbDbContext = imdbDbContext;
    }

    public async Task<int> GetCount()
    {
        return await _imdbDbContext.Names.CountAsync();
    }

    public async Task<Person?> GetActor(string nconst)
    {
        var actor = await _imdbDbContext.Names
            .FindAsync(nconst);

        if (actor == null) return null;

        return new Person
        {
            Id = actor.Nconst,
            Name = actor.Primaryname,
            BirthYear = actor.Birthyear,
            DeathYear = actor.Deathyear,
            Rating = actor.Rating
        };
    }

    public async Task<List<Person>> GetActors(int skip, int take)
    {
        var actors = await _imdbDbContext.Names
            .AsNoTracking()
            .OrderBy(n => n.Primaryname)
            .Skip(skip)
            .Take(take)
            .Select(n =>
                new Person
                {
                    Id = n.Nconst,
                    Name = n.Primaryname,
                    BirthYear = n.Birthyear,
                    DeathYear = n.Deathyear,
                    Rating = n.Rating
                }).ToListAsync();

        return actors;
    }
}