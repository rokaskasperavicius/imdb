using IMDB_API.Application.Common;
using IMDB_API.Application.DTOs;
using IMDB_API.Application.Services;
using IMDB_API.Web.Common;
using Microsoft.AspNetCore.Mvc;

namespace IMDB_API.Web.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MoviesController : ControllerBase
{
    private readonly IMoviesService _moviesService;

    public MoviesController(IMoviesService moviesService)
    {
        _moviesService = moviesService;
    }

    // GET: api/movies/tt0000001
    [HttpGet("{tconst}")]
    public async Task<ActionResult<MovieDto>> GetMovie(string tconst)
    {
        var movie = await _moviesService.GetMovie(tconst);

        if (movie == null) return NotFound();

        return Ok(movie);
    }

    // GET: api/movies
    [HttpGet]
    public async Task<ActionResult<PagedResults<List<MovieDto>>>> GetMovies(
        PagedQuery query)
    {
        var result =
            await _moviesService.GetMovies(query.Page, query.PageSize);

        return Ok(result);
    }

    // GET: api/movies/batch
    [HttpGet("batch")]
    public async Task<ActionResult<List<MovieDto>>>
        GetMoviesBatch([FromQuery] List<string> ids)
    {
        var result = await _moviesService.GetMoviesBatch(ids);

        return Ok(result);
    }

    // GET: api/movies/tt0000001/related
    [HttpGet("{tconst}/related")]
    public async Task<ActionResult<List<MovieDto>>> GetRelatedMovies(
        string tconst)
    {
        var movies = await _moviesService.GetRelatedMovies(tconst);

        return Ok(movies);
    }
}