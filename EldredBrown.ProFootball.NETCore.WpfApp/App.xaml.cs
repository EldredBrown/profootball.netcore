using System.Windows;
using EldredBrown.ProFootball.NETCore.Data;
using EldredBrown.ProFootball.NETCore.Data.Repositories;
using EldredBrown.ProFootball.NETCore.Services;
using EldredBrown.ProFootball.NETCore.WpfApp.Main;
using EldredBrown.ProFootball.NETCore.WpfApp.Properties;
using EldredBrown.ProFootball.NETCore.WpfApp.UserControls.Rankings;
using EldredBrown.ProFootball.NETCore.WpfApp.UserControls.SeasonStandings;
using EldredBrown.ProFootball.NETCore.WpfApp.UserControls.TeamSeasons;
using EldredBrown.ProFootball.NETCore.WpfApp.Windows.GameFinder;
using EldredBrown.ProFootball.NETCore.WpfApp.Windows.GamePredictor;
using EldredBrown.ProFootball.NETCore.WpfApp.Windows.Games;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

namespace EldredBrown.ProFootball.NETCore.WpfApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static ServiceProvider ServiceProvider;

        public App()
        {
            var services = new ServiceCollection();
            ConfigureServices(services);
            ServiceProvider = services.BuildServiceProvider();
        }

        private void ConfigureServices(ServiceCollection services)
        {
            services.AddDbContextPool<ProFootballDbContext>(options =>
            {
                options.UseSqlServer(Settings.Default.ProFootballDbConnectionString);
            });

            services.AddSingleton<MainWindow>();
            services.AddScoped<IMainWindowViewModel, MainWindowViewModel>();
            services.AddScoped<ITeamSeasonsControlViewModel, TeamSeasonsControlViewModel>();
            services.AddScoped<ISeasonStandingsControlViewModel, SeasonStandingsControlViewModel>();
            services.AddScoped<IRankingsControlViewModel, RankingsControlViewModel>();

            services.AddScoped<IGamesWindowFactory, GamesWindowFactory>();
            services.AddScoped<IGamesWindow, GamesWindow>();
            services.AddScoped<IGamesWindowViewModel, GamesWindowViewModel>();

            services.AddScoped<IGameFinderWindowFactory, GameFinderWindowFactory>();
            services.AddScoped<IGameFinderWindow, GameFinderWindow>();
            services.AddScoped<IGameFinderWindowViewModel, GameFinderWindowViewModel>();

            services.AddScoped<IGamePredictorWindowFactory, GamePredictorWindowFactory>();
            services.AddScoped<IGamePredictorWindow, GamePredictorWindow>();
            services.AddScoped<IGamePredictorWindowViewModel, GamePredictorWindowViewModel>();

            services.AddScoped<IMessageBoxService, MessageBoxService>();
            services.AddScoped<IGameService, GameService>();
            services.AddScoped<IGamePredictorService, GamePredictorService>();
            services.AddScoped<IWeeklyUpdateService, WeeklyUpdateService>();

            services.AddScoped<IProcessGameStrategyFactory, ProcessGameStrategyFactory>();

            services.AddScoped<ISeasonRepository, SeasonRepository>();
            services.AddScoped<ITeamSeasonRepository, TeamSeasonRepository>();
            services.AddScoped<ITeamSeasonScheduleProfileRepository, TeamSeasonScheduleProfileRepository>();
            services.AddScoped<ITeamSeasonScheduleTotalsRepository, TeamSeasonScheduleTotalsRepository>();
            services.AddScoped<ITeamSeasonScheduleAveragesRepository, TeamSeasonScheduleAveragesRepository>();
            services.AddScoped<ISeasonStandingsRepository, SeasonStandingsRepository>();
            services.AddScoped<IGameRepository, GameRepository>();
            services.AddScoped<ILeagueSeasonRepository, LeagueSeasonRepository>();
            services.AddScoped<ILeagueSeasonTotalsRepository, MockLeagueSeasonTotalsRepository>();
            services.AddScoped<ISharedRepository, SharedRepository>();
        }

        private void OnStartup(object sender, StartupEventArgs e)
        {
            var mainWindow = ServiceProvider.GetService<MainWindow>();

            if (mainWindow is null)
            {
                return;
            }

            mainWindow.Show();
        }
    }
}

#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
