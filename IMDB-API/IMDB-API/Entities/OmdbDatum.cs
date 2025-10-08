﻿using System;
using System.Collections.Generic;

namespace IMDB_API.Entities;

public partial class OmdbDatum
{
    public string Tconst { get; set; } = null!;

    public string? Episode { get; set; }

    public string? Awards { get; set; }

    public string? Plot { get; set; }

    public string? Seriesid { get; set; }

    public string? Rated { get; set; }

    public string? Imdbrating { get; set; }

    public string? Runtime { get; set; }

    public string? Language { get; set; }

    public string? Released { get; set; }

    public string? Response { get; set; }

    public string? Writer { get; set; }

    public string? Genre { get; set; }

    public string? Title { get; set; }

    public string? Country { get; set; }

    public string? Dvd { get; set; }

    public string? Production { get; set; }

    public string? Season { get; set; }

    public string? Type { get; set; }

    public string? Poster { get; set; }

    public string? Ratings { get; set; }

    public string? Imdbvotes { get; set; }

    public string? Boxoffice { get; set; }

    public string? Actors { get; set; }

    public string? Director { get; set; }

    public string? Year { get; set; }

    public string? Website { get; set; }

    public string? Metascore { get; set; }

    public string? Totalseasons { get; set; }
}
