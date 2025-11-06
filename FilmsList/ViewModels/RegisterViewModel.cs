using FilmsList.Commands;
using FilmsList.Services;
using System;
using System.Windows.Input;

namespace FilmsList.ViewModels
{
    public class RegisterViewModel : ViewModelBase
    {
        private readonly IAuthService _authService;

        private string _username = "";
        private string _password = "";
        private string _confirmPassword = "";
        private string _email = "";
        private string _errorMessage = "";

        public string Username { get => _username; set { _username = value; OnPropertyChanged(); } }
        public string Password { get => _password; set { _password = value; OnPropertyChanged(); } }
        public string ConfirmPassword { get => _confirmPassword; set { _confirmPassword = value; OnPropertyChanged(); } }
        public string Email { get => _email; set { _email = value; OnPropertyChanged(); } }
        public string ErrorMessage { get => _errorMessage; set { _errorMessage = value; OnPropertyChanged(); } }

        public ICommand RegisterCommand { get; }
        public ICommand ShowLoginCommand { get; }

        public event Action? RegisterSuccessful;
        public event Action? ShowLoginRequested;

        public RegisterViewModel(IAuthService authService)
        {
            _authService = authService;
            RegisterCommand = new RelayCommand(Register);
            ShowLoginCommand = new RelayCommand(() => ShowLoginRequested?.Invoke());
        }

        private void Register()
        {
            if (string.IsNullOrWhiteSpace(Username) || string.IsNullOrWhiteSpace(Password) || string.IsNullOrWhiteSpace(Email))
            {
                ErrorMessage = "Заполните все поля";
                return;
            }

            if (Password != ConfirmPassword)
            {
                ErrorMessage = "Пароли не совпадают";
                return;
            }

            if (_authService.Register(Username, Password, Email))
            {
                ErrorMessage = "";
                RegisterSuccessful?.Invoke();
            }
            else
            {
                ErrorMessage = "Пользователь с таким логином или email уже существует";
            }
        }
    }
}