using IMDB_API.Models;
using IMDB_API.Repositories;
using IMDB_API.Shared;
using Microsoft.AspNetCore.Mvc;

namespace IMDB_API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ActorsController : ControllerBase
{
    private readonly IActorsRepository _actorsRepository;

    public ActorsController(IActorsRepository actorsRepository)
    {
        _actorsRepository = actorsRepository;
    }

    // GET: api/actors
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Name>>> GetActors(
        PagedQuery query)
    {
        var count = await _actorsRepository.GetCountAsync();
        var totalPages = (int)Math.Ceiling(count / (double)query.PageSize);
        var actors =
            await _actorsRepository.GetActorsAsync(query.PageSize, query.Skip);

        return Ok(new
        {
            query.Page,
            query.PageSize,
            totalPages,
            Data = actors
        });
    }

    // GET: api/actors/nm0000001
    [HttpGet("{nconst}")]
    public async Task<ActionResult<Name>> GetActor(string nconst)
    {
        var actor = await _actorsRepository.GetActorAsync(nconst);

        if (actor == null) return NotFound();

        return Ok(actor);
    }
}