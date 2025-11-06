using FilmsList.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmsList.Services
{
    public interface IAuthService
    {
        User? CurrentUser { get; }
        bool Login(string username, string password);
        bool Register(string username, string password, string email);
        void Logout();
    }
}
