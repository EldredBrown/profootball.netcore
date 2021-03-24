using System.Collections.Generic;
using System.Threading.Tasks;
using EldredBrown.ProFootball.NETCore.Data.Entities;
using EldredBrown.ProFootball.NETCore.Data.Repositories;
using FakeItEasy;
using Xunit;

namespace EldredBrown.ProFootball.NETCore.Services.Tests
{
    public class WeeklyUpdateServiceTest
    {
        private readonly ISeasonRepository _seasonRepository;
        private readonly IGameRepository _gameRepository;
        private readonly ILeagueSeasonRepository _leagueSeasonRepository;
        private readonly ILeagueSeasonTotalsRepository _leagueSeasonTotalsRepository;
        private readonly ITeamSeasonRepository _teamSeasonRepository;
        private readonly ITeamSeasonScheduleTotalsRepository _teamSeasonScheduleTotalsRepository;
        private readonly ITeamSeasonScheduleAveragesRepository _teamSeasonScheduleAveragesRepository;
        private readonly ISharedRepository _sharedRepository;

        public WeeklyUpdateServiceTest()
        {
            _seasonRepository = A.Fake<ISeasonRepository>();
            _gameRepository = A.Fake<IGameRepository>();
            _leagueSeasonRepository = A.Fake<ILeagueSeasonRepository>();
            _leagueSeasonTotalsRepository = A.Fake<ILeagueSeasonTotalsRepository>();
            _teamSeasonRepository = A.Fake<ITeamSeasonRepository>();
            _teamSeasonScheduleTotalsRepository = A.Fake<ITeamSeasonScheduleTotalsRepository>();
            _teamSeasonScheduleAveragesRepository = A.Fake<ITeamSeasonScheduleAveragesRepository>();
            _sharedRepository = A.Fake<ISharedRepository>();
        }

        [Fact]
        public async Task RunWeeklyUpdate_WhenLeagueSeasonTotalsTotalGamesIsNull_ShouldNotUpdateLeagueSeasonGamesAndPoints()
        {
            // Arrange
            var service = new WeeklyUpdateService(_seasonRepository, _gameRepository, _leagueSeasonRepository,
                _leagueSeasonTotalsRepository, _teamSeasonRepository, _teamSeasonScheduleTotalsRepository,
                _teamSeasonScheduleAveragesRepository, _sharedRepository);

            var leagueSeasonTotals = new LeagueSeasonTotals
            {
                TotalGames = null
            };
            A.CallTo(() => _leagueSeasonTotalsRepository.GetLeagueSeasonTotals(A<string>.Ignored, A<int>.Ignored))
                .Returns(leagueSeasonTotals);

            var seasonYear = 1920;

            var games = new List<Game>
            {
                new Game
                {
                    SeasonYear = seasonYear,
                    Week = 0
                }
            };
            A.CallTo(() => _gameRepository.GetGames()).Returns(games);

            // Act
            await service.RunWeeklyUpdate(seasonYear);

            // Assert
            var leagueName = "APFA";

            A.CallTo(() => _leagueSeasonTotalsRepository.GetLeagueSeasonTotals(leagueName, seasonYear))
                .MustHaveHappened();
            A.CallTo(() => _leagueSeasonRepository.GetLeagueSeasonByLeagueAndSeason(leagueName, seasonYear))
                .MustNotHaveHappened();
            A.CallTo(() => _gameRepository.GetGames()).MustHaveHappened();
            A.CallTo(() => _seasonRepository.GetSeason(seasonYear)).MustHaveHappened();
            A.CallTo(() => _sharedRepository.SaveChanges()).MustHaveHappenedOnceExactly();
            A.CallTo(() => _teamSeasonRepository.GetTeamSeasons()).MustNotHaveHappened();
        }

        [Fact]
        public async Task RunWeeklyUpdate_WhenLeagueSeasonTotalsTotalGamesIsNotNull_ShouldUpdateLeagueSeasonGamesAndPoints()
        {
            // Arrange
            var service = new WeeklyUpdateService(_seasonRepository, _gameRepository, _leagueSeasonRepository,
                _leagueSeasonTotalsRepository, _teamSeasonRepository, _teamSeasonScheduleTotalsRepository,
                _teamSeasonScheduleAveragesRepository, _sharedRepository);

            var leagueSeasonTotals = new LeagueSeasonTotals
            {
                TotalGames = 0,
                TotalPoints = 0
            };
            A.CallTo(() => _leagueSeasonTotalsRepository.GetLeagueSeasonTotals(A<string>.Ignored, A<int>.Ignored))
                .Returns(leagueSeasonTotals);

            var leagueSeason = new LeagueSeason();
            A.CallTo(() => _leagueSeasonRepository.GetLeagueSeasonByLeagueAndSeason(A<string>.Ignored, A<int>.Ignored))
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
            A.CallTo(() => _gameRepository.GetGames()).Returns(games);

            // Act
            await service.RunWeeklyUpdate(seasonYear);

            // Assert
            var leagueName = "APFA";

            A.CallTo(() => _leagueSeasonTotalsRepository.GetLeagueSeasonTotals(leagueName, seasonYear))
                .MustHaveHappened();
            A.CallTo(() => _leagueSeasonRepository.GetLeagueSeasonByLeagueAndSeason(leagueName, seasonYear))
                .MustHaveHappened();
            A.CallTo(() => _gameRepository.GetGames()).MustHaveHappened();
            A.CallTo(() => _seasonRepository.GetSeason(seasonYear)).MustHaveHappened();
            A.CallTo(() => _sharedRepository.SaveChanges()).MustHaveHappenedOnceExactly();
            A.CallTo(() => _teamSeasonRepository.GetTeamSeasons()).MustNotHaveHappened();
        }

        [Fact]
        public async Task RunWeeklyUpdate_WhenWeekCountLessThanThree_ShouldNotUpdateRankings()
        {
            // Arrange
            var service = new WeeklyUpdateService(_seasonRepository, _gameRepository, _leagueSeasonRepository,
                _leagueSeasonTotalsRepository, _teamSeasonRepository, _teamSeasonScheduleTotalsRepository,
                _teamSeasonScheduleAveragesRepository, _sharedRepository);

            var leagueSeasonTotals = new LeagueSeasonTotals
            {
                TotalGames = 0,
                TotalPoints = 0
            };
            A.CallTo(() => _leagueSeasonTotalsRepository.GetLeagueSeasonTotals(A<string>.Ignored, A<int>.Ignored))
                .Returns(leagueSeasonTotals);

            var leagueSeason = new LeagueSeason();
            A.CallTo(() => _leagueSeasonRepository.GetLeagueSeasonByLeagueAndSeason(A<string>.Ignored, A<int>.Ignored))
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
            A.CallTo(() => _gameRepository.GetGames()).Returns(games);

            // Act
            await service.RunWeeklyUpdate(seasonYear);

            // Assert
            var leagueName = "APFA";

            A.CallTo(() => _leagueSeasonTotalsRepository.GetLeagueSeasonTotals(leagueName, seasonYear))
                .MustHaveHappened();
            A.CallTo(() => _leagueSeasonRepository.GetLeagueSeasonByLeagueAndSeason(leagueName, seasonYear))
                .MustHaveHappened();
            A.CallTo(() => _gameRepository.GetGames()).MustHaveHappened();
            A.CallTo(() => _seasonRepository.GetSeason(seasonYear)).MustHaveHappened();
            A.CallTo(() => _sharedRepository.SaveChanges()).MustHaveHappenedOnceExactly();
            A.CallTo(() => _teamSeasonRepository.GetTeamSeasons()).MustNotHaveHappened();
        }

        [Fact]
        public async Task RunWeeklyUpdate_WhenTeamSeasonScheduleTotalsIsNull_ShouldNotUpdateRankingsForTeamSeason()
        {
            // Arrange
            var service = new WeeklyUpdateService(_seasonRepository, _gameRepository, _leagueSeasonRepository,
                _leagueSeasonTotalsRepository, _teamSeasonRepository, _teamSeasonScheduleTotalsRepository,
                _teamSeasonScheduleAveragesRepository, _sharedRepository);

            var leagueSeasonTotals = new LeagueSeasonTotals
            {
                TotalGames = 0,
                TotalPoints = 0
            };
            A.CallTo(() => _leagueSeasonTotalsRepository.GetLeagueSeasonTotals(A<string>.Ignored, A<int>.Ignored))
                .Returns(leagueSeasonTotals);

            var leagueSeason = new LeagueSeason();
            A.CallTo(() => _leagueSeasonRepository.GetLeagueSeasonByLeagueAndSeason(A<string>.Ignored, A<int>.Ignored))
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
            A.CallTo(() => _gameRepository.GetGames()).Returns(games);

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
            A.CallTo(() => _teamSeasonRepository.GetTeamSeasons()).Returns(teamSeasons);

            TeamSeasonScheduleTotals teamSeasonScheduleTotals = null;
            A.CallTo(() => _teamSeasonScheduleTotalsRepository.GetTeamSeasonScheduleTotals(A<string>.Ignored,
                seasonYear)).Returns(teamSeasonScheduleTotals);

            TeamSeasonScheduleAverages teamSeasonScheduleAverages = null;
            A.CallTo(() => _teamSeasonScheduleAveragesRepository.GetTeamSeasonScheduleAverages(A<string>.Ignored,
                seasonYear)).Returns(teamSeasonScheduleAverages);

            // Act
            await service.RunWeeklyUpdate(seasonYear);

            // Assert
            A.CallTo(() => _leagueSeasonTotalsRepository.GetLeagueSeasonTotals(leagueName, seasonYear))
                .MustHaveHappened();
            A.CallTo(() => _leagueSeasonRepository.GetLeagueSeasonByLeagueAndSeason(leagueName, seasonYear))
                .MustHaveHappenedOnceExactly();
            A.CallTo(() => _gameRepository.GetGames()).MustHaveHappened();
            A.CallTo(() => _seasonRepository.GetSeason(seasonYear)).MustHaveHappened();
            A.CallTo(() => _teamSeasonRepository.GetTeamSeasons()).MustHaveHappened();
            A.CallTo(() => _teamSeasonScheduleTotalsRepository.GetTeamSeasonScheduleTotals(teamName, seasonYear))
                .MustHaveHappened();
            A.CallTo(() => _teamSeasonScheduleAveragesRepository.GetTeamSeasonScheduleAverages(teamName, seasonYear))
                .MustHaveHappened();
            A.CallTo(() => _sharedRepository.SaveChanges()).MustHaveHappenedTwiceExactly();
        }

        [Fact]
        public async Task RunWeeklyUpdate_WhenTeamSeasonScheduleAveragesIsNull_ShouldNotUpdateRankingsForTeamSeason()
        {
            // Arrange
            var service = new WeeklyUpdateService(_seasonRepository, _gameRepository, _leagueSeasonRepository,
                _leagueSeasonTotalsRepository, _teamSeasonRepository, _teamSeasonScheduleTotalsRepository,
                _teamSeasonScheduleAveragesRepository, _sharedRepository);

            var leagueSeasonTotals = new LeagueSeasonTotals
            {
                TotalGames = 0,
                TotalPoints = 0
            };
            A.CallTo(() => _leagueSeasonTotalsRepository.GetLeagueSeasonTotals(A<string>.Ignored, A<int>.Ignored))
                .Returns(leagueSeasonTotals);

            var leagueSeason = new LeagueSeason();
            A.CallTo(() => _leagueSeasonRepository.GetLeagueSeasonByLeagueAndSeason(A<string>.Ignored, A<int>.Ignored))
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
            A.CallTo(() => _gameRepository.GetGames()).Returns(games);

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
            A.CallTo(() => _teamSeasonRepository.GetTeamSeasons()).Returns(teamSeasons);

            var teamSeasonScheduleTotals = new TeamSeasonScheduleTotals
            {
                ScheduleGames = null
            };
            A.CallTo(() => _teamSeasonScheduleTotalsRepository.GetTeamSeasonScheduleTotals(A<string>.Ignored,
                seasonYear)).Returns(teamSeasonScheduleTotals);

            TeamSeasonScheduleAverages teamSeasonScheduleAverages = null;
            A.CallTo(() => _teamSeasonScheduleAveragesRepository.GetTeamSeasonScheduleAverages(A<string>.Ignored,
                seasonYear)).Returns(teamSeasonScheduleAverages);

            // Act
            await service.RunWeeklyUpdate(seasonYear);

            // Assert
            A.CallTo(() => _leagueSeasonTotalsRepository.GetLeagueSeasonTotals(leagueName, seasonYear))
                .MustHaveHappened();
            A.CallTo(() => _leagueSeasonRepository.GetLeagueSeasonByLeagueAndSeason(leagueName, seasonYear))
                .MustHaveHappenedOnceExactly();
            A.CallTo(() => _gameRepository.GetGames()).MustHaveHappened();
            A.CallTo(() => _seasonRepository.GetSeason(seasonYear)).MustHaveHappened();
            A.CallTo(() => _teamSeasonRepository.GetTeamSeasons()).MustHaveHappened();
            A.CallTo(() => _teamSeasonScheduleTotalsRepository.GetTeamSeasonScheduleTotals(teamName, seasonYear))
                .MustHaveHappened();
            A.CallTo(() => _teamSeasonScheduleAveragesRepository.GetTeamSeasonScheduleAverages(teamName, seasonYear))
                .MustHaveHappened();
            A.CallTo(() => _sharedRepository.SaveChanges()).MustHaveHappenedTwiceExactly();
        }

        [Fact]
        public async Task RunWeeklyUpdate_WhenTeamSeasonScheduleTotalsScheduleGamesIsNull_ShouldNotUpdateRankingsForTeamSeason()
        {
            // Arrange
            var service = new WeeklyUpdateService(_seasonRepository, _gameRepository, _leagueSeasonRepository,
                _leagueSeasonTotalsRepository, _teamSeasonRepository, _teamSeasonScheduleTotalsRepository,
                _teamSeasonScheduleAveragesRepository, _sharedRepository);

            var leagueSeasonTotals = new LeagueSeasonTotals
            {
                TotalGames = 0,
                TotalPoints = 0
            };
            A.CallTo(() => _leagueSeasonTotalsRepository.GetLeagueSeasonTotals(A<string>.Ignored, A<int>.Ignored))
                .Returns(leagueSeasonTotals);

            var leagueSeason = new LeagueSeason();
            A.CallTo(() => _leagueSeasonRepository.GetLeagueSeasonByLeagueAndSeason(A<string>.Ignored, A<int>.Ignored))
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
            A.CallTo(() => _gameRepository.GetGames()).Returns(games);

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
            A.CallTo(() => _teamSeasonRepository.GetTeamSeasons()).Returns(teamSeasons);

            var teamSeasonScheduleTotals = new TeamSeasonScheduleTotals
            {
                ScheduleGames = null
            };
            A.CallTo(() => _teamSeasonScheduleTotalsRepository.GetTeamSeasonScheduleTotals(A<string>.Ignored,
                seasonYear)).Returns(teamSeasonScheduleTotals);

            var teamSeasonScheduleAverages = new TeamSeasonScheduleAverages();
            A.CallTo(() => _teamSeasonScheduleAveragesRepository.GetTeamSeasonScheduleAverages(A<string>.Ignored,
                seasonYear)).Returns(teamSeasonScheduleAverages);

            // Act
            await service.RunWeeklyUpdate(seasonYear);

            // Assert
            A.CallTo(() => _leagueSeasonTotalsRepository.GetLeagueSeasonTotals(leagueName, seasonYear))
                .MustHaveHappened();
            A.CallTo(() => _leagueSeasonRepository.GetLeagueSeasonByLeagueAndSeason(leagueName, seasonYear))
                .MustHaveHappenedOnceExactly();
            A.CallTo(() => _gameRepository.GetGames()).MustHaveHappened();
            A.CallTo(() => _seasonRepository.GetSeason(seasonYear)).MustHaveHappened();
            A.CallTo(() => _teamSeasonRepository.GetTeamSeasons()).MustHaveHappened();
            A.CallTo(() => _teamSeasonScheduleTotalsRepository.GetTeamSeasonScheduleTotals(teamName, seasonYear))
                .MustHaveHappened();
            A.CallTo(() => _teamSeasonScheduleAveragesRepository.GetTeamSeasonScheduleAverages(teamName, seasonYear))
                .MustHaveHappened();
            A.CallTo(() => _sharedRepository.SaveChanges()).MustHaveHappenedTwiceExactly();
        }

        [Fact]
        public async Task RunWeeklyUpdate_WhenTeamSeasonScheduleTotalsScheduleTotalsAndTeamSeasonScheduleAveragesAndTeamSeasonScheduleTotalsScheduleGamesAreNotNull_ShouldUpdateRankingsForTeamSeason()
        {
            // Arrange
            var service = new WeeklyUpdateService(_seasonRepository, _gameRepository, _leagueSeasonRepository,
                _leagueSeasonTotalsRepository, _teamSeasonRepository, _teamSeasonScheduleTotalsRepository,
                _teamSeasonScheduleAveragesRepository, _sharedRepository);

            var leagueSeasonTotals = new LeagueSeasonTotals
            {
                TotalGames = 0,
                TotalPoints = 0
            };
            A.CallTo(() => _leagueSeasonTotalsRepository.GetLeagueSeasonTotals(A<string>.Ignored, A<int>.Ignored))
                .Returns(leagueSeasonTotals);

            var leagueSeason = new LeagueSeason
            {
                AveragePoints = 0
            };
            A.CallTo(() => _leagueSeasonRepository.GetLeagueSeasonByLeagueAndSeason(A<string>.Ignored, A<int>.Ignored))
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
            A.CallTo(() => _gameRepository.GetGames()).Returns(games);

            var teamName = "Team";
            var leagueName = "APFA";

            var teamSeason = new TeamSeason
            {
                TeamName = teamName,
                SeasonYear = seasonYear,
                LeagueName = leagueName
            };
            var teamSeasons = new List<TeamSeason>
            {
                teamSeason
            };
            A.CallTo(() => _teamSeasonRepository.GetTeamSeasons()).Returns(teamSeasons);

            var teamSeasonScheduleTotals = new TeamSeasonScheduleTotals
            {
                ScheduleGames = 0
            };
            A.CallTo(() => _teamSeasonScheduleTotalsRepository.GetTeamSeasonScheduleTotals(A<string>.Ignored,
                seasonYear)).Returns(teamSeasonScheduleTotals);

            var teamSeasonScheduleAverages = new TeamSeasonScheduleAverages
            {
                PointsFor = 0,
                PointsAgainst = 0
            };
            A.CallTo(() => _teamSeasonScheduleAveragesRepository.GetTeamSeasonScheduleAverages(A<string>.Ignored,
                seasonYear)).Returns(teamSeasonScheduleAverages);

            // Act
            await service.RunWeeklyUpdate(seasonYear);

            // Assert
            A.CallTo(() => _leagueSeasonTotalsRepository.GetLeagueSeasonTotals(leagueName, seasonYear))
                .MustHaveHappened();
            A.CallTo(() => _leagueSeasonRepository.GetLeagueSeasonByLeagueAndSeason(leagueName, seasonYear))
                .MustHaveHappenedTwiceOrMore();
            A.CallTo(() => _gameRepository.GetGames()).MustHaveHappened();
            A.CallTo(() => _seasonRepository.GetSeason(seasonYear)).MustHaveHappened();
            A.CallTo(() => _teamSeasonRepository.GetTeamSeasons()).MustHaveHappened();
            A.CallTo(() => _teamSeasonScheduleTotalsRepository.GetTeamSeasonScheduleTotals(teamName, seasonYear))
                .MustHaveHappened();
            A.CallTo(() => _teamSeasonScheduleAveragesRepository.GetTeamSeasonScheduleAverages(teamName, seasonYear))
                .MustHaveHappened();
            A.CallTo(() => _sharedRepository.SaveChanges()).MustHaveHappenedTwiceExactly();
        }
    }
}
