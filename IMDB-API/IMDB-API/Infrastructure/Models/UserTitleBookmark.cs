using System;
using System.Collections.Generic;

namespace IMDB_API.Infrastructure.Models;

public partial class UserTitleBookmark
{
    public int UserId { get; set; }

    public string BasicTconst { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public virtual Basic BasicTconstNavigation { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
