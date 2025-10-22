using IMDB_API.Domain;

namespace IMDB_API.Application.Interfaces;

public interface IPeopleRepository
{
    Task<int> GetCount();

    Task<List<Person>> GetActors(int skip, int take);
    Task<Person?> GetActor(string nconst);
}