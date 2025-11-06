using FilmsList.Services;

namespace FilmsList.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private ViewModelBase _currentViewModel;

        public ViewModelBase CurrentViewModel
        {
            get => _currentViewModel;
            set { _currentViewModel = value; OnPropertyChanged(); }
        }

        private readonly IAuthService _authService;

        public MainWindowViewModel(IAuthService authService)
        {
            _authService = authService;
            ShowLogin();
        }

        private void ShowLogin()
        {
            var loginVM = new LoginViewModel(_authService);
            loginVM.LoginSuccessful += OnLoginSuccessful;
            loginVM.ShowRegisterRequested += ShowRegister;
            CurrentViewModel = loginVM;
        }

        private void ShowRegister()
        {
            var registerVM = new RegisterViewModel(_authService);
            registerVM.RegisterSuccessful += OnRegisterSuccessful;
            registerVM.ShowLoginRequested += ShowLogin;
            CurrentViewModel = registerVM;
        }

        private void OnLoginSuccessful()
        {
            var mainVM = new MainViewModel(_authService);
            mainVM.LogoutRequested += ShowLogin;
            CurrentViewModel = mainVM;
        }

        private void OnRegisterSuccessful()
        {
            ShowLogin();
        }
    }
}