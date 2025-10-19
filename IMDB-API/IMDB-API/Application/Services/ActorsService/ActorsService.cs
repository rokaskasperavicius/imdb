using IMDB_API.Application.Interfaces;
using IMDB_API.Contracts.DTOs;
using IMDB_API.Contracts.Response;
using Microsoft.EntityFrameworkCore;

namespace IMDB_API.Application.Services;

public class ActorsService : IActorsService
{
    private readonly IActorsRepository _actorsRepository;

    public ActorsService(IActorsRepository actorsRepository)
    {
        _actorsRepository = actorsRepository;
    }

    public async Task<PagedResults<List<ActorDTO>>> GetActors(
        int page,
        int pageSize)
    {
        var actorsCount = await _actorsRepository.GetCount();
        var actors = await _actorsRepository
            .GetActors()
            .OrderBy(n => n.Primaryname)
            .Skip(pageSize * (page - 1))
            .Take(pageSize)
            .Select(n =>
                new ActorDTO
                {
                    PrimaryName = n.Primaryname,
                    BirthYear = n.Birthyear,
                    DeathYear = n.Deathyear,
                    Rating = n.Rating
                })
            .ToListAsync();

        return new PagedResults<List<ActorDTO>>
        {
            Data = actors,
            Page = page,
            PageSize = pageSize,
            TotalCount = actorsCount
        };
    }

    public async Task<ActorDTO?> GetActor(string nconst)
    {
        var actor = await _actorsRepository.GetActor(nconst);

        if (actor == null) return null;

        return new ActorDTO
        {
            PrimaryName = actor.Primaryname,
            BirthYear = actor.Birthyear,
            DeathYear = actor.Deathyear,
            Rating = actor.Rating
        };
    }
}