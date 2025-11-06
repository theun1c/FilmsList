using FilmsList.Data;
using FilmsList.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmsList.Services
{
    public class AuthService : IAuthService
    {
        public User? CurrentUser { get; private set; }

        public bool Login(string username, string password)
        {
            using var db = new AppDbContext();
            var user = db.Users.FirstOrDefault(u => u.Username == username && u.Password == password);

            if (user != null)
            {
                CurrentUser = user;
                return true;
            }
            return false;
        }

        public bool Register(string username, string password, string email)
        {
            using var db = new AppDbContext();

            // Проверяем нет ли такого пользователя
            if (db.Users.Any(u => u.Username == username || u.Email == email))
                return false;

            var newUser = new User
            {
                Username = username,
                Password = password,
                Email = email,
                IsAdmin = false
            };

            db.Users.Add(newUser);
            db.SaveChanges();
            return true;
        }

        public void Logout()
        {
            CurrentUser = null;
        }
    }
}
