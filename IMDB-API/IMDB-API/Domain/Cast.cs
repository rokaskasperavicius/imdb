namespace IMDB_API.Domain;

public class Cast
{
    public int Ordering { get; set; }

    public string PersonId { get; set; }
    public string? PersonName { get; set; }

    public string Category { get; set; } = null!;

    public string? Job { get; set; }

    public string? Character { get; set; }
}