using IMDB_API.Application.Common;
using IMDB_API.Application.DTOs;
using IMDB_API.Application.Services;
using IMDB_API.Web.Common;
using Microsoft.AspNetCore.Mvc;

namespace IMDB_API.Web.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PeopleController : ControllerBase
{
    private readonly IPeopleService _peopleService;

    public PeopleController(IPeopleService peopleService)
    {
        _peopleService = peopleService;
    }

    // GET: api/people
    [HttpGet]
    public async Task<ActionResult<PagedResults<List<PersonDto>>>> GetPeople(
        PagedQuery query, [FromQuery] List<string> ids)
    {
        var result = await _peopleService.GetPeople(query.Page, query.PageSize);

        return Ok(result);
    }

    // GET: api/people/batch
    [HttpGet("batch")]
    public async Task<ActionResult<List<PersonDto>>>
        GetPeopleBatch([FromQuery] List<string> ids)
    {
        var result = await _peopleService.GetPeopleBatch(ids);

        return Ok(result);
    }

    // GET: api/people/nm0000001
    [HttpGet("{nconst}")]
    public async Task<ActionResult<PersonDto>> GetPerson(string nconst)
    {
        var person = await _peopleService.GetPerson(nconst);

        if (person == null) return NotFound();

        return Ok(person);
    }

    // GET: api/people/nm0000001/related
    [HttpGet("{nconst}/related")]
    public async Task<ActionResult<List<PersonDto>>> GetRelatedPeople(
        string nconst)
    {
        var people = await _peopleService.GetRelatedPeople(nconst);

        return Ok(people);
    }
}