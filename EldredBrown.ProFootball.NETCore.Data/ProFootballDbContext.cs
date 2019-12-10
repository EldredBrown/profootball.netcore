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
        /// Gets or sets the LeagueSeasons data source.
        /// </summary>
        public DbSet<LeagueSeason> LeagueSeasons { get; set; }

        /// <summary>
        /// Gets or sets the TeamSeasons data source.
        /// </summary>
        public DbSet<TeamSeason> TeamSeasons { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            SeedLeagues(modelBuilder);
            SeedTeams(modelBuilder);
            SeedSeasons(modelBuilder);
            SeedGames(modelBuilder);
            SeedLeagueSeasons(modelBuilder);
            SeedTeamSeasons(modelBuilder);
        }

        private void SeedLeagues(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<League>().HasData(new League
            {
                ID = 1,
                LongName = "American Professional Football Association",
                ShortName = "APFA",
                FirstSeasonId = 1920,
                LastSeasonId = 1921
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

            modelBuilder.Entity<Team>().HasData(new Team
            {
                ID = 15,
                Name = "Chicago Staleys"
            });

            modelBuilder.Entity<Team>().HasData(new Team
            {
                ID = 16,
                Name = "Cincinnati Celts"
            });

            modelBuilder.Entity<Team>().HasData(new Team
            {
                ID = 17,
                Name = "Cleveland Indians"
            });

            modelBuilder.Entity<Team>().HasData(new Team
            {
                ID = 18,
                Name = "Detroit Tigers"
            });

            modelBuilder.Entity<Team>().HasData(new Team
            {
                ID = 19,
                Name = "Evansville Crimson Giants"
            });

            modelBuilder.Entity<Team>().HasData(new Team
            {
                ID = 20,
                Name = "Green Bay Packers"
            });

            modelBuilder.Entity<Team>().HasData(new Team
            {
                ID = 21,
                Name = "Louisville Brocks"
            });

            modelBuilder.Entity<Team>().HasData(new Team
            {
                ID = 22,
                Name = "Minneapolis Marines"
            });

            modelBuilder.Entity<Team>().HasData(new Team
            {
                ID = 23,
                Name = "New York Brickley Giants"
            });

            modelBuilder.Entity<Team>().HasData(new Team
            {
                ID = 24,
                Name = "Tonawanda Kardex"
            });

            modelBuilder.Entity<Team>().HasData(new Team
            {
                ID = 25,
                Name = "Washington Senators"
            });
        }

        private void SeedSeasons(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Season>().HasData(new Season
            {
                ID = 1920,
                NumOfWeeks = 13
            });

            modelBuilder.Entity<Season>().HasData(new Season
            {
                ID = 1921,
                NumOfWeeks = 13
            });
        }

        private void SeedGames(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Game>().HasData(new Game
            {
                ID = 1,
                SeasonId = 1920,
                Week = 1,
                GuestName = "St. Paul Ideals",
                GuestScore = 0,
                HostName = "Rock Island Independents",
                HostScore = 48
            });

            modelBuilder.Entity<Game>().HasData(new Game
            {
                ID = 2,
                SeasonId = 1920,
                Week = 2,
                GuestName = "Pitcairn Quakers",
                GuestScore = 0,
                HostName = "Canton Bulldogs",
                HostScore = 48
            });

            modelBuilder.Entity<Game>().HasData(new Game
            {
                ID = 3,
                SeasonId = 1920,
                Week = 2,
                GuestName = "West Buffalo",
                GuestScore = 6,
                HostName = "Buffalo All-Americans",
                HostScore = 32
            });

            modelBuilder.Entity<Game>().HasData(new Game
            {
                ID = 4,
                SeasonId = 1920,
                Week = 2,
                GuestName = "Wheeling Stogies",
                GuestScore = 0,
                HostName = "Akron Pros",
                HostScore = 43
            });

            modelBuilder.Entity<Game>().HasData(new Game
            {
                ID = 5,
                SeasonId = 1920,
                Week = 2,
                GuestName = "Muncie Flyers",
                GuestScore = 0,
                HostName = "Rock Island Independents",
                HostScore = 45
            });

            modelBuilder.Entity<Game>().HasData(new Game
            {
                ID = 6,
                SeasonId = 1920,
                Week = 2,
                GuestName = "All Buffalo",
                GuestScore = 0,
                HostName = "Rochester Jeffersons",
                HostScore = 10
            });

            modelBuilder.Entity<Game>().HasData(new Game
            {
                ID = 7,
                SeasonId = 1920,
                Week = 2,
                GuestName = "Columbus Panhandles",
                GuestScore = 0,
                HostName = "Dayton Triangles",
                HostScore = 14
            });

            modelBuilder.Entity<Game>().HasData(new Game
            {
                ID = 8,
                SeasonId = 1920,
                Week = 2,
                GuestName = "Moline Universal Tractors",
                GuestScore = 0,
                HostName = "Decatur Staleys",
                HostScore = 20
            });
        }

        private void SeedLeagueSeasons(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LeagueSeason>().HasData(new LeagueSeason
            {
                ID = 1,
                LeagueName = "American Professional Football Association",
                SeasonId = 1920,
                TotalGames = 0,
                TotalPoints = 0
            });

            modelBuilder.Entity<LeagueSeason>().HasData(new LeagueSeason
            {
                ID = 2,
                LeagueName = "American Professional Football Association",
                SeasonId = 1921,
                TotalGames = 0,
                TotalPoints = 0
            });
        }

        private void SeedTeamSeasons(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TeamSeason>().HasData(new TeamSeason
            {
                ID = 1,
                TeamName = "Akron Pros",
                SeasonId = 1920,
                LeagueName = "American Professional Football Association"
            });

            modelBuilder.Entity<TeamSeason>().HasData(new TeamSeason
            {
                ID = 2,
                TeamName = "Buffalo All-Americans",
                SeasonId = 1920,
                LeagueName = "American Professional Football Association"
            });

            modelBuilder.Entity<TeamSeason>().HasData(new TeamSeason
            {
                ID = 3,
                TeamName = "Canton Bulldogs",
                SeasonId = 1920,
                LeagueName = "American Professional Football Association"
            });

            modelBuilder.Entity<TeamSeason>().HasData(new TeamSeason
            {
                ID = 4,
                TeamName = "Chicago Cardinals",
                SeasonId = 1920,
                LeagueName = "American Professional Football Association"
            });

            modelBuilder.Entity<TeamSeason>().HasData(new TeamSeason
            {
                ID = 5,
                TeamName = "Chicago Tigers",
                SeasonId = 1920,
                LeagueName = "American Professional Football Association"
            });

            modelBuilder.Entity<TeamSeason>().HasData(new TeamSeason
            {
                ID = 6,
                TeamName = "Cleveland Tigers",
                SeasonId = 1920,
                LeagueName = "American Professional Football Association"
            });

            modelBuilder.Entity<TeamSeason>().HasData(new TeamSeason
            {
                ID = 7,
                TeamName = "Columbus Panhandles",
                SeasonId = 1920,
                LeagueName = "American Professional Football Association"
            });

            modelBuilder.Entity<TeamSeason>().HasData(new TeamSeason
            {
                ID = 8,
                TeamName = "Dayton Triangles",
                SeasonId = 1920,
                LeagueName = "American Professional Football Association"
            });

            modelBuilder.Entity<TeamSeason>().HasData(new TeamSeason
            {
                ID = 9,
                TeamName = "Decatur Staleys",
                SeasonId = 1920,
                LeagueName = "American Professional Football Association"
            });

            modelBuilder.Entity<TeamSeason>().HasData(new TeamSeason
            {
                ID = 10,
                TeamName = "Detroit Heralds",
                SeasonId = 1920,
                LeagueName = "American Professional Football Association"
            });

            modelBuilder.Entity<TeamSeason>().HasData(new TeamSeason
            {
                ID = 11,
                TeamName = "Hammond Pros",
                SeasonId = 1920,
                LeagueName = "American Professional Football Association"
            });

            modelBuilder.Entity<TeamSeason>().HasData(new TeamSeason
            {
                ID = 12,
                TeamName = "Muncie Flyers",
                SeasonId = 1920,
                LeagueName = "American Professional Football Association"
            });

            modelBuilder.Entity<TeamSeason>().HasData(new TeamSeason
            {
                ID = 13,
                TeamName = "Rochester Jeffersons",
                SeasonId = 1920,
                LeagueName = "American Professional Football Association"
            });

            modelBuilder.Entity<TeamSeason>().HasData(new TeamSeason
            {
                ID = 14,
                TeamName = "Rock Island Independents",
                SeasonId = 1920,
                LeagueName = "American Professional Football Association"
            });

            modelBuilder.Entity<TeamSeason>().HasData(new TeamSeason
            {
                ID = 15,
                TeamName = "Akron Pros",
                SeasonId = 1921,
                LeagueName = "American Professional Football Association"
            });

            modelBuilder.Entity<TeamSeason>().HasData(new TeamSeason
            {
                ID = 16,
                TeamName = "Buffalo All-Americans",
                SeasonId = 1921,
                LeagueName = "American Professional Football Association"
            });

            modelBuilder.Entity<TeamSeason>().HasData(new TeamSeason
            {
                ID = 17,
                TeamName = "Canton Bulldogs",
                SeasonId = 1921,
                LeagueName = "American Professional Football Association"
            });

            modelBuilder.Entity<TeamSeason>().HasData(new TeamSeason
            {
                ID = 18,
                TeamName = "Chicago Cardinals",
                SeasonId = 1921,
                LeagueName = "American Professional Football Association"
            });

            modelBuilder.Entity<TeamSeason>().HasData(new TeamSeason
            {
                ID = 19,
                TeamName = "Chicago Staleys",
                SeasonId = 1921,
                LeagueName = "American Professional Football Association"
            });

            modelBuilder.Entity<TeamSeason>().HasData(new TeamSeason
            {
                ID = 20,
                TeamName = "Cincinnati Celts",
                SeasonId = 1921,
                LeagueName = "American Professional Football Association"
            });

            modelBuilder.Entity<TeamSeason>().HasData(new TeamSeason
            {
                ID = 21,
                TeamName = "Cleveland Indians",
                SeasonId = 1921,
                LeagueName = "American Professional Football Association"
            });

            modelBuilder.Entity<TeamSeason>().HasData(new TeamSeason
            {
                ID = 22,
                TeamName = "Columbus Panhandles",
                SeasonId = 1921,
                LeagueName = "American Professional Football Association"
            });

            modelBuilder.Entity<TeamSeason>().HasData(new TeamSeason
            {
                ID = 23,
                TeamName = "Dayton Triangles",
                SeasonId = 1921,
                LeagueName = "American Professional Football Association"
            });

            modelBuilder.Entity<TeamSeason>().HasData(new TeamSeason
            {
                ID = 24,
                TeamName = "Detroit Tigers",
                SeasonId = 1921,
                LeagueName = "American Professional Football Association"
            });

            modelBuilder.Entity<TeamSeason>().HasData(new TeamSeason
            {
                ID = 25,
                TeamName = "Evansville Crimson Giants",
                SeasonId = 1921,
                LeagueName = "American Professional Football Association"
            });

            modelBuilder.Entity<TeamSeason>().HasData(new TeamSeason
            {
                ID = 26,
                TeamName = "Green Bay Packers",
                SeasonId = 1921,
                LeagueName = "American Professional Football Association"
            });

            modelBuilder.Entity<TeamSeason>().HasData(new TeamSeason
            {
                ID = 27,
                TeamName = "Hammond Pros",
                SeasonId = 1921,
                LeagueName = "American Professional Football Association"
            });

            modelBuilder.Entity<TeamSeason>().HasData(new TeamSeason
            {
                ID = 28,
                TeamName = "Louisville Brocks",
                SeasonId = 1921,
                LeagueName = "American Professional Football Association"
            });

            modelBuilder.Entity<TeamSeason>().HasData(new TeamSeason
            {
                ID = 29,
                TeamName = "Minneapolis Marines",
                SeasonId = 1921,
                LeagueName = "American Professional Football Association"
            });

            modelBuilder.Entity<TeamSeason>().HasData(new TeamSeason
            {
                ID = 30,
                TeamName = "Muncie Flyers",
                SeasonId = 1921,
                LeagueName = "American Professional Football Association"
            });

            modelBuilder.Entity<TeamSeason>().HasData(new TeamSeason
            {
                ID = 31,
                TeamName = "New York Brickley Giants",
                SeasonId = 1921,
                LeagueName = "American Professional Football Association"
            });

            modelBuilder.Entity<TeamSeason>().HasData(new TeamSeason
            {
                ID = 32,
                TeamName = "Rochester Jeffersons",
                SeasonId = 1921,
                LeagueName = "American Professional Football Association"
            });

            modelBuilder.Entity<TeamSeason>().HasData(new TeamSeason
            {
                ID = 33,
                TeamName = "Rock Island Independents",
                SeasonId = 1921,
                LeagueName = "American Professional Football Association"
            });

            modelBuilder.Entity<TeamSeason>().HasData(new TeamSeason
            {
                ID = 34,
                TeamName = "Tonawanda Kardex",
                SeasonId = 1921,
                LeagueName = "American Professional Football Association"
            });

            modelBuilder.Entity<TeamSeason>().HasData(new TeamSeason
            {
                ID = 35,
                TeamName = "Washington Senators",
                SeasonId = 1921,
                LeagueName = "American Professional Football Association"
            });
        }
    }
}
