using System;
using System.Collections.Generic;

namespace IMDB_API.Infrastructure.Models;

public partial class Episode
{
    public string Tconst { get; set; } = null!;

    public string? Parenttconst { get; set; }

    public int? Seasonnumber { get; set; }

    public int? Episodenumber { get; set; }

    public virtual Basic? ParenttconstNavigation { get; set; }

    public virtual Basic TconstNavigation { get; set; } = null!;
}
