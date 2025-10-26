using System.Linq.Expressions;
using IMDB_API.Application.Interfaces;
using IMDB_API.Domain;
using IMDB_API.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace IMDB_API.Infrastructure.Repositories;

public class PeopleRepository : IPeopleRepository
{
    private static readonly Expression<Func<Name, Person>> PersonProjection =
        n => new Person
        {
            Id = n.Nconst,
            Name = n.Primaryname,
            BirthYear = n.Birthyear,
            DeathYear = n.Deathyear,
            Rating = n.Rating,
            // NameKnownForTitle
            KnownForTitles = n.Tconsts1
                .Select(b => new KnownForTitles { Id = b.Tconst })
                .ToList(),
            Professions = n.Professions
                .Select(p => new PersonProfession { Name = p.Name })
                .ToList()
        };

    private readonly ImdbDbContext _imdbDbContext;

    public PeopleRepository(ImdbDbContext imdbDbContext)
    {
        _imdbDbContext = imdbDbContext;
    }

    public async Task<int> GetCount()
    {
        return await _imdbDbContext.Names.CountAsync();
    }

    public async Task<Person?> GetPerson(string nconst)
    {
        var person = await _imdbDbContext.Names
            .Where(p => p.Nconst == nconst)
            .Select(PersonProjection)
            .SingleOrDefaultAsync();

        return person;
    }

    public async Task<List<Person>> GetPeople(int skip, int take)
    {
        var people = await _imdbDbContext.Names
            .AsNoTracking()
            .OrderByDescending(p => p.Rating ?? 0)
            .ThenBy(p => p.Primaryname)
            .Skip(skip)
            .Take(take)
            .Select(PersonProjection)
            .ToListAsync();

        return people;
    }

    public async Task<List<Person>> GetPeopleByIds(List<string> ids)
    {
        var people = await _imdbDbContext.Names
            .AsNoTracking()
            .Where(n => ids.Contains(n.Nconst))
            .OrderByDescending(p => p.Rating ?? 0)
            .ThenBy(p => p.Primaryname)
            .Select(PersonProjection)
            .ToListAsync();

        return people;
    }

    public async Task<List<Person>> GetRelatedPeople(string nconst)
    {
        var person = new NpgsqlParameter("person", nconst);
        var rows = await _imdbDbContext.Database
            .SqlQueryRaw<FrequentCoPlayersRow>(
                "select * from f_frequent_co_players({0})", person)
            .ToListAsync();

        // Needed to maintain sorting
        var freqDic = rows
            .Select(r => new { r.Nconst, r.Frequency })
            .ToDictionary(r => r.Nconst, r => r.Frequency);

        var people = await _imdbDbContext.Names
            .Where(n => freqDic.Keys.Contains(n.Nconst))
            .Select(PersonProjection)
            .ToListAsync();

        return people
            .OrderByDescending(p => freqDic[p.Id])
            .ThenBy(p => p.Name)
            .ToList();
    }
}