namespace IMDB_API.Domain;

public class Rating
{
    public int UserId { get; set; }
    public string TitleId { get; set; }
    public int? TitleRating { get; set; }
}