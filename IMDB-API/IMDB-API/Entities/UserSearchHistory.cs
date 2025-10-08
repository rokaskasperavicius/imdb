using System;
using System.Collections.Generic;

namespace IMDB_API.Entities;

public partial class UserSearchHistory
{
    public int Id { get; set; }

    public int? UserId { get; set; }

    public string SearchQuery { get; set; } = null!;

    public DateTime? CreatedAt { get; set; }

    public virtual User? User { get; set; }
}
