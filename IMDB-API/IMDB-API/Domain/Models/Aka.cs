using System;
using System.Collections.Generic;

namespace IMDB_API.Models;

public partial class Aka
{
    public string Tconst { get; set; } = null!;

    public int Ordering { get; set; }

    public string? Title { get; set; }

    public string? Region { get; set; }

    public string? Language { get; set; }

    public string? Types { get; set; }

    public string? Attributes { get; set; }

    public bool? Isoriginaltitle { get; set; }

    public virtual Basic TconstNavigation { get; set; } = null!;
}
