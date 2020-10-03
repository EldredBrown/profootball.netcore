using System.Threading.Tasks;
using EldredBrown.ProFootball.NETCore.Data.Entities;
using EldredBrown.ProFootball.NETCore.Data.Repositories;
using FakeItEasy;
using NUnit.Framework;

namespace EldredBrown.ProFootball.NETCore.Services.Tests
{
    [TestFixture]
    public class AddGameStrategyTests
    {
        private ITeamSeasonRepository _teamSeasonRepository;

        [SetUp]
        public void Setup()
        {
            _teamSeasonRepository = A.Fake<ITeamSeasonRepository>();
        }

        [Test]
        public async Task ProcessGame_UpdatesTiesForTeamSeasonsWhenGameIsATie()
        {
            var strategy = new AddGameStrategy(_teamSeasonRepository);

            var game = new Game
            {
                GuestName = "Guest",
                GuestScore = 3,
                HostName = "Host",
                HostScore = 3
            };

            await strategy.ProcessGame(game);


            var seasonYear = game.SeasonYear;

            // Update games for the guest's season and the host's season.
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
            var strategy = new AddGameStrategy(_teamSeasonRepository);

            var game = new Game
            {
                GuestName = "Guest",
                GuestScore = 3,
                HostName = "Host",
                HostScore = 7,
                WinnerName = "Host",
                LoserName = "Guest"
            };

            await strategy.ProcessGame(game);

            var seasonYear = game.SeasonYear;

            // Update games for the guest's season and the host's season.
            A.CallTo(() => _teamSeasonRepository.GetTeamSeasonByTeamAndSeason(game.GuestName, seasonYear))
                .MustHaveHappenedTwiceExactly();
            A.CallTo(() => _teamSeasonRepository.GetTeamSeasonByTeamAndSeason(game.HostName, seasonYear))
                .MustHaveHappenedTwiceExactly();
        }
    }
}
