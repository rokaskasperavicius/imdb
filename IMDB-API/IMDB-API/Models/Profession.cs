using System;
using System.Collections.Generic;

namespace IMDB_API.Models;

public partial class Profession
{
    public string Name { get; set; } = null!;

    public virtual ICollection<Name> Nconsts { get; set; } = new List<Name>();
}
