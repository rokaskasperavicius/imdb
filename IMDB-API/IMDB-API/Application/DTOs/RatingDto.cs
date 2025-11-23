namespace IMDB_API.Application.DTOs;

public class RatingDto
{
    public required string Id { get; set; }
    public required int Rating { get; set; }
    public required DateTime CreatedAt { get; set; }
}