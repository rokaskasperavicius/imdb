using IMDB_API.Application.Common;
using IMDB_API.Application.DTOs;
using IMDB_API.Application.Services;
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

    // GET: api/actors
    [HttpGet]
    public async Task<ActionResult<PagedResults<PersonDTO>>> GetActors(
        PagedQuery query)
    {
        var result = await _peopleService.GetPeople(query.Page, query.PageSize);

        return Ok(result);
    }

    // GET: api/actors/nm0000001
    [HttpGet("{nconst}")]
    public async Task<ActionResult<PersonDTO>> GetActor(string nconst)
    {
        var actor = await _peopleService.GetPerson(nconst);

        if (actor == null) return NotFound();

        return Ok(actor);
    }
}