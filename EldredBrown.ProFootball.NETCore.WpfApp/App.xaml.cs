using System.Windows;
using EldredBrown.ProFootball.NETCore.Data;
using EldredBrown.ProFootball.NETCore.Data.Repositories;
using EldredBrown.ProFootball.NETCore.Services;
using EldredBrown.ProFootball.NETCore.WpfApp.Properties;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

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

            services.AddScoped<ISeasonRepository, SeasonRepository>();
            services.AddScoped<ITeamSeasonRepository, TeamSeasonRepository>();
            services.AddScoped<ITeamSeasonScheduleProfileRepository, TeamSeasonScheduleProfileRepository>();
            services.AddScoped<ITeamSeasonScheduleTotalsRepository, TeamSeasonScheduleTotalsRepository>();
            services.AddScoped<ITeamSeasonScheduleAveragesRepository, TeamSeasonScheduleAveragesRepository>();
            services.AddScoped<ISeasonStandingsRepository, SeasonStandingsRepository>();
            services.AddScoped<IGameRepository, GameRepository>();

            services.AddScoped<IGameService, GameService>();
            services.AddScoped<IProcessGameStrategyFactory, ProcessGameStrategyFactory>();
        }

        private void OnStartup(object sender, StartupEventArgs e)
        {
            var mainWindow = ServiceProvider.GetService<MainWindow>();
            mainWindow.Show();
        }
    }
}
