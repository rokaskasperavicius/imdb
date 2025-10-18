using System;
using System.Collections.Generic;

namespace IMDB_API.Models;

public partial class Basic
{
    public string Tconst { get; set; } = null!;

    public string Titletype { get; set; } = null!;

    public string Primarytitle { get; set; } = null!;

    public string Originaltitle { get; set; } = null!;

    public bool Isadult { get; set; }

    public string? Startyear { get; set; }

    public string? Endyear { get; set; }

    public int? Runtimeminutes { get; set; }

    public string? Plot { get; set; }

    public string? Poster { get; set; }

    public virtual ICollection<Aka> Akas { get; set; } = new List<Aka>();

    public virtual ICollection<Episode> EpisodeParenttconstNavigations { get; set; } = new List<Episode>();

    public virtual Episode? EpisodeTconstNavigation { get; set; }

    public virtual ICollection<Principal> Principals { get; set; } = new List<Principal>();

    public virtual Rating? Rating { get; set; }

    public virtual ICollection<UserTitleBookmark> UserTitleBookmarks { get; set; } = new List<UserTitleBookmark>();

    public virtual ICollection<UserTitleRating> UserTitleRatings { get; set; } = new List<UserTitleRating>();

    public virtual ICollection<WordIndex> WordIndices { get; set; } = new List<WordIndex>();

    public virtual ICollection<Genre> Genres { get; set; } = new List<Genre>();

    public virtual ICollection<Name> Nconsts { get; set; } = new List<Name>();

    public virtual ICollection<Name> Nconsts1 { get; set; } = new List<Name>();

    public virtual ICollection<Name> NconstsNavigation { get; set; } = new List<Name>();
}
