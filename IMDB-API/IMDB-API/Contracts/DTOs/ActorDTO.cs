namespace IMDB_API.Contracts.DTOs;

public class ActorDTO
{
    public string? PrimaryName { get; set; }
    public string? BirthYear { get; set; }
    public string? DeathYear { get; set; }
    public decimal? Rating { get; set; }
}