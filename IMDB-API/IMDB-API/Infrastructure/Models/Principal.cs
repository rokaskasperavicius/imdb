using System;
using System.Collections.Generic;

namespace IMDB_API.Infrastructure.Models;

public partial class Principal
{
    public string Tconst { get; set; } = null!;

    public int Ordering { get; set; }

    public string? Nconst { get; set; }

    public string Category { get; set; } = null!;

    public string? Job { get; set; }

    public string? Characters { get; set; }

    public virtual Name? NconstNavigation { get; set; }

    public virtual Basic TconstNavigation { get; set; } = null!;
}
