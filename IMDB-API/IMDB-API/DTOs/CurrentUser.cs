using System.Security.Claims;

namespace IMDB_API.DTOs;

public interface ICurrentUser
{
    int Id { get; }
}

public class CurrentUser : ICurrentUser
{
    public int Id { get; }

    public CurrentUser(IHttpContextAccessor accessor)
    {
        var user = accessor.HttpContext?.User;
        var stringId = user?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (!int.TryParse(stringId, out var id))
        {
            throw new UnauthorizedAccessException("Unauthorized");
        }
        
        Id = id;
    }
}