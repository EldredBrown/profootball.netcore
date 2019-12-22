using System;
using System.Threading.Tasks;
using EldredBrown.ProFootball.NETCore.Data;
using EldredBrown.ProFootball.NETCore.Data.Repositories;
using EldredBrown.ProFootball.NETCore.Services;
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

            services.AddDefaultIdentity<IdentityUser>(opts => opts.SignIn.RequireConfirmedAccount = true)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ProFootballDbContext>();

            services.AddScoped<ILeagueRepository, LeagueRepository>();
            services.AddScoped<ITeamRepository, TeamRepository>();
            services.AddScoped<ISeasonRepository, SeasonRepository>();
            services.AddScoped<IGameRepository, GameRepository>();
            services.AddScoped<ILeagueSeasonRepository, LeagueSeasonRepository>();
            services.AddScoped<ITeamSeasonRepository, TeamSeasonRepository>();
            services.AddScoped<ITeamSeasonScheduleProfileRepository, TeamSeasonScheduleProfileRepository>();
            services.AddScoped<ITeamSeasonScheduleTotalsRepository, TeamSeasonScheduleTotalsRepository>();
            services.AddScoped<ITeamSeasonScheduleAveragesRepository, TeamSeasonScheduleAveragesRepository>();
            services.AddScoped<ISeasonStandingsRepository, SeasonStandingsRepository>();
            services.AddScoped<ISharedRepository, SharedRepository>();

            services.AddServiceLibrary();

            services.AddControllersWithViews();
            services.AddRazorPages();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider services)
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

            CreateUserRole(services).Wait();
        }

        private async Task CreateUserRole(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();

            var roleCheck = await roleManager.RoleExistsAsync("Admin");
            if (!roleCheck)
            {
                await roleManager.CreateAsync(new IdentityRole("Admin"));
            }

            var user = await userManager.FindByEmailAsync("eldred.brown@outlook.com");
            if (user != null)
            {
                await userManager.AddToRoleAsync(user, "Admin");
            }
        }
    }
}
