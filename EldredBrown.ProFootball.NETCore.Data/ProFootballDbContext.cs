using EldredBrown.ProFootball.NETCore.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EldredBrown.ProFootball.NETCore.Data
{
    /// <summary>
    /// Represents the ProFootball database.
    /// </summary>
    public class ProFootballDbContext : IdentityDbContext<IdentityUser>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProFootballDbContext"/> class.
        /// </summary>
        /// <param name="options"></param>
        public ProFootballDbContext(DbContextOptions<ProFootballDbContext> options)
            : base(options)
        {

        }

        /// <summary>
        /// Gets or sets the Leagues data source.
        /// </summary>
        public DbSet<League> Leagues { get; set; }

        /// <summary>
        /// Gets or sets the Conferences data source.
        /// </summary>
        public DbSet<Conference> Conferences { get; set; }

        /// <summary>
        /// Gets or sets the Divisions data source.
        /// </summary>
        public DbSet<Division> Divisions { get; set; }

        /// <summary>
        /// Gets or sets the Teams data source.
        /// </summary>
        public DbSet<Team> Teams { get; set; }

        /// <summary>
        /// Gets or sets the Seasons data source.
        /// </summary>
        public DbSet<Season> Seasons { get; set; }

        /// <summary>
        /// Gets or sets the Games data source.
        /// </summary>
        public DbSet<Game> Games { get; set; }

        /// <summary>
        /// Gets or sets the SeasonLeagues data source.
        /// </summary>
        public DbSet<SeasonLeague> SeasonLeagues { get; set; }

        /// <summary>
        /// Gets or sets the SeasonTeams data source.
        /// </summary>
        public DbSet<SeasonTeam> SeasonTeams { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            SeedLeagues(modelBuilder);
            SeedTeams(modelBuilder);
            SeedSeasons(modelBuilder);
            SeedSeasonLeagues(modelBuilder);
            SeedSeasonTeams(modelBuilder);
        }

        private void SeedLeagues(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<League>()
                .HasAlternateKey(league => league.LongName)
                .HasName("AlternateKey_LongName");

            modelBuilder.Entity<League>()
                .HasAlternateKey(league => league.ShortName)
                .HasName("AlternateKey_ShortName");

            modelBuilder.Entity<League>().HasData(new League
            {
                ID = 1,
                LongName = "American Professional Football Association",
                ShortName = "APFA",
                FirstSeasonId = 1920,
                LastSeasonId = 1921
            });

            modelBuilder.Entity<League>().HasData(new League
            {
                ID = 2,
                LongName = "National Football League",
                ShortName = "NFL",
                FirstSeasonId = 1922
            });

            modelBuilder.Entity<League>().HasData(new League
            {
                ID = 3,
                LongName = "All-America Football Conference",
                ShortName = "AAFC",
                FirstSeasonId = 1946,
                LastSeasonId = 1949
            });

            modelBuilder.Entity<League>().HasData(new League
            {
                ID = 4,
                LongName = "American Football League",
                ShortName = "AFL",
                FirstSeasonId = 1960,
                LastSeasonId = 1969
            });
        }

        private void SeedTeams(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Team>().HasData(new Team
            {
                ID = 1,
                Name = "Akron Pros"
            });

            modelBuilder.Entity<Team>().HasData(new Team
            {
                ID = 2,
                Name = "Buffalo All-Americans"
            });

            modelBuilder.Entity<Team>().HasData(new Team
            {
                ID = 3,
                Name = "Canton Bulldogs"
            });

            modelBuilder.Entity<Team>().HasData(new Team
            {
                ID = 4,
                Name = "Chicago Cardinals"
            });

            modelBuilder.Entity<Team>().HasData(new Team
            {
                ID = 5,
                Name = "Chicago Tigers"
            });

            modelBuilder.Entity<Team>().HasData(new Team
            {
                ID = 6,
                Name = "Cleveland Tigers"
            });

            modelBuilder.Entity<Team>().HasData(new Team
            {
                ID = 7,
                Name = "Columbus Panhandles"
            });

            modelBuilder.Entity<Team>().HasData(new Team
            {
                ID = 8,
                Name = "Dayton Triangles"
            });

            modelBuilder.Entity<Team>().HasData(new Team
            {
                ID = 9,
                Name = "Decatur Staleys"
            });

            modelBuilder.Entity<Team>().HasData(new Team
            {
                ID = 10,
                Name = "Detroit Heralds"
            });

            modelBuilder.Entity<Team>().HasData(new Team
            {
                ID = 11,
                Name = "Hammond Pros"
            });

            modelBuilder.Entity<Team>().HasData(new Team
            {
                ID = 12,
                Name = "Muncie Flyers"
            });

            modelBuilder.Entity<Team>().HasData(new Team
            {
                ID = 13,
                Name = "Rochester Jeffersons"
            });

            modelBuilder.Entity<Team>().HasData(new Team
            {
                ID = 14,
                Name = "Rock Island Independents"
            });
        }

        private void SeedSeasons(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Season>().HasData(new Season
            {
                ID = 1920,
                NumOfWeeks = 13
            });
        }

        private void SeedSeasonLeagues(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SeasonLeague>().HasData(new SeasonLeague
            {
                ID = 1,
                SeasonId = 1920,
                LeagueName = "American Professional Football Association",
                TotalGames = 0,
                TotalPoints = 0
            });
        }

        private void SeedSeasonTeams(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SeasonTeam>().HasData(new SeasonTeam
            {
                ID = 1,
                SeasonId = 1920,
                TeamName = "Akron Pros",
                LeagueName = "American Professional Football Association"
            });

            modelBuilder.Entity<SeasonTeam>().HasData(new SeasonTeam
            {
                ID = 2,
                SeasonId = 1920,
                TeamName = "Buffalo All-Americans",
                LeagueName = "American Professional Football Association"
            });

            modelBuilder.Entity<SeasonTeam>().HasData(new SeasonTeam
            {
                ID = 3,
                SeasonId = 1920,
                TeamName = "Canton Bulldogs",
                LeagueName = "American Professional Football Association"
            });

            modelBuilder.Entity<SeasonTeam>().HasData(new SeasonTeam
            {
                ID = 4,
                SeasonId = 1920,
                TeamName = "Chicago Cardinals",
                LeagueName = "American Professional Football Association"
            });

            modelBuilder.Entity<SeasonTeam>().HasData(new SeasonTeam
            {
                ID = 5,
                SeasonId = 1920,
                TeamName = "Chicago Tigers",
                LeagueName = "American Professional Football Association"
            });

            modelBuilder.Entity<SeasonTeam>().HasData(new SeasonTeam
            {
                ID = 6,
                SeasonId = 1920,
                TeamName = "Cleveland Tigers",
                LeagueName = "American Professional Football Association"
            });

            modelBuilder.Entity<SeasonTeam>().HasData(new SeasonTeam
            {
                ID = 7,
                SeasonId = 1920,
                TeamName = "Columbus Panhandles",
                LeagueName = "American Professional Football Association"
            });

            modelBuilder.Entity<SeasonTeam>().HasData(new SeasonTeam
            {
                ID = 8,
                SeasonId = 1920,
                TeamName = "Dayton Triangles",
                LeagueName = "American Professional Football Association"
            });

            modelBuilder.Entity<SeasonTeam>().HasData(new SeasonTeam
            {
                ID = 9,
                SeasonId = 1920,
                TeamName = "Decatur Staleys",
                LeagueName = "American Professional Football Association"
            });

            modelBuilder.Entity<SeasonTeam>().HasData(new SeasonTeam
            {
                ID = 10,
                SeasonId = 1920,
                TeamName = "Detroit Heralds",
                LeagueName = "American Professional Football Association"
            });

            modelBuilder.Entity<SeasonTeam>().HasData(new SeasonTeam
            {
                ID = 11,
                SeasonId = 1920,
                TeamName = "Hammond Pros",
                LeagueName = "American Professional Football Association"
            });

            modelBuilder.Entity<SeasonTeam>().HasData(new SeasonTeam
            {
                ID = 12,
                SeasonId = 1920,
                TeamName = "Muncie Flyers",
                LeagueName = "American Professional Football Association"
            });

            modelBuilder.Entity<SeasonTeam>().HasData(new SeasonTeam
            {
                ID = 13,
                SeasonId = 1920,
                TeamName = "Rochester Jeffersons",
                LeagueName = "American Professional Football Association"
            });

            modelBuilder.Entity<SeasonTeam>().HasData(new SeasonTeam
            {
                ID = 14,
                SeasonId = 1920,
                TeamName = "Rock Island Independents",
                LeagueName = "American Professional Football Association"
            });
        }
    }
}
