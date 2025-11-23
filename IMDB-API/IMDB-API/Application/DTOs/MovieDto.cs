namespace IMDB_API.Application.DTOs;

public class MovieDto
{
    public string Id { get; set; }
    public string Title { get; set; }
    public bool IsAdult { get; set; }
    public string? Year { get; set; }
    public int? RunTimeInMinutes { get; set; }
    public string? Plot { get; set; }
    public string? Poster { get; set; }
    public List<string> Genres { get; set; }
    public required decimal AverageRating { get; set; }
    public required decimal NumberOfVotes { get; set; }
}