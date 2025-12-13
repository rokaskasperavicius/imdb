namespace IMDB_API.Application.Common;

public class PagedResults<T>
{
    public required T Data { get; set; }
    public required int TotalCount { get; set; }
    public required int Page { get; set; }
    public required int PageSize { get; set; }

    public int TotalPages =>
        (int)Math.Ceiling((double)TotalCount / PageSize);
}