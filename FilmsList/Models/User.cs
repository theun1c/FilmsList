using System;
using System.Collections.Generic;

namespace FilmsList.Models;

public partial class User
{
    public int Id { get; set; }

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Email { get; set; } = null!;

    public bool? IsAdmin { get; set; }

    public virtual ICollection<WatchedMovie> WatchedMovies { get; set; } = new List<WatchedMovie>();
}
