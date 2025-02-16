using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using workStationManager.Data;
using workStationManager.Views;

namespace workStationManager.ViewModels
{
    public partial class LoginViewModel : ViewModelBase
    {
        [ObservableProperty]
        private string username = string.Empty;

        [ObservableProperty]
        private string password = string.Empty;

        [ObservableProperty]
        private string errorMessage = string.Empty;

        [ObservableProperty]
        private bool showPassword;

        public LoginViewModel()
        {
            LoginCommand = new RelayCommand<Window>(Login);
        }

        public RelayCommand<Window> LoginCommand { get; }

        private void Login(Window? window)
        {
            if (string.IsNullOrWhiteSpace(Username) || string.IsNullOrWhiteSpace(Password))
            {
                ErrorMessage = "Unesite korisničko ime i lozinku!";
                return;
            }

            using var db = new AppDbContext();
            var user = db.Users
                .Include(u => u.WorkPositions)
                    .ThenInclude(uwp => uwp.WorkPosition)
                .FirstOrDefault(u => u.Username == Username);

            if (user == null || !BCrypt.Net.BCrypt.Verify(Password, user.Password))
            {
                ErrorMessage = "Neispravni podaci!";
                return;
            }

            ErrorMessage = "Prijava uspješna!";

            Window nextWindow;

            if (user.RoleId == 1)
            {
                nextWindow = new AdminView { DataContext = new AdminViewModel() };
            }
            else
            {
                nextWindow = new MainWindow { DataContext = new MainWindowViewModel(user) };
            }

            nextWindow.Show();

            if (Avalonia.Application.Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                var loginWindows = desktop.Windows.OfType<LoginView>().ToList(); 
                foreach (var openWindow in loginWindows)
                {
                    openWindow.Close();
                }
            }

            window?.Close();
        }

    }
}
