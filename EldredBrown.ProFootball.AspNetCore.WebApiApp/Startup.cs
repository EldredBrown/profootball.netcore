using AutoMapper;
using EldredBrown.ProFootball.NETCore.Data;
using EldredBrown.ProFootball.NETCore.Data.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace EldredBrown.ProFootball.AspNetCore.WebApiApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContextPool<ProFootballDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("ProFootballDb"));
            });

            // TODO: 2019-12-05 - The mock repositories need to be added as singletons until they are replaced by repositories for SQL data.
            services.AddScoped<ITeamRepository, TeamRepository>();
            services.AddScoped<IGameRepository, GameRepository>();
            services.AddScoped<ISeasonRepository, SeasonRepository>();
            services.AddScoped<ITeamSeasonRepository, TeamSeasonRepository>();
            services.AddScoped<ITeamSeasonScheduleProfileRepository, TeamSeasonScheduleProfileRepository>();
            services.AddScoped<ITeamSeasonScheduleTotalsRepository, TeamSeasonScheduleTotalsRepository>();
            services.AddScoped<ITeamSeasonScheduleAveragesRepository, TeamSeasonScheduleAveragesRepository>();
            services.AddScoped<ISeasonStandingsRepository, SeasonStandingsRepository>();
            services.AddScoped<ISharedRepository, SharedRepository>();

            services.AddAutoMapper(typeof(Startup));

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
