using EldredBrown.ProFootball.NETCore.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace EldredBrown.ProFootball.NETCore.Services
{
    public static class IServiceCollectionExtension
    {
        public static IServiceCollection AddServiceLibrary(this IServiceCollection services)
        {
            services.AddScoped<IWeeklyUpdateService, WeeklyUpdateService>();
            services.AddSingleton<ISeasonLeagueRepository, MockSeasonLeagueRepository>();
            services.AddSingleton<ISeasonLeagueTotalsRepository, MockSeasonLeagueTotalsRepository>();
            services.AddSingleton<IWeekCountRepository, MockWeekCountRepository>();
            services.AddScoped<ICalculator, Calculator>();

            return services;
        }
    }
}
