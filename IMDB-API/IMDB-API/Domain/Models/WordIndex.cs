using System;
using System.Collections.Generic;

namespace IMDB_API.Models;

public partial class WordIndex
{
    public string Tconst { get; set; } = null!;

    public string Word { get; set; } = null!;

    public char Field { get; set; }

    public string? Lexeme { get; set; }

    public virtual Basic TconstNavigation { get; set; } = null!;
}
