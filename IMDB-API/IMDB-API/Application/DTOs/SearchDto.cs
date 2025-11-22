namespace IMDB_API.Application.DTOs;

public class SearchDto
{
    public required string Query { get; set; }
    public required DateTime CreatedAt { get; set; }
}