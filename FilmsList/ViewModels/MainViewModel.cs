using FilmsList.Services;

namespace FilmsList.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly IAuthService _authService;

        public string WelcomeMessage => $"Добро пожаловать, {_authService.CurrentUser?.Username}!";

        public MainViewModel(IAuthService authService)
        {
            _authService = authService;
        }
    }
}