using IMDB_API.Application.DTOs;
using IMDB_API.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IMDB_API.Web.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BookmarksController : ControllerBase
{
    private readonly IBookmarksService _bookmarksService;

    public BookmarksController(IBookmarksService bookmarksService)
    {
        _bookmarksService = bookmarksService;
    }

    // GET: api/bookmarks
    [HttpGet]
    [Authorize]
    public async Task<ActionResult<List<BookmarkDTO>>> GetBookmarks(
        ICurrentUser currentUser)
    {
        var result = await _bookmarksService.GetBookmarks(currentUser.Id);

        return Ok(result);
    }

    // POST: api/bookmarks/tt0063929
    [HttpPost("{tconst}")]
    [Authorize]
    public ActionResult<BookmarkDTO> CreateBookmark(
        [FromRoute] string tconst, ICurrentUser currentUser)
    {
        try
        {
            _bookmarksService.CreateBookmark(currentUser.Id, tconst);

            return Created($"/bookmarks/{tconst}",
                new BookmarkDTO
                {
                    Id = tconst
                });
        }
        catch (Exception ex)
        {
            return Conflict(ex.Message);
        }
    }

    // DELETE: api/bookmarks/tt0063929
    [HttpDelete("{tconst}")]
    [Authorize]
    public ActionResult DeleteBookmark(
        [FromRoute] string tconst, ICurrentUser currentUser)
    {
        _bookmarksService.DeleteBookmark(currentUser.Id, tconst);
        return NoContent();
    }
}