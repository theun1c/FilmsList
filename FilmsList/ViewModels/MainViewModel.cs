using FilmsList.Commands;
using FilmsList.Models;
using FilmsList.Services;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace FilmsList.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly IAuthService _authService;
        private readonly MovieService _movieService;

        private string _searchText = "";
        private string _selectedGenre = "Все жанры";

        public string WelcomeMessage => $"Добро пожаловать, {_authService.CurrentUser?.Username}!";

        public string SearchText
        {
            get => _searchText;
            set { _searchText = value; OnPropertyChanged(); UpdateMovies(); }
        }

        public string SelectedGenre
        {
            get => _selectedGenre;
            set { _selectedGenre = value; OnPropertyChanged(); UpdateMovies(); }
        }

        public ObservableCollection<Movie> Movies { get; } = new ObservableCollection<Movie>();
        public ObservableCollection<string> Genres { get; } = new ObservableCollection<string>();

        public ICommand LogoutCommand { get; }

        public event Action? LogoutRequested;

        public MainViewModel(IAuthService authService)
        {
            _authService = authService;
            _movieService = new MovieService();

            LogoutCommand = new RelayCommand(Logout);
            LoadData();
        }

        private void LoadData()
        {
            // Загружаем жанры
            var allGenres = _movieService.GetAllGenres();
            Genres.Add("Все жанры");
            foreach (var genre in allGenres)
            {
                Genres.Add(genre.Name);
            }

            UpdateMovies();
        }

        private void UpdateMovies()
        {
            var allMovies = _movieService.GetAllMovies();

            // Фильтруем по поиску
            var filteredMovies = allMovies.Where(m =>
                m.Title.ToLower().Contains(SearchText.ToLower()) ||
                m.Description.ToLower().Contains(SearchText.ToLower())
            ).ToList();

            // Фильтруем по жанру
            if (SelectedGenre != "Все жанры")
            {
                filteredMovies = filteredMovies.Where(m => m.Genre?.Name == SelectedGenre).ToList();
            }

            // Обновляем список
            Movies.Clear();
            foreach (var movie in filteredMovies)
            {
                Movies.Add(movie);
            }
        }

        private void Logout()
        {
            _authService.Logout();
            LogoutRequested?.Invoke();
        }
    }
}