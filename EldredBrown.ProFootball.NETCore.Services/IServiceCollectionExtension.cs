﻿using EldredBrown.ProFootball.NETCore.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;

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
