using EldredBrown.ProFootball.NETCore.Data.Repositories;
using EldredBrown.ProFootball.NETCore.Data.Utilities;
using Microsoft.Extensions.DependencyInjection;

namespace EldredBrown.ProFootball.NETCore.Services
{
    public static class IServiceCollectionExtension
    {
        public static IServiceCollection AddServiceLibrary(this IServiceCollection services)
        {
            services.AddScoped<IGameService, GameService>();
            services.AddScoped<IGameUtility, GameUtility>();
            services.AddScoped<ILeagueSeasonUtility, LeagueSeasonUtility>();
            services.AddScoped<IProcessGameStrategyFactory, ProcessGameStrategyFactory>();
            services.AddScoped<ITeamSeasonUtility, TeamSeasonUtility>();
            services.AddScoped<IWeeklyUpdateService, WeeklyUpdateService>();

            services.AddSingleton<ILeagueSeasonTotalsRepository, MockLeagueSeasonTotalsRepository>();

            return services;
        }
    }
}
