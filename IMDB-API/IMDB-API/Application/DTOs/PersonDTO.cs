namespace IMDB_API.Application.DTOs;

public class PersonDto
{
    public string Id { get; set; }
    public string? PrimaryName { get; set; }
    public string? BirthYear { get; set; }
    public string? DeathYear { get; set; }
    public decimal? Rating { get; set; }
    public List<string> KnownForTitles { get; set; }
    public List<string> Professions { get; set; }
}