using System;
using System.Collections.Generic;

namespace FilmsList.Models;

public partial class Movie
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public int? GenreId { get; set; }

    public virtual Genre? Genre { get; set; }

    public virtual ICollection<WatchedMovie> WatchedMovies { get; set; } = new List<WatchedMovie>();
}
