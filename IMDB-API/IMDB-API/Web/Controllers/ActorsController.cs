using IMDB_API.Application.Services;
using IMDB_API.Contracts.DTOs;
using IMDB_API.Contracts.Response;
using Microsoft.AspNetCore.Mvc;

namespace IMDB_API.Web.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ActorsController : ControllerBase
{
    private readonly IActorsService _actorsService;

    public ActorsController(IActorsService actorsService)
    {
        _actorsService = actorsService;
    }

    // GET: api/actors
    [HttpGet]
    public async Task<ActionResult<PagedResults<ActorDTO>>> GetActors(
        PagedQuery query)
    {
        var result = await _actorsService.GetActors(query.Page, query.PageSize);

        return Ok(result);
    }

    // GET: api/actors/nm0000001
    [HttpGet("{nconst}")]
    public async Task<ActionResult<ActorDTO>> GetActor(string nconst)
    {
        var actor = await _actorsService.GetActor(nconst);

        if (actor == null) return NotFound();

        return Ok(actor);
    }
}