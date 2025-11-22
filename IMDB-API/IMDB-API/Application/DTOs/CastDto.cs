namespace IMDB_API.Application.DTOs;

public class CastDto
{
    public required int Ordering { get; set; }

    public required string PersonId { get; set; }
    public string? PersonName { get; set; }

    public required string Category { get; set; } = null!;

    public string? Job { get; set; }

    public string? Character { get; set; }
}