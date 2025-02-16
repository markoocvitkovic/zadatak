using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using workStationManager.ViewModels;
using workStationManager.Views;

namespace workStationManager
{
    public partial class App : Application
    {
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public override void OnFrameworkInitializationCompleted()
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                desktop.MainWindow = new LoginView
                {
                    DataContext = new LoginViewModel()
                };
            }

            base.OnFrameworkInitializationCompleted();
        }
    }
}

