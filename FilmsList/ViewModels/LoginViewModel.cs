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
    public class LoginViewModel : ViewModelBase
    {
        private string _username = string.Empty;
        private string _password = string.Empty;
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

        public string ErrorMessage
        {
            get => _errorMessage;
            set => this.RaiseAndSetIfChanged(ref _errorMessage, value);
        }

        public ReactiveCommand<Unit, Unit> LoginCommand { get; }
        public ReactiveCommand<Unit, Unit> ShowRegisterCommand { get; }

        public event Action? LoginSuccessful;
        public event Action? ShowRegisterRequested;

        private readonly IAuthService _authService;

        public LoginViewModel(IAuthService authService)
        {
            _authService = authService;

            LoginCommand = ReactiveCommand.Create(Login);
            ShowRegisterCommand = ReactiveCommand.Create(() => ShowRegisterRequested?.Invoke());
        }

        private void Login()
        {
            if (string.IsNullOrWhiteSpace(Username) || string.IsNullOrWhiteSpace(Password))
            {
                ErrorMessage = "Заполните все поля";
                return;
            }

            if (_authService.Login(Username, Password))
            {
                ErrorMessage = string.Empty;
                LoginSuccessful?.Invoke();
            }
            else
            {
                ErrorMessage = "Неверный логин или пароль";
            }
        }
    }
}
