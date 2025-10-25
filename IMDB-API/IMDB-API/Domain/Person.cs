namespace IMDB_API.Domain;

public class Person
{
    public string Id { get; set; }
    public string? Name { get; set; }
    public string? BirthYear { get; set; }
    public string? DeathYear { get; set; }
    public decimal? Rating { get; set; }
    public List<KnownForTitles> KnownForTitles { get; set; }
    public List<PersonProfession> Professions { get; set; }
}

public class KnownForTitles
{
    public string Id { get; set; }
}

public class PersonProfession
{
    public string Name { get; set; }
}