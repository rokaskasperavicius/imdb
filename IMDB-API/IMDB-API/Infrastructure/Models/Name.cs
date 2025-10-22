using System;
using System.Collections.Generic;

namespace IMDB_API.Infrastructure.Models;

public partial class Name
{
    public string Nconst { get; set; } = null!;

    public string? Primaryname { get; set; }

    public string? Birthyear { get; set; }

    public string? Deathyear { get; set; }

    public decimal? Rating { get; set; }

    public virtual ICollection<Principal> Principals { get; set; } = new List<Principal>();

    public virtual ICollection<Profession> Professions { get; set; } = new List<Profession>();

    public virtual ICollection<Basic> Tconsts { get; set; } = new List<Basic>();

    public virtual ICollection<Basic> Tconsts1 { get; set; } = new List<Basic>();

    public virtual ICollection<Basic> TconstsNavigation { get; set; } = new List<Basic>();
}
