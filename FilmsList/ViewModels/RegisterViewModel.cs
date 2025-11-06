using FilmsList.Services;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;

namespace FilmsList.ViewModels
{
    public class RegisterViewModel : ViewModelBase
    {
        private string _username = string.Empty;
        private string _password = string.Empty;
        private string _confirmPassword = string.Empty;
        private string _email = string.Empty;
        private string _errorMessage = string.Empty;

        public string Username
        {
            get => _username;
            set => this.RaiseAndSetIfChanged(ref _username, value);
        }

        public string Password
        {
            get => _password;
            set => this.RaiseAndSetIfChanged(ref _password, value);
        }

        public string ConfirmPassword
        {
            get => _confirmPassword;
            set => this.RaiseAndSetIfChanged(ref _confirmPassword, value);
        }

        public string Email
        {
            get => _email;
            set => this.RaiseAndSetIfChanged(ref _email, value);
        }

        public string ErrorMessage
        {
            get => _errorMessage;
            set => this.RaiseAndSetIfChanged(ref _errorMessage, value);
        }

        public ReactiveCommand<Unit, Unit> RegisterCommand { get; }
        public ReactiveCommand<Unit, Unit> ShowLoginCommand { get; }

        public event Action? RegisterSuccessful;
        public event Action? ShowLoginRequested;

        private readonly IAuthService _authService;

        public RegisterViewModel(IAuthService authService)
        {
            _authService = authService;

            RegisterCommand = ReactiveCommand.Create(Register);
            ShowLoginCommand = ReactiveCommand.Create(() => ShowLoginRequested?.Invoke());
        }

        private void Register()
        {
            if (string.IsNullOrWhiteSpace(Username) ||
                string.IsNullOrWhiteSpace(Password) ||
                string.IsNullOrWhiteSpace(Email))
            {
                ErrorMessage = "Заполните все поля";
                return;
            }

            if (Password != ConfirmPassword)
            {
                ErrorMessage = "Пароли не совпадают";
                return;
            }

            if (Password.Length < 3)
            {
                ErrorMessage = "Пароль должен быть не менее 3 символов";
                return;
            }

            if (_authService.Register(Username, Password, Email))
            {
                ErrorMessage = string.Empty;
                RegisterSuccessful?.Invoke();
            }
            else
            {
                ErrorMessage = "Пользователь с таким логином или email уже существует";
            }
        }
    }
}
