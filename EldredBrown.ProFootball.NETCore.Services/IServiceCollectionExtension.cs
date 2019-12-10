using EldredBrown.ProFootball.NETCore.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace EldredBrown.ProFootball.NETCore.Services
{
    public static class IServiceCollectionExtension
    {
        public static IServiceCollection AddServiceLibrary(this IServiceCollection services)
        {
            services.AddScoped<IGameService, GameService>();
            services.AddScoped<IWeeklyUpdateService, WeeklyUpdateService>();

            services.AddSingleton<ILeagueSeasonRepository, MockLeagueSeasonRepository>();
            services.AddSingleton<ILeagueSeasonTotalsRepository, MockLeagueSeasonTotalsRepository>();

            services.AddScoped<ICalculator, Calculator>();

            return services;
        }
    }
}
