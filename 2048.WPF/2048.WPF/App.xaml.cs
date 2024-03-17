using _2048.EntityFramework;
using _2048.EntityFramework.Commands;
using _2048.EntityFramework.Queries;
using _2048.WPF.Stores;
using _2048.WPF.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Configuration;
using System.Windows;

namespace _2048.WPF;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    protected override void OnStartup(StartupEventArgs e)
    {
        string connectionString = ConfigurationManager.ConnectionStrings["sqlite"].ConnectionString;

        _2048DbContextFactory _2048DbContextFactory = new(
    new DbContextOptionsBuilder().UseSqlite(connectionString).Options
    );

        using (_2048DbContext dbContext = _2048DbContextFactory.Create())
        {
            dbContext.Database.EnsureCreated();
        }
        NavigationStore.CreateInstance();
        LeaderboardStore.CreateInstance(new GetAllLeaderboardEntriesQuery(_2048DbContextFactory), new CreateLeaderboardEntryCommand(_2048DbContextFactory));
        UserStore.CreateInstance(new LoginUserCommand(_2048DbContextFactory), new RegisterUserCommand(_2048DbContextFactory));

        NavigationStore.Instance!.CurrentViewModel = new LoginViewModel();

        MainWindow mainWindow = new()
        {
            DataContext = new MainViewModel()
        };
        mainWindow.Show();

        base.OnStartup(e);
    }
}
