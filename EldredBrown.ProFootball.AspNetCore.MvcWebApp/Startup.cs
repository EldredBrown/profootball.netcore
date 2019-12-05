using EldredBrown.ProFootball.NETCore.Data;
using EldredBrown.ProFootball.NETCore.Data.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace EldredBrown.ProFootball.AspNetCore.MvcWebApp
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

            services.AddDefaultIdentity<IdentityUser>().AddEntityFrameworkStores<ProFootballDbContext>();

            // TODO: 2019-11-30 - The mock repositories need to be added as singletons until they are replaced by repositories for SQL data.
            services.AddScoped<ITeamRepository, TeamRepository>();
            services.AddScoped<ISeasonRepository, SeasonRepository>();
            services.AddScoped<IGameRepository, GameRepository>();
            services.AddScoped<ISeasonTeamRepository, SeasonTeamRepository>();
            services.AddSingleton<ISeasonTeamScheduleProfileRepository, MockSeasonTeamScheduleProfileRepository>();
            services.AddSingleton<ISeasonTeamScheduleTotalsRepository, MockSeasonTeamScheduleTotalsRepository>();
            services.AddSingleton<ISeasonTeamScheduleAveragesRepository, MockSeasonTeamScheduleAveragesRepository>();
            services.AddSingleton<ISeasonStandingsRepository, MockSeasonStandingsRepository>();

            services.AddControllersWithViews();
            services.AddRazorPages();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                endpoints.MapRazorPages();
            });
        }
    }
}
