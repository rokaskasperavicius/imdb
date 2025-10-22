using IMDB_API.Domain;

namespace IMDB_API.Application.Interfaces;

public interface IPeopleRepository
{
    Task<int> GetCount();

    IQueryable<Person> GetActors();
    Task<Person?> GetActor(string nconst);
}