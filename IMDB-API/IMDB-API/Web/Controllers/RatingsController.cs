using IMDB_API.Application.DTOs;
using IMDB_API.Application.Services;
using IMDB_API.Web.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IMDB_API.Web.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RatingsController : ControllerBase
{
    private readonly IRatingsService _ratingsService;

    public RatingsController(IRatingsService ratingsService)
    {
        _ratingsService = ratingsService;
    }

    // POST: api/ratings/tt0000001/rate
    [HttpPost("{tconst}/rate")]
    [Authorize]
    public ActionResult Rate(
        [FromRoute] string tconst,
        [FromBody] RateMovieBody rating,
        ICurrentUser currentUser)
    {
        try
        {
            _ratingsService.Rate(currentUser.Id, tconst, rating.Rating);

            return NoContent();
        }
        catch (Exception ex)
        {
            return Conflict(ex.Message);
        }
    }

    [HttpGet]
    [Authorize]
    public async Task<ActionResult<List<RatingDto>>> GetRatings(
        ICurrentUser currentUser)
    {
        try
        {
            var result = await _ratingsService.GetRatings(currentUser.Id);

            return Ok(result);
        }
        catch (Exception ex)
        {
            return Conflict(ex.Message);
        }
    }
}