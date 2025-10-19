namespace IMDB_API.Contracts.Response;

public class PagedResults<T>
{
    public T Data { get; set; }
    public int TotalCount { get; set; }
    public int Page { get; set; }
    public int PageSize { get; set; }

    public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);
}