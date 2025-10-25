namespace IMDB_API.Infrastructure.Models;

public class FrequentCoPlayersRow
{
    public string Nconst { get; set; } = null!;
    public string? Primaryname { get; set; }
    public int Frequency { get; set; }
}