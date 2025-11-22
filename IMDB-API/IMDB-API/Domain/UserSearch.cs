namespace IMDB_API.Domain;

public class UserSearch
{
    public string Query { get; set; }
    public int UserId { get; set; }
    public DateTime CreatedAt { get; set; }
}