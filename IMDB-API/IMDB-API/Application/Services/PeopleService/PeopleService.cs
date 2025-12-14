using IMDB_API.Application.Common;
using IMDB_API.Application.DTOs;
using IMDB_API.Application.Interfaces;
using IMDB_API.Domain;

namespace IMDB_API.Application.Services;

public class PeopleService : IPeopleService
{
    private readonly IPeopleRepository _peopleRepository;

    public PeopleService(IPeopleRepository peopleRepository)
    {
        _peopleRepository = peopleRepository;
    }

    public async Task<PagedResults<List<PersonDto>>> GetPeople(
        int page,
        int pageSize)
    {
        var skip = (page - 1) * pageSize;

        var peopleCount = await _peopleRepository.GetCount();
        var people = await _peopleRepository
            .GetPeople(skip, pageSize);

        var mapped = people.Select(PersonToDto).ToList();

        return new PagedResults<List<PersonDto>>
        {
            Data = mapped,
            Page = page,
            PageSize = pageSize,
            TotalCount = peopleCount
        };
    }

    public async Task<PersonDto?> GetPerson(string nconst)
    {
        var person = await _peopleRepository.GetPerson(nconst);

        return person != null ? PersonToDto(person) : null;
    }

    public async Task<List<PersonDto>> GetRelatedPeople(string nconst)
    {
        var people = await _peopleRepository.GetRelatedPeople(nconst);

        return people.Select(PersonToDto).ToList();
    }

    public async Task<List<PersonDto>> GetPeopleBatch(List<string> ids)
    {
        var people = await _peopleRepository.GetPeopleByIds(ids);

        return people.Select(PersonToDto).ToList();
    }

    public async Task<List<PersonDto>> GetPeopleBySearch(int userId,
        string searchQuery)
    {
        var people =
            await _peopleRepository.GetPeopleBySearch(userId, searchQuery);

        return people.Select(PersonToDto).ToList();
    }

    private static PersonDto PersonToDto(Person person)
    {
        return new PersonDto
        {
            Id = person.Id.Trim(),
            PrimaryName = person.Name,
            BirthYear = person.BirthYear,
            DeathYear = person.DeathYear,
            Rating = person.Rating,
            KnownForMovies =
                person.KnownForTitles
                    .Where(kft => kft.TitleType == "movie")
                    .Select(kft => kft.Id.Trim()).ToList(),
            Professions = person.Professions.Select(g => g.Name).ToList()
        };
    }
}