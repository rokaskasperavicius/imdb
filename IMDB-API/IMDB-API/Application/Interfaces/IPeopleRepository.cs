using IMDB_API.Domain;

namespace IMDB_API.Application.Interfaces;

public interface IPeopleRepository
{
    Task<int> GetCount();

    Task<List<Person>> GetPeople(int skip, int take);
    Task<Person?> GetPerson(string nconst);
}