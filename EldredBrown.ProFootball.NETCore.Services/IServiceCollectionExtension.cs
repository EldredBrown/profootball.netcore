using Microsoft.Extensions.DependencyInjection;
using EldredBrown.ProFootball.NETCore.Data.Repositories;

namespace EldredBrown.ProFootball.NETCore.Services
{
    public static class IServiceCollectionExtension
    {
        public static IServiceCollection AddServiceLibrary(this IServiceCollection services)
        {
            services.AddScoped<IGameService, GameService>();
            services.AddScoped<IProcessGameStrategyFactory, ProcessGameStrategyFactory>();
            services.AddScoped<IWeeklyUpdateService, WeeklyUpdateService>();

            services.AddSingleton<ILeagueSeasonTotalsRepository, MockLeagueSeasonTotalsRepository>();

            return services;
        }
    }
}
