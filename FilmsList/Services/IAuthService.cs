using FilmsList.Models;

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