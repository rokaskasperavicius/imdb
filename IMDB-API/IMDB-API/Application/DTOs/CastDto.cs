namespace IMDB_API.Application.DTOs;

public class CastDto
{
    public int Ordering { get; set; }

    public string? PersonId { get; set; }

    public string Category { get; set; } = null!;

    public string? Job { get; set; }

    public string? Character { get; set; }
}