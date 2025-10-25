using IMDB_API.Application.Common;
using IMDB_API.Application.DTOs;

namespace IMDB_API.Application.Services;

public interface IPeopleService
{
    Task<PagedResults<List<PersonDto>>> GetPeople(int page, int pageSize);
    Task<PersonDto?> GetPerson(string nconst);
}