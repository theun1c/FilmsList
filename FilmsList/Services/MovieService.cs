using FilmsList.Data;
using FilmsList.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace FilmsList.Services
{
    public class MovieService
    {
        public List<Movie> GetAllMovies()
        {
            using var db = new AppDbContext();
            return db.Movies
                .Include(m => m.Genre)
                .ToList();
        }

        public List<Genre> GetAllGenres()
        {
            using var db = new AppDbContext();
            return db.Genres.ToList();
        }
    }
}