using Avalonia.Controls;
using FilmsList.Services;
using FilmsList.ViewModels;

namespace FilmsList
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            // Устанавливаем DataContext
            var authService = new AuthService();
            DataContext = new MainWindowViewModel(authService);
        }
    }
}