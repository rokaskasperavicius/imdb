using System.ComponentModel.DataAnnotations;

namespace IMDB_API.Web.Common;

public class RateMovieBody
{
    [Range(1, 10)] public int Rating { get; set; }
}