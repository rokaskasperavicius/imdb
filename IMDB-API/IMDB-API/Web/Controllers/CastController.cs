using IMDB_API.Application.DTOs;
using IMDB_API.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace IMDB_API.Web.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CastController : ControllerBase
{
    private readonly ICastService _castService;

    public CastController(ICastService castService)
    {
        _castService = castService;
    }

    // GET: api/cast/tt0000001
    [HttpGet("{tconst}")]
    public async Task<ActionResult<List<CastDto>>> GetCast(string tconst)
    {
        var cast = await _castService.GetCast(tconst);

        return Ok(cast);
    }
}