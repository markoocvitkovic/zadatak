using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using workStationManager.Models;
using workStationManager.Views;

namespace workStationManager.ViewModels
{
    public partial class MainWindowViewModel : ViewModelBase
    {
        [ObservableProperty]
        private string fullName = string.Empty;

        [ObservableProperty]
        private ObservableCollection<string> workPositions = new();

        public MainWindowViewModel(User user)
        {
            fullName = $"{user.FirstName} {user.LastName}";

            foreach (var wp in user.WorkPositions)
            {
                workPositions.Add(wp.WorkPosition.PositionName);
            }
        }
        [RelayCommand]
        private void Logout(Window? window)
        {
            var loginView = new LoginView { DataContext = new LoginViewModel() };
            loginView.Show();
            window?.Close();
        }
    }
}
