using System;
using System.Collections.Generic;

namespace FilmsList.Models;

public partial class WatchedMovie
{
    public int Id { get; set; }

    public int? UserId { get; set; }

    public int? MovieId { get; set; }

    public virtual Movie? Movie { get; set; }

    public virtual User? User { get; set; }
}
