using System;
using System.Collections.ObjectModel;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore;
using workStationManager.Data;
using workStationManager.Models;
using BCrypt.Net;
using Avalonia.Controls;
using workStationManager.Views;

namespace workStationManager.ViewModels
{
    public partial class AdminViewModel : ViewModelBase
    {
        [ObservableProperty]
        private ObservableCollection<User> users = new();

        [ObservableProperty]
        private ObservableCollection<WorkPosition> workPositions = new();

        [ObservableProperty]
        private ObservableCollection<Role> roles = new();

        [ObservableProperty]
        private User? selectedUser;

        [ObservableProperty]
        private WorkPosition? selectedWorkPosition;

        [ObservableProperty]
        private string newFirstName = string.Empty;

        [ObservableProperty]
        private string newLastName = string.Empty;

        [ObservableProperty]
        private string newUsername = string.Empty;

        [ObservableProperty]
        private string newPassword = string.Empty;

        [ObservableProperty]
        private Role? selectedRole;

        [ObservableProperty]
        private string errorMessage = string.Empty;

        public AdminViewModel()
        {
            LoadData();
        }

        private void LoadData()
        {
            using var db = new AppDbContext();
            Users = new ObservableCollection<User>(
                db.Users
                  .Include(u => u.Role)
                  .Include(u => u.WorkPositions)
                      .ThenInclude(uwp => uwp.WorkPosition)
                  .AsNoTracking()
                  .ToList()
            );

            WorkPositions = new ObservableCollection<WorkPosition>(
                db.WorkPositions.AsNoTracking().ToList()
            );

            Roles = new ObservableCollection<Role>(
                db.Roles.AsNoTracking().ToList()
            );
        }

        [RelayCommand]
        private void ChangeUserWorkPosition()
        {
            if (SelectedUser is null || SelectedWorkPosition is null)
                return;

            using var db = new AppDbContext();

            var trackedUser = db.Users
                .Include(u => u.WorkPositions)
                .FirstOrDefault(u => u.Id == SelectedUser.Id);

            if (trackedUser is null)
                return;

            trackedUser.WorkPositions.Clear();

            var workPositionFromDb = db.WorkPositions.FirstOrDefault(wp => wp.Id == SelectedWorkPosition.Id);
            if (workPositionFromDb is null)
                return;

            trackedUser.WorkPositions.Add(new UserWorkPosition
            {
                UserId = trackedUser.Id,
                WorkPositionId = workPositionFromDb.Id,
                User = trackedUser,         
                WorkPosition = workPositionFromDb, 
                ProductName = "Updated Product",
                WorkDate = DateTime.UtcNow
            });

            db.SaveChanges();
            LoadData();
        }

        [RelayCommand]
        private void CreateUser()
        {
            if (string.IsNullOrWhiteSpace(NewFirstName) || string.IsNullOrWhiteSpace(NewLastName) ||
                string.IsNullOrWhiteSpace(NewUsername) || string.IsNullOrWhiteSpace(NewPassword) ||
                SelectedRole is null)
            {
                ErrorMessage = "Sva polja su obavezna!";
                return;
            }

            using var db = new AppDbContext();

            var existingUser = db.Users.FirstOrDefault(u => u.Username == NewUsername);
            if (existingUser != null)
            {
                ErrorMessage = "Korisničko ime već postoji!";
                return;
            }

            var newUser = new User
            {
                FirstName = NewFirstName,
                LastName = NewLastName,
                Username = NewUsername,
                Password = BCrypt.Net.BCrypt.HashPassword(NewPassword),
                RoleId = SelectedRole.Id
            };

            db.Users.Add(newUser);
            db.SaveChanges();
            LoadData();

            NewFirstName = string.Empty;
            NewLastName = string.Empty;
            NewUsername = string.Empty;
            NewPassword = string.Empty;
            SelectedRole = null;
            ErrorMessage = "Korisnik uspješno dodan!";
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
