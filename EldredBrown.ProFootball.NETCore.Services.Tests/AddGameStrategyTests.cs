using System.Threading.Tasks;
using EldredBrown.ProFootball.NETCore.Data.Entities;
using EldredBrown.ProFootball.NETCore.Data.Repositories;
using EldredBrown.ProFootball.NETCore.Data.Utilities;
using FakeItEasy;
using NUnit.Framework;

namespace EldredBrown.ProFootball.NETCore.Services.Tests
{
    [TestFixture]
    public class AddGameStrategyTests
    {
        private IGameUtility _gameUtility;
        private ITeamSeasonUtility _teamSeasonUtility;
        private ITeamSeasonRepository _teamSeasonRepository;

        [SetUp]
        public void Setup()
        {
            _gameUtility = A.Fake<IGameUtility>();
            _teamSeasonUtility = A.Fake<ITeamSeasonUtility>();
            _teamSeasonRepository = A.Fake<ITeamSeasonRepository>();
        }

        [Test]
        public async Task ProcessGame_UpdatesTiesForTeamSeasonsWhenGameIsATie()
        {
            var strategy = new AddGameStrategy(_gameUtility, _teamSeasonUtility, _teamSeasonRepository);

            A.CallTo(() => _gameUtility.IsTie(A<Game>.Ignored)).Returns(true);

            var game = new Game
            {
                GuestName = "Guest",
                HostName = "Host",
            };

            TeamSeason guestSeason = null;
            A.CallTo(() => _teamSeasonRepository.GetTeamSeasonByTeamAndSeason(game.GuestName, A<int>.Ignored))
                .Returns(guestSeason);

            TeamSeason hostSeason = null;
            A.CallTo(() => _teamSeasonRepository.GetTeamSeasonByTeamAndSeason(game.HostName, A<int>.Ignored))
                .Returns(hostSeason);

            await strategy.ProcessGame(game);

            var seasonYear = game.SeasonYear;

            A.CallTo(() => _teamSeasonRepository.GetTeamSeasonByTeamAndSeason(game.GuestName, seasonYear))
                .MustHaveHappenedOnceExactly();
            A.CallTo(() => _teamSeasonRepository.GetTeamSeasonByTeamAndSeason(game.HostName, seasonYear))
                .MustHaveHappenedOnceExactly();
            A.CallTo(() => _teamSeasonRepository.GetTeamSeasonByTeamAndSeason(game.WinnerName, seasonYear))
                .MustNotHaveHappened();
            A.CallTo(() => _teamSeasonRepository.GetTeamSeasonByTeamAndSeason(game.LoserName, seasonYear))
                .MustNotHaveHappened();
        }

        [Test]
        public async Task ProcessGame_UpdatesWinsAndLossesForTeamSeasonsWhenGameIsNotATie()
        {
            var strategy = new AddGameStrategy(_gameUtility, _teamSeasonUtility, _teamSeasonRepository);

            A.CallTo(() => _gameUtility.IsTie(A<Game>.Ignored)).Returns(false);

            var game = new Game
            {
                GuestName = "Guest",
                HostName = "Host",
                WinnerName = "Winner",
                LoserName = "Loser"
            };

            TeamSeason guestSeason = null;
            A.CallTo(() => _teamSeasonRepository.GetTeamSeasonByTeamAndSeason(game.GuestName, A<int>.Ignored))
                .Returns(guestSeason);

            TeamSeason hostSeason = null;
            A.CallTo(() => _teamSeasonRepository.GetTeamSeasonByTeamAndSeason(game.HostName, A<int>.Ignored))
                .Returns(hostSeason);

            await strategy.ProcessGame(game);

            var seasonYear = game.SeasonYear;

            A.CallTo(() => _teamSeasonRepository.GetTeamSeasonByTeamAndSeason(game.GuestName, seasonYear))
                .MustHaveHappened();
            A.CallTo(() => _teamSeasonRepository.GetTeamSeasonByTeamAndSeason(game.HostName, seasonYear))
                .MustHaveHappened();
            A.CallTo(() => _teamSeasonRepository.GetTeamSeasonByTeamAndSeason(game.WinnerName, seasonYear))
                .MustHaveHappened();
            A.CallTo(() => _teamSeasonRepository.GetTeamSeasonByTeamAndSeason(game.LoserName, seasonYear))
                .MustHaveHappened();
            A.CallTo(() => _teamSeasonUtility.CalculateWinningPercentage(guestSeason)).MustNotHaveHappened();
            A.CallTo(() => _teamSeasonUtility.CalculateWinningPercentage(hostSeason)).MustNotHaveHappened();
            A.CallTo(() => _teamSeasonUtility.CalculatePythagoreanWinsAndLosses(guestSeason)).MustNotHaveHappened();
            A.CallTo(() => _teamSeasonUtility.CalculatePythagoreanWinsAndLosses(hostSeason)).MustNotHaveHappened();
        }

        [Test]
        public async Task ProcessGame_DoesNotCalculateWinningPercentagesOrPythagoreanWinsLossesWhenGuestSeasonAndHostSeasonAreNull()
        {
            var strategy = new AddGameStrategy(_gameUtility, _teamSeasonUtility, _teamSeasonRepository);

            var game = new Game
            {
                GuestName = "Guest",
                HostName = "Host"
            };

            TeamSeason guestSeason = null;
            A.CallTo(() => _teamSeasonRepository.GetTeamSeasonByTeamAndSeason(game.GuestName, A<int>.Ignored))
                .Returns(guestSeason);

            TeamSeason hostSeason = null;
            A.CallTo(() => _teamSeasonRepository.GetTeamSeasonByTeamAndSeason(game.HostName, A<int>.Ignored))
                .Returns(hostSeason);

            await strategy.ProcessGame(game);

            A.CallTo(() => _teamSeasonUtility.CalculateWinningPercentage(guestSeason)).MustNotHaveHappened();
            A.CallTo(() => _teamSeasonUtility.CalculateWinningPercentage(hostSeason)).MustNotHaveHappened();
            A.CallTo(() => _teamSeasonUtility.CalculatePythagoreanWinsAndLosses(guestSeason)).MustNotHaveHappened();
            A.CallTo(() => _teamSeasonUtility.CalculatePythagoreanWinsAndLosses(hostSeason)).MustNotHaveHappened();
        }

        [Test]
        public async Task ProcessGame_CalculatesGuestWinningPercentagesAndPythagoreanWinsLossesWhenGuestSeasonIsNotNull()
        {
            var strategy = new AddGameStrategy(_gameUtility, _teamSeasonUtility, _teamSeasonRepository);

            var game = new Game
            {
                GuestName = "Guest",
                HostName = "Host"
            };

            var guestSeason = new TeamSeason();
            A.CallTo(() => _teamSeasonRepository.GetTeamSeasonByTeamAndSeason(game.GuestName, A<int>.Ignored))
                .Returns(guestSeason);

            TeamSeason hostSeason = null;
            A.CallTo(() => _teamSeasonRepository.GetTeamSeasonByTeamAndSeason(game.HostName, A<int>.Ignored))
                .Returns(hostSeason);

            await strategy.ProcessGame(game);

            A.CallTo(() => _teamSeasonUtility.CalculateWinningPercentage(guestSeason)).MustHaveHappened();
            A.CallTo(() => _teamSeasonUtility.CalculateWinningPercentage(hostSeason)).MustNotHaveHappened();
            A.CallTo(() => _teamSeasonUtility.CalculatePythagoreanWinsAndLosses(guestSeason)).MustHaveHappened();
            A.CallTo(() => _teamSeasonUtility.CalculatePythagoreanWinsAndLosses(hostSeason)).MustNotHaveHappened();
        }

        [Test]
        public async Task ProcessGame_CalculatesHostWinningPercentagesAndPythagoreanWinsLossesWhenHostSeasonIsNotNull()
        {
            var strategy = new AddGameStrategy(_gameUtility, _teamSeasonUtility, _teamSeasonRepository);

            var game = new Game
            {
                GuestName = "Guest",
                HostName = "Host"
            };

            TeamSeason guestSeason = null;
            A.CallTo(() => _teamSeasonRepository.GetTeamSeasonByTeamAndSeason(game.GuestName, A<int>.Ignored))
                .Returns(guestSeason);

            var hostSeason = new TeamSeason();
            A.CallTo(() => _teamSeasonRepository.GetTeamSeasonByTeamAndSeason(game.HostName, A<int>.Ignored))
                .Returns(hostSeason);

            await strategy.ProcessGame(game);

            A.CallTo(() => _teamSeasonUtility.CalculateWinningPercentage(guestSeason)).MustNotHaveHappened();
            A.CallTo(() => _teamSeasonUtility.CalculateWinningPercentage(hostSeason)).MustHaveHappened();
            A.CallTo(() => _teamSeasonUtility.CalculatePythagoreanWinsAndLosses(guestSeason)).MustNotHaveHappened();
            A.CallTo(() => _teamSeasonUtility.CalculatePythagoreanWinsAndLosses(hostSeason)).MustHaveHappened();
        }
    }
}
