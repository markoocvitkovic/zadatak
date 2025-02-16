# Workstation Manager

## Overview
This is an Avalonia .NET 8 application for managing workstations in a production environment. The application follows the MVVM (Model-View-ViewModel) pattern and utilizes the CommunityToolkit framework. Data is stored in a PostgreSQL database, and Entity Framework Core is used for database access.

## Features
### 1. Login Window
- Users log in using their username and password.

### 2. Admin View
- View currently assigned work positions along with the assignment time.
- Change the work position assigned to a selected user.
- Create new users.

### 3. User View
- Displays the user’s name, surname, and the workstation assigned to them.

## Database Schema
The database consists of four tables:
- **users**: Stores first name, last name, username, and password.
- **roles**: Stores role name and role description (Admin, User).
- **work_positions**: Stores work position names and descriptions.
- **user_work_positions**: Links users to work positions with product name and date.

## Installation and Setup
### Prerequisites
- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
- [PostgreSQL](https://www.postgresql.org/download/)
- [Entity Framework Core](https://docs.microsoft.com/en-us/ef/core/)
- [Avalonia UI](https://avaloniaui.net/)

### Steps to Set Up the Project
1. **Clone the repository:**
   ```sh
   git clone <repository-url>
   cd workstation-manager
   ```
2. **Set up the PostgreSQL database:**
   ```sql
   CREATE DATABASE workstation_manager;
   
   CREATE TABLE users (
       id SERIAL PRIMARY KEY,
       first_name VARCHAR(50),
       last_name VARCHAR(50),
       username VARCHAR(50) UNIQUE,
       password VARCHAR(255)
   );

   CREATE TABLE roles (
       id SERIAL PRIMARY KEY,
       role_name VARCHAR(50) UNIQUE,
       role_description TEXT
   );

   CREATE TABLE work_positions (
       id SERIAL PRIMARY KEY,
       position_name VARCHAR(50) UNIQUE,
       position_description TEXT
   );

   CREATE TABLE user_work_positions (
       id SERIAL PRIMARY KEY,
       user_id INT REFERENCES users(id),
       work_position_id INT REFERENCES work_positions(id),
       product_name VARCHAR(100),
       assigned_date TIMESTAMP DEFAULT CURRENT_TIMESTAMP
   );
   ```
3. **Update `appsettings.json` with your database connection:**
   ```json
   {
       "ConnectionStrings": {
           "DefaultConnection": "Host=localhost;Database=workstation_manager;Username=your_user;Password=your_password"
       }
   }
   ```
4. **Install required dependencies:**
   ```sh
   dotnet add package Microsoft.EntityFrameworkCore.Design
   dotnet add package Npgsql.EntityFrameworkCore.PostgreSQL
   dotnet add package CommunityToolkit.Mvvm
   ```
5. **Run the application:**
   ```sh
   dotnet run
   ```

## Architecture
The application follows the MVVM pattern using the CommunityToolkit framework. The structure is as follows:
- **Models**: Defines the data structures.
- **ViewModels**: Handles business logic and data binding.
- **Views**: XAML UI elements bound to ViewModels.

## License
This project is open-source.
