using System.ComponentModel.DataAnnotations.Schema;

namespace IMDB_API.Infrastructure.Models;

public class SimilarMoviesRow
{
    public string Tconst { get; set; } = null!;

    [Column("genres_count")] public int GenresCount { get; set; }
}