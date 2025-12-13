namespace IMDB_API.Application.DTOs;

public class SearchDto
{
    public required int Id { get; set; }
    public required string Query { get; set; }
    public required DateTime CreatedAt { get; set; }
}