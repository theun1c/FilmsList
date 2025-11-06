using FilmsList.Data;
using FilmsList.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmsList.Services
{
    public class DbService
    {
        public static List<User> GetUsers()
        {
            using var db = new AppDbContext();
            return db.Users.ToList();
        }
    }
}
