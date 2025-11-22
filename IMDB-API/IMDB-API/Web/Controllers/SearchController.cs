using IMDB_API.Application.DTOs;
using IMDB_API.Application.Services;
using IMDB_API.Web.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IMDB_API.Web.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SearchController : ControllerBase
{
    private readonly ISearchService _searchService;

    public SearchController(ISearchService searchService)
    {
        _searchService = searchService;
    }

    // GET: api/search
    [HttpGet]
    [Authorize]
    public async Task<ActionResult<List<SearchDto>>> Search(
        ICurrentUser currentUser)
    {
        var result =
            await _searchService.GetUserSearches(currentUser.Id);

        return Ok(result);
    }
}