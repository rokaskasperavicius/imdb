using IMDB_API.Application.Common;
using IMDB_API.Application.DTOs;

namespace IMDB_API.Application.Services;

public interface IPeopleService
{
    Task<PagedResults<List<PersonDTO>>> GetPeople(int page, int pageSize);
    Task<PersonDTO?> GetPerson(string nconst);
}