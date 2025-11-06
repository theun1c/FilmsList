using FilmsList.Services;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmsList.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private ViewModelBase _currentViewModel;

        public ViewModelBase CurrentViewModel
        {
            get => _currentViewModel;
            set => this.RaiseAndSetIfChanged(ref _currentViewModel, value);
        }

        private readonly IAuthService _authService;

        public MainWindowViewModel(IAuthService authService)
        {
            _authService = authService;

            // Начинаем с экрана логина
            var loginViewModel = new LoginViewModel(authService);
            loginViewModel.LoginSuccessful += OnLoginSuccessful;
            loginViewModel.ShowRegisterRequested += ShowRegister;

            CurrentViewModel = loginViewModel;
        }

        private void ShowRegister()
        {
            var registerViewModel = new RegisterViewModel(_authService);
            registerViewModel.RegisterSuccessful += OnRegisterSuccessful;
            registerViewModel.ShowLoginRequested += ShowLogin;

            CurrentViewModel = registerViewModel;
        }

        private void ShowLogin()
        {
            var loginViewModel = new LoginViewModel(_authService);
            loginViewModel.LoginSuccessful += OnLoginSuccessful;
            loginViewModel.ShowRegisterRequested += ShowRegister;

            CurrentViewModel = loginViewModel;
        }

        private void OnLoginSuccessful()
        {
            // TODO: Перейти на главный экран с фильмами
            Console.WriteLine($"Пользователь вошел: {_authService.CurrentUser?.Username}");
        }

        private void OnRegisterSuccessful()
        {
            // После регистрации показываем логин
            ShowLogin();
        }
    }
}
