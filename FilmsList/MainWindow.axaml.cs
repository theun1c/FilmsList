using Avalonia.Controls;
using FilmsList.Services;
using System;

namespace FilmsList
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            var users = DbService.GetUsers();

            Console.WriteLine($"users: {users.Count}");
        }
    }
}