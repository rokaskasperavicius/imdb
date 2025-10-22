using IMDB_API.Application.Common;
using IMDB_API.Application.DTOs;
using IMDB_API.Application.Interfaces;

namespace IMDB_API.Application.Services;

public class PeopleService : IPeopleService
{
    private readonly IPeopleRepository _peopleRepository;

    public PeopleService(IPeopleRepository peopleRepository)
    {
        _peopleRepository = peopleRepository;
    }

    public async Task<PagedResults<List<PersonDTO>>> GetPeople(
        int page,
        int pageSize)
    {
        var skip = (page - 1) * pageSize;

        var actorsCount = await _peopleRepository.GetCount();
        var actors = await _peopleRepository
            .GetActors(skip, pageSize);

        var mapped = actors.Select(n =>
            new PersonDTO
            {
                Id = n.Id.Trim(),
                PrimaryName = n.Name,
                BirthYear = n.BirthYear,
                DeathYear = n.DeathYear,
                Rating = n.Rating
            }).ToList();

        return new PagedResults<List<PersonDTO>>
        {
            Data = mapped,
            Page = page,
            PageSize = pageSize,
            TotalCount = actorsCount
        };
    }

    public async Task<PersonDTO?> GetPerson(string nconst)
    {
        var actor = await _peopleRepository.GetActor(nconst);

        if (actor == null) return null;

        return new PersonDTO
        {
            Id = actor.Id.Trim(),
            PrimaryName = actor.Name,
            BirthYear = actor.BirthYear,
            DeathYear = actor.DeathYear,
            Rating = actor.Rating
        };
    }
}