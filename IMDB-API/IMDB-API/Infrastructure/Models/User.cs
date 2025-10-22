using System;
using System.Collections.Generic;

namespace IMDB_API.Infrastructure.Models;

public partial class User
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public virtual ICollection<UserSearchHistory> UserSearchHistories { get; set; } = new List<UserSearchHistory>();

    public virtual ICollection<UserTitleBookmark> UserTitleBookmarks { get; set; } = new List<UserTitleBookmark>();

    public virtual ICollection<UserTitleRating> UserTitleRatings { get; set; } = new List<UserTitleRating>();
}
