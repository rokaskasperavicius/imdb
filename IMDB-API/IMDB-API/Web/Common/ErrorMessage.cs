namespace IMDB_API.Web.Common;

public class ErrorMessage
{
    public bool IsError { get; set; } = true;

    public string Message { get; set; }
}