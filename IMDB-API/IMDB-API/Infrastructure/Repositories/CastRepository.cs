using System.Linq.Expressions;
using IMDB_API.Application.Interfaces;
using IMDB_API.Domain;
using IMDB_API.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace IMDB_API.Infrastructure.Repositories;

public class CastRepository : ICastRepository
{
    private static readonly Expression<Func<Principal, Cast>> CastProjection =
        c => new Cast
        {
            Ordering = c.Ordering,
            PersonId = c.Nconst,
            PersonName = c.NconstNavigation.Primaryname,
            Category = c.Category,
            Character = c.Characters,
            Job = c.Job
        };

    private readonly ImdbDbContext _imdbDbContext;

    public CastRepository(ImdbDbContext imdbDbContext)
    {
        _imdbDbContext = imdbDbContext;
    }

    public async Task<List<Cast>> GetCast(string tconst)
    {
        var result = await _imdbDbContext.Principals
            .AsNoTracking()
            .Where(p => p.Tconst == tconst)
            .OrderByDescending(p => p.Ordering)
            .Select(CastProjection)
            .ToListAsync();

        return result;
    }
}