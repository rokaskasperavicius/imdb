using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace IMDB_API.Web;

public class PagedQuery
{
    [FromQuery] [Range(1, int.MaxValue)] public int Page { get; set; } = 1;

    [FromQuery] [Range(20, 200)] public int PageSize { get; set; } = 20;
}