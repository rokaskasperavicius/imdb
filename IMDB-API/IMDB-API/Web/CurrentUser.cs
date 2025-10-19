using System.Security.Claims;

namespace IMDB_API.Web;

public interface ICurrentUser
{
    int Id { get; }
}

public class CurrentUser : ICurrentUser
{
    public CurrentUser(IHttpContextAccessor accessor)
    {
        var user = accessor.HttpContext?.User;
        var stringId = user?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (!int.TryParse(stringId, out var id))
            throw new UnauthorizedAccessException("Unauthorized");

        Id = id;
    }

    public int Id { get; }
}