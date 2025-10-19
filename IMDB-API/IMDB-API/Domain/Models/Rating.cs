using System;
using System.Collections.Generic;

namespace IMDB_API.Models;

public partial class Rating
{
    public string Tconst { get; set; } = null!;

    public decimal Averagerating { get; set; }

    public int Numvotes { get; set; }

    public virtual Basic TconstNavigation { get; set; } = null!;
}
