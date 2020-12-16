using System.Collections.Generic;
using System.Threading.Tasks;
using EldredBrown.ProFootball.NETCore.Data.Entities;
using EldredBrown.ProFootball.NETCore.Data.Repositories;
using EldredBrown.ProFootball.NETCore.Data.Utilities;
using FakeItEasy;
using NUnit.Framework;

namespace EldredBrown.ProFootball.NETCore.Services.Tests
{
    [TestFixture]
    public class WeeklyUpdateServiceTests
    {
        private ISeasonRepository _seasonRepository;
        private IGameRepository _gameRepository;
        private ILeagueSeasonUtility _leagueSeasonUtility;
        private ILeagueSeasonRepository _leagueSeasonRepository;
        private ILeagueSeasonTotalsRepository _leagueSeasonTotalsRepository;
        private ITeamSeasonUtility _teamSeasonUtility;
        private ITeamSeasonRepository _teamSeasonRepository;
        private ITeamSeasonScheduleTotalsRepository _teamSeasonScheduleTotalsRepository;
        private ITeamSeasonScheduleAveragesRepository _teamSeasonScheduleAveragesRepository;
        private ISharedRepository _sharedRepository;

        [SetUp]
        public void Setup()
        {
            _seasonRepository = A.Fake<ISeasonRepository>();
            _gameRepository = A.Fake<IGameRepository>();
            _leagueSeasonUtility = A.Fake<ILeagueSeasonUtility>();
            _leagueSeasonRepository = A.Fake<ILeagueSeasonRepository>();
            _leagueSeasonTotalsRepository = A.Fake<ILeagueSeasonTotalsRepository>();
            _teamSeasonUtility = A.Fake<ITeamSeasonUtility>();
            _teamSeasonRepository = A.Fake<ITeamSeasonRepository>();
            _teamSeasonScheduleTotalsRepository = A.Fake<ITeamSeasonScheduleTotalsRepository>();
            _teamSeasonScheduleAveragesRepository = A.Fake<ITeamSeasonScheduleAveragesRepository>();
            _sharedRepository = A.Fake<ISharedRepository>();
        }

        [Test]
        public async Task RunWeeklyUpdate_DoesNotUpdateLeagueSeasonGamesAndPointsWhenLeagueSeasonTotalsTotalGamesIsNull()
        {
            var service = new WeeklyUpdateService(_seasonRepository, _gameRepository, _leagueSeasonUtility,
                _leagueSeasonRepository, _leagueSeasonTotalsRepository, _teamSeasonUtility, _teamSeasonRepository,
                _teamSeasonScheduleTotalsRepository, _teamSeasonScheduleAveragesRepository, _sharedRepository);

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

            await service.RunWeeklyUpdate(seasonYear);

            var leagueName = "APFA";

            A.CallTo(() => _leagueSeasonTotalsRepository.GetLeagueSeasonTotals(leagueName, seasonYear))
                .MustHaveHappened();
            A.CallTo(() => _leagueSeasonRepository.GetLeagueSeasonByLeagueAndSeason(leagueName, seasonYear))
                .MustNotHaveHappened();
            A.CallTo(() => _leagueSeasonUtility.UpdateGamesAndPoints(A<LeagueSeason>.Ignored, A<int>.Ignored,
                A<int>.Ignored)).MustNotHaveHappened();
            A.CallTo(() => _gameRepository.GetGames()).MustHaveHappened();
            A.CallTo(() => _seasonRepository.GetSeason(seasonYear)).MustHaveHappened();
            A.CallTo(() => _sharedRepository.SaveChanges()).MustHaveHappenedOnceExactly();
            A.CallTo(() => _teamSeasonRepository.GetTeamSeasons()).MustNotHaveHappened();
        }

        [Test]
        public async Task RunWeeklyUpdate_UpdatesLeagueSeasonGamesAndPointsWhenLeagueSeasonTotalsTotalGamesIsNotNull()
        {
            var service = new WeeklyUpdateService(_seasonRepository, _gameRepository, _leagueSeasonUtility,
                _leagueSeasonRepository, _leagueSeasonTotalsRepository, _teamSeasonUtility, _teamSeasonRepository,
                _teamSeasonScheduleTotalsRepository, _teamSeasonScheduleAveragesRepository, _sharedRepository);

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

            await service.RunWeeklyUpdate(seasonYear);

            var leagueName = "APFA";

            A.CallTo(() => _leagueSeasonTotalsRepository.GetLeagueSeasonTotals(leagueName, seasonYear))
                .MustHaveHappened();
            A.CallTo(() => _leagueSeasonRepository.GetLeagueSeasonByLeagueAndSeason(leagueName, seasonYear))
                .MustHaveHappened();
            A.CallTo(() => _leagueSeasonUtility.UpdateGamesAndPoints(leagueSeason, leagueSeasonTotals.TotalGames.Value,
                leagueSeasonTotals.TotalPoints.Value)).MustHaveHappened();
            A.CallTo(() => _gameRepository.GetGames()).MustHaveHappened();
            A.CallTo(() => _seasonRepository.GetSeason(seasonYear)).MustHaveHappened();
            A.CallTo(() => _sharedRepository.SaveChanges()).MustHaveHappenedOnceExactly();
            A.CallTo(() => _teamSeasonRepository.GetTeamSeasons()).MustNotHaveHappened();
        }

        [Test]
        public async Task RunWeeklyUpdate_DoesNotUpdateRankingsWhenWeekCountLessThanThree()
        {
            var service = new WeeklyUpdateService(_seasonRepository, _gameRepository, _leagueSeasonUtility,
                _leagueSeasonRepository, _leagueSeasonTotalsRepository, _teamSeasonUtility, _teamSeasonRepository,
                _teamSeasonScheduleTotalsRepository, _teamSeasonScheduleAveragesRepository, _sharedRepository);

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

            await service.RunWeeklyUpdate(seasonYear);

            var leagueName = "APFA";

            A.CallTo(() => _leagueSeasonTotalsRepository.GetLeagueSeasonTotals(leagueName, seasonYear))
                .MustHaveHappened();
            A.CallTo(() => _leagueSeasonRepository.GetLeagueSeasonByLeagueAndSeason(leagueName, seasonYear))
                .MustHaveHappened();
            A.CallTo(() => _leagueSeasonUtility.UpdateGamesAndPoints(leagueSeason, leagueSeasonTotals.TotalGames.Value,
                leagueSeasonTotals.TotalPoints.Value)).MustHaveHappened();
            A.CallTo(() => _gameRepository.GetGames()).MustHaveHappened();
            A.CallTo(() => _seasonRepository.GetSeason(seasonYear)).MustHaveHappened();
            A.CallTo(() => _sharedRepository.SaveChanges()).MustHaveHappenedOnceExactly();
            A.CallTo(() => _teamSeasonRepository.GetTeamSeasons()).MustNotHaveHappened();
        }

        [Test]
        public async Task RunWeeklyUpdate_DoesNotUpdateRankingsForTeamSeasonWhenTeamSeasonScheduleTotalsIsNull()
        {
            var service = new WeeklyUpdateService(_seasonRepository, _gameRepository, _leagueSeasonUtility,
                _leagueSeasonRepository, _leagueSeasonTotalsRepository, _teamSeasonUtility, _teamSeasonRepository,
                _teamSeasonScheduleTotalsRepository, _teamSeasonScheduleAveragesRepository, _sharedRepository);

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

            await service.RunWeeklyUpdate(seasonYear);

            A.CallTo(() => _leagueSeasonTotalsRepository.GetLeagueSeasonTotals(leagueName, seasonYear))
                .MustHaveHappened();
            A.CallTo(() => _leagueSeasonRepository.GetLeagueSeasonByLeagueAndSeason(leagueName, seasonYear))
                .MustHaveHappenedOnceExactly();
            A.CallTo(() => _leagueSeasonUtility.UpdateGamesAndPoints(leagueSeason, leagueSeasonTotals.TotalGames.Value,
                leagueSeasonTotals.TotalPoints.Value)).MustHaveHappened();
            A.CallTo(() => _gameRepository.GetGames()).MustHaveHappened();
            A.CallTo(() => _seasonRepository.GetSeason(seasonYear)).MustHaveHappened();
            A.CallTo(() => _teamSeasonRepository.GetTeamSeasons()).MustHaveHappened();
            A.CallTo(() => _teamSeasonScheduleTotalsRepository.GetTeamSeasonScheduleTotals(teamName, seasonYear))
                .MustHaveHappened();
            A.CallTo(() => _teamSeasonScheduleAveragesRepository.GetTeamSeasonScheduleAverages(teamName, seasonYear))
                .MustHaveHappened();
            A.CallTo(() => _teamSeasonUtility.UpdateRankings(A<TeamSeason>.Ignored, A<double>.Ignored,
                A<double>.Ignored, A<double>.Ignored)).MustNotHaveHappened();
            A.CallTo(() => _sharedRepository.SaveChanges()).MustHaveHappenedTwiceExactly();
        }

        [Test]
        public async Task RunWeeklyUpdate_DoesNotUpdateRankingsForTeamSeasonWhenTeamSeasonScheduleAveragesIsNull()
        {
            var service = new WeeklyUpdateService(_seasonRepository, _gameRepository, _leagueSeasonUtility,
                _leagueSeasonRepository, _leagueSeasonTotalsRepository, _teamSeasonUtility, _teamSeasonRepository,
                _teamSeasonScheduleTotalsRepository, _teamSeasonScheduleAveragesRepository, _sharedRepository);

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

            await service.RunWeeklyUpdate(seasonYear);

            A.CallTo(() => _leagueSeasonTotalsRepository.GetLeagueSeasonTotals(leagueName, seasonYear))
                .MustHaveHappened();
            A.CallTo(() => _leagueSeasonRepository.GetLeagueSeasonByLeagueAndSeason(leagueName, seasonYear))
                .MustHaveHappenedOnceExactly();
            A.CallTo(() => _leagueSeasonUtility.UpdateGamesAndPoints(leagueSeason, leagueSeasonTotals.TotalGames.Value,
                leagueSeasonTotals.TotalPoints.Value)).MustHaveHappened();
            A.CallTo(() => _gameRepository.GetGames()).MustHaveHappened();
            A.CallTo(() => _seasonRepository.GetSeason(seasonYear)).MustHaveHappened();
            A.CallTo(() => _teamSeasonRepository.GetTeamSeasons()).MustHaveHappened();
            A.CallTo(() => _teamSeasonScheduleTotalsRepository.GetTeamSeasonScheduleTotals(teamName, seasonYear))
                .MustHaveHappened();
            A.CallTo(() => _teamSeasonScheduleAveragesRepository.GetTeamSeasonScheduleAverages(teamName, seasonYear))
                .MustHaveHappened();
            A.CallTo(() => _teamSeasonUtility.UpdateRankings(A<TeamSeason>.Ignored, A<double>.Ignored,
                A<double>.Ignored, A<double>.Ignored)).MustNotHaveHappened();
            A.CallTo(() => _sharedRepository.SaveChanges()).MustHaveHappenedTwiceExactly();
        }

        [Test]
        public async Task RunWeeklyUpdate_DoesNotUpdateRankingsForTeamSeasonWhenTeamSeasonScheduleTotalsScheduleGamesIsNull()
        {
            var service = new WeeklyUpdateService(_seasonRepository, _gameRepository, _leagueSeasonUtility,
                _leagueSeasonRepository, _leagueSeasonTotalsRepository, _teamSeasonUtility, _teamSeasonRepository,
                _teamSeasonScheduleTotalsRepository, _teamSeasonScheduleAveragesRepository, _sharedRepository);

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

            await service.RunWeeklyUpdate(seasonYear);

            A.CallTo(() => _leagueSeasonTotalsRepository.GetLeagueSeasonTotals(leagueName, seasonYear))
                .MustHaveHappened();
            A.CallTo(() => _leagueSeasonRepository.GetLeagueSeasonByLeagueAndSeason(leagueName, seasonYear))
                .MustHaveHappenedOnceExactly();
            A.CallTo(() => _leagueSeasonUtility.UpdateGamesAndPoints(leagueSeason, leagueSeasonTotals.TotalGames.Value,
                leagueSeasonTotals.TotalPoints.Value)).MustHaveHappened();
            A.CallTo(() => _gameRepository.GetGames()).MustHaveHappened();
            A.CallTo(() => _seasonRepository.GetSeason(seasonYear)).MustHaveHappened();
            A.CallTo(() => _teamSeasonRepository.GetTeamSeasons()).MustHaveHappened();
            A.CallTo(() => _teamSeasonScheduleTotalsRepository.GetTeamSeasonScheduleTotals(teamName, seasonYear))
                .MustHaveHappened();
            A.CallTo(() => _teamSeasonScheduleAveragesRepository.GetTeamSeasonScheduleAverages(teamName, seasonYear))
                .MustHaveHappened();
            A.CallTo(() => _teamSeasonUtility.UpdateRankings(A<TeamSeason>.Ignored, A<double>.Ignored,
                A<double>.Ignored, A<double>.Ignored)).MustNotHaveHappened();
            A.CallTo(() => _sharedRepository.SaveChanges()).MustHaveHappenedTwiceExactly();
        }

        [Test]
        public async Task RunWeeklyUpdate_UpdatesRankingsForTeamSeasonWhenTeamSeasonScheduleTotalsScheduleTotalsAndTeamSeasonScheduleAveragesAndTeamSeasonScheduleTotalsScheduleGamesAreNotNull()
        {
            var service = new WeeklyUpdateService(_seasonRepository, _gameRepository, _leagueSeasonUtility,
                _leagueSeasonRepository, _leagueSeasonTotalsRepository, _teamSeasonUtility, _teamSeasonRepository,
                _teamSeasonScheduleTotalsRepository, _teamSeasonScheduleAveragesRepository, _sharedRepository);

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

            await service.RunWeeklyUpdate(seasonYear);

            A.CallTo(() => _leagueSeasonTotalsRepository.GetLeagueSeasonTotals(leagueName, seasonYear))
                .MustHaveHappened();
            A.CallTo(() => _leagueSeasonRepository.GetLeagueSeasonByLeagueAndSeason(leagueName, seasonYear))
                .MustHaveHappenedTwiceOrMore();
            A.CallTo(() => _leagueSeasonUtility.UpdateGamesAndPoints(leagueSeason, leagueSeasonTotals.TotalGames.Value,
                leagueSeasonTotals.TotalPoints.Value)).MustHaveHappened();
            A.CallTo(() => _gameRepository.GetGames()).MustHaveHappened();
            A.CallTo(() => _seasonRepository.GetSeason(seasonYear)).MustHaveHappened();
            A.CallTo(() => _teamSeasonRepository.GetTeamSeasons()).MustHaveHappened();
            A.CallTo(() => _teamSeasonScheduleTotalsRepository.GetTeamSeasonScheduleTotals(teamName, seasonYear))
                .MustHaveHappened();
            A.CallTo(() => _teamSeasonScheduleAveragesRepository.GetTeamSeasonScheduleAverages(teamName, seasonYear))
                .MustHaveHappened();
            A.CallTo(() => _teamSeasonUtility.UpdateRankings(teamSeason, teamSeasonScheduleAverages.PointsFor.Value,
                teamSeasonScheduleAverages.PointsAgainst.Value, leagueSeason.AveragePoints.Value)).MustHaveHappened();
            A.CallTo(() => _sharedRepository.SaveChanges()).MustHaveHappenedTwiceExactly();
        }
    }
}
