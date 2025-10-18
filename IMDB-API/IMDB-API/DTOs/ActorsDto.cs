namespace IMDB_API.DTOs;

public class ActorsDto
{
    public ActorsDto(
        string? primaryName,
        string? birthYear,
        string? deathYear,
        decimal? rating)
    {
        PrimaryName = primaryName;
        BirthYear = birthYear;
        DeathYear = deathYear;
        Rating = rating;
    }

    public string? PrimaryName { get; private set; }
    public string? BirthYear { get; private set; }
    public string? DeathYear { get; private set; }
    public decimal? Rating { get; private set; }
}