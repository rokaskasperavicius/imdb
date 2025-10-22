using System;
using System.Collections.Generic;

namespace IMDB_API.Infrastructure.Models;

public partial class UserTitleRating
{
    public int UserId { get; set; }

    public string BasicTconst { get; set; } = null!;

    public int? Rating { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual Basic BasicTconstNavigation { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
