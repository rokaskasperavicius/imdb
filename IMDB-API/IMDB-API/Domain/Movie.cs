namespace IMDB_API.Domain;

public class Movie
{
    public string Id { get; set; }
    public string Title { get; set; }
    public bool IsAdult { get; set; }
    public string? Year { get; set; }
    public int? RunTimeInMinutes { get; set; }
    public string? Plot { get; set; }
    public string? Poster { get; set; }
    public decimal AverageRating { get; set; }
    public decimal NumberOfVotes { get; set; }

    public List<MovieGenre> Genres { get; set; }
}

public class MovieGenre
{
    public string Name { get; set; }
}