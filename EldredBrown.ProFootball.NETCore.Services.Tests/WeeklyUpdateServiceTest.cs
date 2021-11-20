using System.Collections.Generic;
using System.Threading.Tasks;
using FakeItEasy;
using Xunit;
using EldredBrown.ProFootball.NETCore.Data.Entities;
using EldredBrown.ProFootball.NETCore.Data.Repositories;

namespace EldredBrown.ProFootball.NETCore.Services.Tests
{
    public class WeeklyUpdateServiceTest
    {
        [Fact]
        public async Task RunWeeklyUpdate_WhenLeagueSeasonTotalsTotalGamesIsNull_ShouldNotUpdateLeagueSeasonGamesAndPoints()
        {
            // Arrange
            var seasonRepository = A.Fake<ISeasonRepository>();
            var gameRepository = A.Fake<IGameRepository>();
            var leagueSeasonRepository = A.Fake<ILeagueSeasonRepository>();
            var leagueSeasonTotalsRepository = A.Fake<ILeagueSeasonTotalsRepository>();
            var teamSeasonRepository = A.Fake<ITeamSeasonRepository>();
            var teamSeasonScheduleRepository = A.Fake<ITeamSeasonScheduleRepository>();
            var sharedRepository = A.Fake<ISharedRepository>();

            var service = new WeeklyUpdateService(seasonRepository, gameRepository, leagueSeasonRepository,
                leagueSeasonTotalsRepository, teamSeasonRepository, teamSeasonScheduleRepository, sharedRepository);

            var leagueSeasonTotals = new LeagueSeasonTotals
            {
                TotalGames = null,
                TotalPoints = null
            };
            A.CallTo(() => leagueSeasonTotalsRepository.GetLeagueSeasonTotals(A<string>.Ignored, A<int>.Ignored))
                .Returns(leagueSeasonTotals);

            A.CallTo(() => leagueSeasonRepository.GetLeagueSeasonByLeagueAndSeasonAsync(A<string>.Ignored, A<int>.Ignored))
                .Returns<LeagueSeason?>(null);

            var seasonYear = 1920;

            var games = new List<Game>
            {
                new Game
                {
                    SeasonYear = seasonYear,
                    Week = 0
                }
            };
            A.CallTo(() => gameRepository.GetGamesAsync()).Returns(games);

            A.CallTo(() => seasonRepository.GetSeasonAsync(A<int>.Ignored)).Returns<Season?>(null);

            // Act
            await service.RunWeeklyUpdate(seasonYear);

            // Assert
            var leagueName = "APFA";

            A.CallTo(() => leagueSeasonTotalsRepository.GetLeagueSeasonTotals(leagueName, seasonYear))
                .MustHaveHappened();
            A.CallTo(() => leagueSeasonRepository.GetLeagueSeasonByLeagueAndSeasonAsync(leagueName, seasonYear))
                .MustNotHaveHappened();
            A.CallTo(() => gameRepository.GetGamesAsync()).MustHaveHappened();
            A.CallTo(() => seasonRepository.GetSeasonAsync(seasonYear)).MustHaveHappened();
            A.CallTo(() => sharedRepository.SaveChangesAsync()).MustHaveHappenedOnceExactly();
            A.CallTo(() => teamSeasonRepository.GetTeamSeasonsBySeasonAsync(A<int>.Ignored)).MustNotHaveHappened();
        }

        [Fact]
        public async Task RunWeeklyUpdate_WhenLeagueSeasonTotalsTotalPointsIsNull_ShouldNotUpdateLeagueSeasonGamesAndPoints()
        {
            // Arrange
            var seasonRepository = A.Fake<ISeasonRepository>();
            var gameRepository = A.Fake<IGameRepository>();
            var leagueSeasonRepository = A.Fake<ILeagueSeasonRepository>();
            var leagueSeasonTotalsRepository = A.Fake<ILeagueSeasonTotalsRepository>();
            var teamSeasonRepository = A.Fake<ITeamSeasonRepository>();
            var teamSeasonScheduleRepository = A.Fake<ITeamSeasonScheduleRepository>();
            var sharedRepository = A.Fake<ISharedRepository>();

            var service = new WeeklyUpdateService(seasonRepository, gameRepository, leagueSeasonRepository,
                leagueSeasonTotalsRepository, teamSeasonRepository, teamSeasonScheduleRepository, sharedRepository);

            var leagueSeasonTotals = new LeagueSeasonTotals
            {
                TotalGames = 0,
                TotalPoints = null
            };
            A.CallTo(() => leagueSeasonTotalsRepository.GetLeagueSeasonTotals(A<string>.Ignored, A<int>.Ignored))
                .Returns(leagueSeasonTotals);

            A.CallTo(() => leagueSeasonRepository.GetLeagueSeasonByLeagueAndSeasonAsync(A<string>.Ignored, A<int>.Ignored))
                .Returns<LeagueSeason?>(null);

            var seasonYear = 1920;

            var games = new List<Game>
            {
                new Game
                {
                    SeasonYear = seasonYear,
                    Week = 0
                }
            };
            A.CallTo(() => gameRepository.GetGamesAsync()).Returns(games);

            A.CallTo(() => seasonRepository.GetSeasonAsync(A<int>.Ignored)).Returns<Season?>(null);

            // Act
            await service.RunWeeklyUpdate(seasonYear);

            // Assert
            var leagueName = "APFA";

            A.CallTo(() => leagueSeasonTotalsRepository.GetLeagueSeasonTotals(leagueName, seasonYear))
                .MustHaveHappened();
            A.CallTo(() => leagueSeasonRepository.GetLeagueSeasonByLeagueAndSeasonAsync(leagueName, seasonYear))
                .MustNotHaveHappened();
            A.CallTo(() => gameRepository.GetGamesAsync()).MustHaveHappened();
            A.CallTo(() => seasonRepository.GetSeasonAsync(seasonYear)).MustHaveHappened();
            A.CallTo(() => sharedRepository.SaveChangesAsync()).MustHaveHappenedOnceExactly();
            A.CallTo(() => teamSeasonRepository.GetTeamSeasonsBySeasonAsync(A<int>.Ignored)).MustNotHaveHappened();
        }

        [Fact]
        public async Task RunWeeklyUpdate_WhenLeagueSeasonIsNull_ShouldNotUpdateLeagueSeasonGamesAndPoints()
        {
            // Arrange
            var seasonRepository = A.Fake<ISeasonRepository>();
            var gameRepository = A.Fake<IGameRepository>();
            var leagueSeasonRepository = A.Fake<ILeagueSeasonRepository>();
            var leagueSeasonTotalsRepository = A.Fake<ILeagueSeasonTotalsRepository>();
            var teamSeasonRepository = A.Fake<ITeamSeasonRepository>();
            var teamSeasonScheduleRepository = A.Fake<ITeamSeasonScheduleRepository>();
            var sharedRepository = A.Fake<ISharedRepository>();

            var service = new WeeklyUpdateService(seasonRepository, gameRepository, leagueSeasonRepository,
                leagueSeasonTotalsRepository, teamSeasonRepository, teamSeasonScheduleRepository, sharedRepository);

            var leagueSeasonTotals = new LeagueSeasonTotals
            {
                TotalGames = 0,
                TotalPoints = 0
            };
            A.CallTo(() => leagueSeasonTotalsRepository.GetLeagueSeasonTotals(A<string>.Ignored, A<int>.Ignored))
                .Returns(leagueSeasonTotals);

            A.CallTo(() => leagueSeasonRepository.GetLeagueSeasonByLeagueAndSeasonAsync(A<string>.Ignored, A<int>.Ignored))
                .Returns<LeagueSeason?>(null);

            var seasonYear = 1920;

            var games = new List<Game>
            {
                new Game
                {
                    SeasonYear = seasonYear,
                    Week = 0
                }
            };
            A.CallTo(() => gameRepository.GetGamesAsync()).Returns(games);

            A.CallTo(() => seasonRepository.GetSeasonAsync(A<int>.Ignored)).Returns<Season?>(null);

            // Act
            await service.RunWeeklyUpdate(seasonYear);

            // Assert
            var leagueName = "APFA";

            A.CallTo(() => leagueSeasonTotalsRepository.GetLeagueSeasonTotals(leagueName, seasonYear))
                .MustHaveHappened();
            A.CallTo(() => leagueSeasonRepository.GetLeagueSeasonByLeagueAndSeasonAsync(leagueName, seasonYear))
                .MustHaveHappened();
            A.CallTo(() => gameRepository.GetGamesAsync()).MustHaveHappened();
            A.CallTo(() => seasonRepository.GetSeasonAsync(seasonYear)).MustHaveHappened();
            A.CallTo(() => sharedRepository.SaveChangesAsync()).MustHaveHappenedOnceExactly();
            A.CallTo(() => teamSeasonRepository.GetTeamSeasonsBySeasonAsync(A<int>.Ignored)).MustNotHaveHappened();
        }

        [Fact]
        public async Task RunWeeklyUpdate_WhenLeagueSeasonTotalsTotalGamesAndTotalPointsAreNotNull_ShouldUpdateLeagueSeasonGamesAndPoints()
        {
            // Arrange
            var seasonRepository = A.Fake<ISeasonRepository>();
            var gameRepository = A.Fake<IGameRepository>();
            var leagueSeasonRepository = A.Fake<ILeagueSeasonRepository>();
            var leagueSeasonTotalsRepository = A.Fake<ILeagueSeasonTotalsRepository>();
            var teamSeasonRepository = A.Fake<ITeamSeasonRepository>();
            var teamSeasonScheduleRepository = A.Fake<ITeamSeasonScheduleRepository>();
            var sharedRepository = A.Fake<ISharedRepository>();

            var service = new WeeklyUpdateService(seasonRepository, gameRepository, leagueSeasonRepository,
                leagueSeasonTotalsRepository, teamSeasonRepository, teamSeasonScheduleRepository, sharedRepository);

            var leagueSeasonTotals = new LeagueSeasonTotals
            {
                TotalGames = 0,
                TotalPoints = 0
            };
            A.CallTo(() => leagueSeasonTotalsRepository.GetLeagueSeasonTotals(A<string>.Ignored, A<int>.Ignored))
                .Returns(leagueSeasonTotals);

            var leagueSeason = new LeagueSeason();
            A.CallTo(() => leagueSeasonRepository.GetLeagueSeasonByLeagueAndSeasonAsync(A<string>.Ignored, A<int>.Ignored))
                .Returns(leagueSeason);

            var seasonYear = 1920;

            var games = new List<Game>
            {
                new Game
                {
                    SeasonYear = seasonYear,
                    Week = 0
                }
            };
            A.CallTo(() => gameRepository.GetGamesAsync()).Returns(games);

            A.CallTo(() => seasonRepository.GetSeasonAsync(A<int>.Ignored)).Returns<Season?>(null);

            // Act
            await service.RunWeeklyUpdate(seasonYear);

            // Assert
            var leagueName = "APFA";

            A.CallTo(() => leagueSeasonTotalsRepository.GetLeagueSeasonTotals(leagueName, seasonYear))
                .MustHaveHappened();
            A.CallTo(() => leagueSeasonRepository.GetLeagueSeasonByLeagueAndSeasonAsync(leagueName, seasonYear))
                .MustHaveHappened();
            A.CallTo(() => gameRepository.GetGamesAsync()).MustHaveHappened();
            A.CallTo(() => seasonRepository.GetSeasonAsync(seasonYear)).MustHaveHappened();
            A.CallTo(() => sharedRepository.SaveChangesAsync()).MustHaveHappenedOnceExactly();
            A.CallTo(() => teamSeasonRepository.GetTeamSeasonsBySeasonAsync(A<int>.Ignored)).MustNotHaveHappened();
        }

        [Fact]
        public async Task RunWeeklyUpdate_WhenWeekCountLessThanThree_ShouldNotUpdateRankings()
        {
            // Arrange
            var seasonRepository = A.Fake<ISeasonRepository>();
            var gameRepository = A.Fake<IGameRepository>();
            var leagueSeasonRepository = A.Fake<ILeagueSeasonRepository>();
            var leagueSeasonTotalsRepository = A.Fake<ILeagueSeasonTotalsRepository>();
            var teamSeasonRepository = A.Fake<ITeamSeasonRepository>();
            var teamSeasonScheduleRepository = A.Fake<ITeamSeasonScheduleRepository>();
            var sharedRepository = A.Fake<ISharedRepository>();

            var service = new WeeklyUpdateService(seasonRepository, gameRepository, leagueSeasonRepository,
                leagueSeasonTotalsRepository, teamSeasonRepository, teamSeasonScheduleRepository, sharedRepository);

            var leagueSeasonTotals = new LeagueSeasonTotals
            {
                TotalGames = 0,
                TotalPoints = 0
            };
            A.CallTo(() => leagueSeasonTotalsRepository.GetLeagueSeasonTotals(A<string>.Ignored, A<int>.Ignored))
                .Returns(leagueSeasonTotals);

            var leagueSeason = new LeagueSeason();
            A.CallTo(() => leagueSeasonRepository.GetLeagueSeasonByLeagueAndSeasonAsync(A<string>.Ignored, A<int>.Ignored))
                .Returns(leagueSeason);

            var seasonYear = 1920;

            var games = new List<Game>
            {
                new Game
                {
                    SeasonYear = seasonYear,
                    Week = 2
                }
            };
            A.CallTo(() => gameRepository.GetGamesAsync()).Returns(games);

            A.CallTo(() => seasonRepository.GetSeasonAsync(A<int>.Ignored)).Returns<Season?>(null);

            // Act
            await service.RunWeeklyUpdate(seasonYear);

            // Assert
            var leagueName = "APFA";

            A.CallTo(() => leagueSeasonTotalsRepository.GetLeagueSeasonTotals(leagueName, seasonYear))
                .MustHaveHappened();
            A.CallTo(() => leagueSeasonRepository.GetLeagueSeasonByLeagueAndSeasonAsync(leagueName, seasonYear))
                .MustHaveHappened();
            A.CallTo(() => gameRepository.GetGamesAsync()).MustHaveHappened();
            A.CallTo(() => seasonRepository.GetSeasonAsync(seasonYear)).MustHaveHappened();
            A.CallTo(() => sharedRepository.SaveChangesAsync()).MustHaveHappenedOnceExactly();
            A.CallTo(() => teamSeasonRepository.GetTeamSeasonsBySeasonAsync(A<int>.Ignored)).MustNotHaveHappened();
            A.CallTo(() => teamSeasonScheduleRepository.GetTeamSeasonScheduleTotalsAsync(A<string>.Ignored,
                A<int>.Ignored)).MustNotHaveHappened();
            A.CallTo(() => teamSeasonScheduleRepository.GetTeamSeasonScheduleAveragesAsync(A<string>.Ignored,
                A<int>.Ignored)).MustNotHaveHappened();
        }

        [Fact]
        public async Task RunWeeklyUpdate_WhenTeamSeasonScheduleTotalsScheduleGamesIsNull_ShouldNotUpdateRankingsForTeamSeason()
        {
            // Arrange
            var seasonRepository = A.Fake<ISeasonRepository>();
            var gameRepository = A.Fake<IGameRepository>();
            var leagueSeasonRepository = A.Fake<ILeagueSeasonRepository>();
            var leagueSeasonTotalsRepository = A.Fake<ILeagueSeasonTotalsRepository>();
            var teamSeasonRepository = A.Fake<ITeamSeasonRepository>();
            var teamSeasonScheduleRepository = A.Fake<ITeamSeasonScheduleRepository>();
            var sharedRepository = A.Fake<ISharedRepository>();

            var service = new WeeklyUpdateService(seasonRepository, gameRepository, leagueSeasonRepository,
                leagueSeasonTotalsRepository, teamSeasonRepository, teamSeasonScheduleRepository, sharedRepository);

            var leagueSeasonTotals = new LeagueSeasonTotals
            {
                TotalGames = 0,
                TotalPoints = 0
            };
            A.CallTo(() => leagueSeasonTotalsRepository.GetLeagueSeasonTotals(A<string>.Ignored, A<int>.Ignored))
                .Returns(leagueSeasonTotals);

            var leagueSeason = new LeagueSeason
            {
                AveragePoints = null
            };
            A.CallTo(() => leagueSeasonRepository.GetLeagueSeasonByLeagueAndSeasonAsync(A<string>.Ignored, A<int>.Ignored))
                .Returns(leagueSeason);

            var seasonYear = 1920;

            var games = new List<Game>
            {
                new Game
                {
                    SeasonYear = seasonYear,
                    Week = 3
                }
            };
            A.CallTo(() => gameRepository.GetGamesAsync()).Returns(games);

            A.CallTo(() => seasonRepository.GetSeasonAsync(A<int>.Ignored)).Returns<Season?>(null);

            var teamName = "Team";
            var leagueName = "APFA";

            var teamSeasons = new List<TeamSeason>
            {
                new TeamSeason
                {
                    TeamName = teamName,
                    SeasonYear = seasonYear,
                    LeagueName = leagueName
                }
            };
            A.CallTo(() => teamSeasonRepository.GetTeamSeasonsBySeasonAsync(seasonYear)).Returns(teamSeasons);

            var teamSeasonScheduleTotals = new TeamSeasonScheduleTotals
            {
                ScheduleGames = null
            };
            A.CallTo(() => teamSeasonScheduleRepository.GetTeamSeasonScheduleTotalsAsync(A<string>.Ignored,
                seasonYear)).Returns(teamSeasonScheduleTotals);

            var teamSeasonScheduleAverages = new TeamSeasonScheduleAverages
            {
                PointsFor = null,
                PointsAgainst = null
            };
            A.CallTo(() => teamSeasonScheduleRepository.GetTeamSeasonScheduleAveragesAsync(A<string>.Ignored,
                seasonYear)).Returns<TeamSeasonScheduleAverages>(teamSeasonScheduleAverages);

            // Act
            await service.RunWeeklyUpdate(seasonYear);

            // Assert
            A.CallTo(() => leagueSeasonTotalsRepository.GetLeagueSeasonTotals(leagueName, seasonYear))
                .MustHaveHappened();
            A.CallTo(() => leagueSeasonRepository.GetLeagueSeasonByLeagueAndSeasonAsync(leagueName, seasonYear))
                .MustHaveHappenedOnceExactly();
            A.CallTo(() => gameRepository.GetGamesAsync()).MustHaveHappened();
            A.CallTo(() => seasonRepository.GetSeasonAsync(seasonYear)).MustHaveHappened();
            A.CallTo(() => teamSeasonRepository.GetTeamSeasonsBySeasonAsync(seasonYear)).MustHaveHappened();
            A.CallTo(() => teamSeasonScheduleRepository.GetTeamSeasonScheduleTotalsAsync(teamName, seasonYear))
                .MustHaveHappened();
            A.CallTo(() => teamSeasonScheduleRepository.GetTeamSeasonScheduleAveragesAsync(teamName, seasonYear))
                .MustNotHaveHappened();
            A.CallTo(() => sharedRepository.SaveChangesAsync()).MustHaveHappenedTwiceExactly();
        }

        [Fact]
        public async Task RunWeeklyUpdate_WhenTeamSeasonScheduleAveragesPointsForIsNull_ShouldNotUpdateRankingsForTeamSeason()
        {
            // Arrange
            var seasonRepository = A.Fake<ISeasonRepository>();
            var gameRepository = A.Fake<IGameRepository>();
            var leagueSeasonRepository = A.Fake<ILeagueSeasonRepository>();
            var leagueSeasonTotalsRepository = A.Fake<ILeagueSeasonTotalsRepository>();
            var teamSeasonRepository = A.Fake<ITeamSeasonRepository>();
            var teamSeasonScheduleRepository = A.Fake<ITeamSeasonScheduleRepository>();
            var sharedRepository = A.Fake<ISharedRepository>();

            var service = new WeeklyUpdateService(seasonRepository, gameRepository, leagueSeasonRepository,
                leagueSeasonTotalsRepository, teamSeasonRepository, teamSeasonScheduleRepository, sharedRepository);

            var leagueSeasonTotals = new LeagueSeasonTotals
            {
                TotalGames = 0,
                TotalPoints = 0
            };
            A.CallTo(() => leagueSeasonTotalsRepository.GetLeagueSeasonTotals(A<string>.Ignored, A<int>.Ignored))
                .Returns(leagueSeasonTotals);

            var leagueSeason = new LeagueSeason
            {
                AveragePoints = null
            };
            A.CallTo(() => leagueSeasonRepository.GetLeagueSeasonByLeagueAndSeasonAsync(A<string>.Ignored, A<int>.Ignored))
                .Returns(leagueSeason);

            var seasonYear = 1920;

            var games = new List<Game>
            {
                new Game
                {
                    SeasonYear = seasonYear,
                    Week = 3
                }
            };
            A.CallTo(() => gameRepository.GetGamesAsync()).Returns(games);

            A.CallTo(() => seasonRepository.GetSeasonAsync(A<int>.Ignored)).Returns<Season?>(null);

            var teamName = "Team";
            var leagueName = "APFA";

            var teamSeasons = new List<TeamSeason>
            {
                new TeamSeason
                {
                    TeamName = teamName,
                    SeasonYear = seasonYear,
                    LeagueName = leagueName
                }
            };
            A.CallTo(() => teamSeasonRepository.GetTeamSeasonsBySeasonAsync(seasonYear)).Returns(teamSeasons);

            var teamSeasonScheduleTotals = new TeamSeasonScheduleTotals
            {
                ScheduleGames = 0
            };
            A.CallTo(() => teamSeasonScheduleRepository.GetTeamSeasonScheduleTotalsAsync(A<string>.Ignored,
                seasonYear)).Returns(teamSeasonScheduleTotals);

            var teamSeasonScheduleAverages = new TeamSeasonScheduleAverages
            {
                PointsFor = null,
                PointsAgainst = null
            };
            A.CallTo(() => teamSeasonScheduleRepository.GetTeamSeasonScheduleAveragesAsync(A<string>.Ignored,
                seasonYear)).Returns(teamSeasonScheduleAverages);

            // Act
            await service.RunWeeklyUpdate(seasonYear);

            // Assert
            A.CallTo(() => leagueSeasonTotalsRepository.GetLeagueSeasonTotals(leagueName, seasonYear))
                .MustHaveHappened();
            A.CallTo(() => leagueSeasonRepository.GetLeagueSeasonByLeagueAndSeasonAsync(leagueName, seasonYear))
                .MustHaveHappenedOnceExactly();
            A.CallTo(() => gameRepository.GetGamesAsync()).MustHaveHappened();
            A.CallTo(() => seasonRepository.GetSeasonAsync(seasonYear)).MustHaveHappened();
            A.CallTo(() => teamSeasonRepository.GetTeamSeasonsBySeasonAsync(seasonYear)).MustHaveHappened();
            A.CallTo(() => teamSeasonScheduleRepository.GetTeamSeasonScheduleTotalsAsync(teamName, seasonYear))
                .MustHaveHappened();
            A.CallTo(() => teamSeasonScheduleRepository.GetTeamSeasonScheduleAveragesAsync(teamName, seasonYear))
                .MustHaveHappened();
            A.CallTo(() => sharedRepository.SaveChangesAsync()).MustHaveHappenedTwiceExactly();
        }

        [Fact]
        public async Task RunWeeklyUpdate_WhenTeamSeasonScheduleAveragesPointsAgainstIsNull_ShouldNotUpdateRankingsForTeamSeason()
        {
            // Arrange
            var seasonRepository = A.Fake<ISeasonRepository>();
            var gameRepository = A.Fake<IGameRepository>();
            var leagueSeasonRepository = A.Fake<ILeagueSeasonRepository>();
            var leagueSeasonTotalsRepository = A.Fake<ILeagueSeasonTotalsRepository>();
            var teamSeasonRepository = A.Fake<ITeamSeasonRepository>();
            var teamSeasonScheduleRepository = A.Fake<ITeamSeasonScheduleRepository>();
            var sharedRepository = A.Fake<ISharedRepository>();

            var service = new WeeklyUpdateService(seasonRepository, gameRepository, leagueSeasonRepository,
                leagueSeasonTotalsRepository, teamSeasonRepository, teamSeasonScheduleRepository, sharedRepository);

            var leagueSeasonTotals = new LeagueSeasonTotals
            {
                TotalGames = 0,
                TotalPoints = 0
            };
            A.CallTo(() => leagueSeasonTotalsRepository.GetLeagueSeasonTotals(A<string>.Ignored, A<int>.Ignored))
                .Returns(leagueSeasonTotals);

            var leagueSeason = new LeagueSeason
            {
                AveragePoints = null
            };
            A.CallTo(() => leagueSeasonRepository.GetLeagueSeasonByLeagueAndSeasonAsync(A<string>.Ignored, A<int>.Ignored))
                .Returns(leagueSeason);

            var seasonYear = 1920;

            var games = new List<Game>
            {
                new Game
                {
                    SeasonYear = seasonYear,
                    Week = 3
                }
            };
            A.CallTo(() => gameRepository.GetGamesAsync()).Returns(games);

            A.CallTo(() => seasonRepository.GetSeasonAsync(A<int>.Ignored)).Returns<Season?>(null);

            var teamName = "Team";
            var leagueName = "APFA";

            var teamSeasons = new List<TeamSeason>
            {
                new TeamSeason
                {
                    TeamName = teamName,
                    SeasonYear = seasonYear,
                    LeagueName = leagueName
                }
            };
            A.CallTo(() => teamSeasonRepository.GetTeamSeasonsBySeasonAsync(seasonYear)).Returns(teamSeasons);

            var teamSeasonScheduleTotals = new TeamSeasonScheduleTotals
            {
                ScheduleGames = 0
            };
            A.CallTo(() => teamSeasonScheduleRepository.GetTeamSeasonScheduleTotalsAsync(A<string>.Ignored,
                seasonYear)).Returns(teamSeasonScheduleTotals);

            var teamSeasonScheduleAverages = new TeamSeasonScheduleAverages
            {
                PointsFor = 0,
                PointsAgainst = null
            };
            A.CallTo(() => teamSeasonScheduleRepository.GetTeamSeasonScheduleAveragesAsync(A<string>.Ignored,
                seasonYear)).Returns(teamSeasonScheduleAverages);

            // Act
            await service.RunWeeklyUpdate(seasonYear);

            // Assert
            A.CallTo(() => leagueSeasonTotalsRepository.GetLeagueSeasonTotals(leagueName, seasonYear))
                .MustHaveHappened();
            A.CallTo(() => leagueSeasonRepository.GetLeagueSeasonByLeagueAndSeasonAsync(leagueName, seasonYear))
                .MustHaveHappenedOnceExactly();
            A.CallTo(() => gameRepository.GetGamesAsync()).MustHaveHappened();
            A.CallTo(() => seasonRepository.GetSeasonAsync(seasonYear)).MustHaveHappened();
            A.CallTo(() => teamSeasonRepository.GetTeamSeasonsBySeasonAsync(seasonYear)).MustHaveHappened();
            A.CallTo(() => teamSeasonScheduleRepository.GetTeamSeasonScheduleTotalsAsync(teamName, seasonYear))
                .MustHaveHappened();
            A.CallTo(() => teamSeasonScheduleRepository.GetTeamSeasonScheduleAveragesAsync(teamName, seasonYear))
                .MustHaveHappened();
            A.CallTo(() => sharedRepository.SaveChangesAsync()).MustHaveHappenedTwiceExactly();
        }

        [Fact]
        public async Task RunWeeklyUpdate_WhenLeagueSeasonIsNull_ShouldNotUpdateRankingsForTeamSeason()
        {
            // Arrange
            var seasonRepository = A.Fake<ISeasonRepository>();
            var gameRepository = A.Fake<IGameRepository>();
            var leagueSeasonRepository = A.Fake<ILeagueSeasonRepository>();
            var leagueSeasonTotalsRepository = A.Fake<ILeagueSeasonTotalsRepository>();
            var teamSeasonRepository = A.Fake<ITeamSeasonRepository>();
            var teamSeasonScheduleRepository = A.Fake<ITeamSeasonScheduleRepository>();
            var sharedRepository = A.Fake<ISharedRepository>();

            var service = new WeeklyUpdateService(seasonRepository, gameRepository, leagueSeasonRepository,
                leagueSeasonTotalsRepository, teamSeasonRepository, teamSeasonScheduleRepository, sharedRepository);

            var leagueSeasonTotals = new LeagueSeasonTotals
            {
                TotalGames = 0,
                TotalPoints = 0
            };
            A.CallTo(() => leagueSeasonTotalsRepository.GetLeagueSeasonTotals(A<string>.Ignored, A<int>.Ignored))
                .Returns(leagueSeasonTotals);

            A.CallTo(() => leagueSeasonRepository.GetLeagueSeasonByLeagueAndSeasonAsync(A<string>.Ignored, A<int>.Ignored))
                .Returns<LeagueSeason?>(null);

            var seasonYear = 1920;

            var games = new List<Game>
            {
                new Game
                {
                    SeasonYear = seasonYear,
                    Week = 3
                }
            };
            A.CallTo(() => gameRepository.GetGamesAsync()).Returns(games);

            A.CallTo(() => seasonRepository.GetSeasonAsync(A<int>.Ignored)).Returns<Season?>(null);

            var teamName = "Team";
            var leagueName = "APFA";

            var teamSeasons = new List<TeamSeason>
            {
                new TeamSeason
                {
                    TeamName = teamName,
                    SeasonYear = seasonYear,
                    LeagueName = leagueName
                }
            };
            A.CallTo(() => teamSeasonRepository.GetTeamSeasonsBySeasonAsync(seasonYear)).Returns(teamSeasons);

            var teamSeasonScheduleTotals = new TeamSeasonScheduleTotals
            {
                ScheduleGames = 0
            };
            A.CallTo(() => teamSeasonScheduleRepository.GetTeamSeasonScheduleTotalsAsync(A<string>.Ignored,
                seasonYear)).Returns(teamSeasonScheduleTotals);

            var teamSeasonScheduleAverages = new TeamSeasonScheduleAverages
            {
                PointsFor = 0,
                PointsAgainst = 0
            };
            A.CallTo(() => teamSeasonScheduleRepository.GetTeamSeasonScheduleAveragesAsync(A<string>.Ignored,
                seasonYear)).Returns(teamSeasonScheduleAverages);

            // Act
            await service.RunWeeklyUpdate(seasonYear);

            // Assert
            A.CallTo(() => leagueSeasonTotalsRepository.GetLeagueSeasonTotals(leagueName, seasonYear))
                .MustHaveHappened();
            A.CallTo(() => leagueSeasonRepository.GetLeagueSeasonByLeagueAndSeasonAsync(leagueName, seasonYear))
                .MustHaveHappenedTwiceExactly();
            A.CallTo(() => gameRepository.GetGamesAsync()).MustHaveHappened();
            A.CallTo(() => seasonRepository.GetSeasonAsync(seasonYear)).MustHaveHappened();
            A.CallTo(() => teamSeasonRepository.GetTeamSeasonsBySeasonAsync(seasonYear)).MustHaveHappened();
            A.CallTo(() => teamSeasonScheduleRepository.GetTeamSeasonScheduleTotalsAsync(teamName, seasonYear))
                .MustHaveHappened();
            A.CallTo(() => teamSeasonScheduleRepository.GetTeamSeasonScheduleAveragesAsync(teamName, seasonYear))
                .MustHaveHappened();
            A.CallTo(() => sharedRepository.SaveChangesAsync()).MustHaveHappenedTwiceExactly();
        }

        [Fact]
        public async Task RunWeeklyUpdate_WhenLeagueSeasonAveragePointsIsNull_ShouldNotUpdateRankingsForTeamSeason()
        {
            // Arrange
            var seasonRepository = A.Fake<ISeasonRepository>();
            var gameRepository = A.Fake<IGameRepository>();
            var leagueSeasonRepository = A.Fake<ILeagueSeasonRepository>();
            var leagueSeasonTotalsRepository = A.Fake<ILeagueSeasonTotalsRepository>();
            var teamSeasonRepository = A.Fake<ITeamSeasonRepository>();
            var teamSeasonScheduleRepository = A.Fake<ITeamSeasonScheduleRepository>();
            var sharedRepository = A.Fake<ISharedRepository>();

            var service = new WeeklyUpdateService(seasonRepository, gameRepository, leagueSeasonRepository,
                leagueSeasonTotalsRepository, teamSeasonRepository, teamSeasonScheduleRepository, sharedRepository);

            var leagueSeasonTotals = new LeagueSeasonTotals
            {
                TotalGames = 0,
                TotalPoints = 0
            };
            A.CallTo(() => leagueSeasonTotalsRepository.GetLeagueSeasonTotals(A<string>.Ignored, A<int>.Ignored))
                .Returns(leagueSeasonTotals);

            var leagueSeason = new LeagueSeason
            {
                AveragePoints = null
            };
            A.CallTo(() => leagueSeasonRepository.GetLeagueSeasonByLeagueAndSeasonAsync(A<string>.Ignored, A<int>.Ignored))
                .Returns(leagueSeason);

            var seasonYear = 1920;

            var games = new List<Game>
            {
                new Game
                {
                    SeasonYear = seasonYear,
                    Week = 3
                }
            };
            A.CallTo(() => gameRepository.GetGamesAsync()).Returns(games);

            A.CallTo(() => seasonRepository.GetSeasonAsync(A<int>.Ignored)).Returns<Season?>(null);

            var teamName = "Team";
            var leagueName = "APFA";

            var teamSeasons = new List<TeamSeason>
            {
                new TeamSeason
                {
                    TeamName = teamName,
                    SeasonYear = seasonYear,
                    LeagueName = leagueName
                }
            };
            A.CallTo(() => teamSeasonRepository.GetTeamSeasonsBySeasonAsync(seasonYear)).Returns(teamSeasons);

            var teamSeasonScheduleTotals = new TeamSeasonScheduleTotals
            {
                ScheduleGames = 0
            };
            A.CallTo(() => teamSeasonScheduleRepository.GetTeamSeasonScheduleTotalsAsync(A<string>.Ignored,
                seasonYear)).Returns(teamSeasonScheduleTotals);

            var teamSeasonScheduleAverages = new TeamSeasonScheduleAverages
            {
                PointsFor = 0,
                PointsAgainst = 0
            };
            A.CallTo(() => teamSeasonScheduleRepository.GetTeamSeasonScheduleAveragesAsync(A<string>.Ignored,
                seasonYear)).Returns(teamSeasonScheduleAverages);

            // Act
            await service.RunWeeklyUpdate(seasonYear);

            // Assert
            A.CallTo(() => leagueSeasonTotalsRepository.GetLeagueSeasonTotals(leagueName, seasonYear))
                .MustHaveHappened();
            A.CallTo(() => leagueSeasonRepository.GetLeagueSeasonByLeagueAndSeasonAsync(leagueName, seasonYear))
                .MustHaveHappenedTwiceExactly();
            A.CallTo(() => gameRepository.GetGamesAsync()).MustHaveHappened();
            A.CallTo(() => seasonRepository.GetSeasonAsync(seasonYear)).MustHaveHappened();
            A.CallTo(() => teamSeasonRepository.GetTeamSeasonsBySeasonAsync(seasonYear)).MustHaveHappened();
            A.CallTo(() => teamSeasonScheduleRepository.GetTeamSeasonScheduleTotalsAsync(teamName, seasonYear))
                .MustHaveHappened();
            A.CallTo(() => teamSeasonScheduleRepository.GetTeamSeasonScheduleAveragesAsync(teamName, seasonYear))
                .MustHaveHappened();
            A.CallTo(() => sharedRepository.SaveChangesAsync()).MustHaveHappenedTwiceExactly();
        }

        [Fact]
        public async Task RunWeeklyUpdate_WhenTeamSeasonScheduleTotalsScheduleGamesIsNotNullAndWhenTeamSeasonScheduleAveragesPointsForAndPointsAgainstAreNotNullAndLeagueSeasonAveragePointsIsNotNull_ShouldUpdateRankingsForTeamSeason()
        {
            // Arrange
            var seasonRepository = A.Fake<ISeasonRepository>();
            var gameRepository = A.Fake<IGameRepository>();
            var leagueSeasonRepository = A.Fake<ILeagueSeasonRepository>();
            var leagueSeasonTotalsRepository = A.Fake<ILeagueSeasonTotalsRepository>();
            var teamSeasonRepository = A.Fake<ITeamSeasonRepository>();
            var teamSeasonScheduleRepository = A.Fake<ITeamSeasonScheduleRepository>();
            var sharedRepository = A.Fake<ISharedRepository>();

            var service = new WeeklyUpdateService(seasonRepository, gameRepository, leagueSeasonRepository,
                leagueSeasonTotalsRepository, teamSeasonRepository, teamSeasonScheduleRepository, sharedRepository);

            var leagueSeasonTotals = new LeagueSeasonTotals
            {
                TotalGames = 1,
                TotalPoints = 0
            };
            A.CallTo(() => leagueSeasonTotalsRepository.GetLeagueSeasonTotals(A<string>.Ignored, A<int>.Ignored))
                .Returns(leagueSeasonTotals);

            var leagueSeason = new LeagueSeason
            {
                AveragePoints = 0
            };
            A.CallTo(() => leagueSeasonRepository.GetLeagueSeasonByLeagueAndSeasonAsync(A<string>.Ignored, A<int>.Ignored))
                .Returns(leagueSeason);

            var seasonYear = 1920;

            var games = new List<Game>
            {
                new Game
                {
                    SeasonYear = seasonYear,
                    Week = 3
                }
            };
            A.CallTo(() => gameRepository.GetGamesAsync()).Returns(games);

            A.CallTo(() => seasonRepository.GetSeasonAsync(A<int>.Ignored)).Returns<Season?>(null);

            var teamName = "Team";
            var leagueName = "APFA";

            var teamSeasons = new List<TeamSeason>
            {
                new TeamSeason
                {
                    TeamName = teamName,
                    SeasonYear = seasonYear,
                    LeagueName = leagueName
                }
            };
            A.CallTo(() => teamSeasonRepository.GetTeamSeasonsBySeasonAsync(seasonYear)).Returns(teamSeasons);

            var teamSeasonScheduleTotals = new TeamSeasonScheduleTotals
            {
                ScheduleGames = 0
            };
            A.CallTo(() => teamSeasonScheduleRepository.GetTeamSeasonScheduleTotalsAsync(A<string>.Ignored,
                seasonYear)).Returns(teamSeasonScheduleTotals);

            var teamSeasonScheduleAverages = new TeamSeasonScheduleAverages
            {
                PointsFor = 0,
                PointsAgainst = 0
            };
            A.CallTo(() => teamSeasonScheduleRepository.GetTeamSeasonScheduleAveragesAsync(A<string>.Ignored,
                seasonYear)).Returns(teamSeasonScheduleAverages);

            // Act
            await service.RunWeeklyUpdate(seasonYear);

            // Assert
            A.CallTo(() => leagueSeasonTotalsRepository.GetLeagueSeasonTotals(leagueName, seasonYear))
                .MustHaveHappened();
            A.CallTo(() => leagueSeasonRepository.GetLeagueSeasonByLeagueAndSeasonAsync(leagueName, seasonYear))
                .MustHaveHappenedTwiceExactly();
            A.CallTo(() => gameRepository.GetGamesAsync()).MustHaveHappened();
            A.CallTo(() => seasonRepository.GetSeasonAsync(seasonYear)).MustHaveHappened();
            A.CallTo(() => teamSeasonRepository.GetTeamSeasonsBySeasonAsync(seasonYear)).MustHaveHappened();
            A.CallTo(() => teamSeasonScheduleRepository.GetTeamSeasonScheduleTotalsAsync(teamName, seasonYear))
                .MustHaveHappened();
            A.CallTo(() => teamSeasonScheduleRepository.GetTeamSeasonScheduleAveragesAsync(teamName, seasonYear))
                .MustHaveHappened();
            A.CallTo(() => sharedRepository.SaveChangesAsync()).MustHaveHappenedTwiceExactly();
        }
    }
}
