using System;
using System.Collections.Generic;

namespace IMDB_API.Models;

public partial class Genre
{
    public string Name { get; set; } = null!;

    public virtual ICollection<Basic> Tconsts { get; set; } = new List<Basic>();
}
