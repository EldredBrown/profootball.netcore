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
            A.CallTo(() => _sharedRepository.SaveChanges()).MustHaveHappened();
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
            A.CallTo(() => _sharedRepository.SaveChanges()).MustHaveHappened();
            A.CallTo(() => _teamSeasonRepository.GetTeamSeasons()).MustNotHaveHappened();
        }
    }
}
