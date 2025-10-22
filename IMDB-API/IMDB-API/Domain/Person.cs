namespace IMDB_API.Domain;

public class Person
{
    public string Id { get; set; }
    public string? Name { get; set; }
    public string? BirthYear { get; set; }
    public string? DeathYear { get; set; }
    public decimal? Rating { get; set; }
}