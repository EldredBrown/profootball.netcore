using System;
using System.Threading.Tasks;
using EldredBrown.ProFootball.NETCore.Data.Entities;
using EldredBrown.ProFootball.NETCore.Data.Repositories;
using FakeItEasy;
using NUnit.Framework;

namespace EldredBrown.ProFootball.NETCore.Services.Tests
{
    [TestFixture]
    public class ProcessGameStrategyBaseTests
    {
        private ITeamSeasonRepository _teamSeasonRepository;

        [SetUp]
        public void Setup()
        {
            _teamSeasonRepository = A.Fake<ITeamSeasonRepository>();
        }

        [Test]
        public async Task ProcessGame_ProcessesGameWhenGamePassed()
        {
            var strategy = new ProcessGameStrategyBase(_teamSeasonRepository);

            var game = new Game
            {
                GuestName = "Guest",
                HostName = "Host"
            };

            try
            {
                await strategy.ProcessGame(game);
            }
            catch (Exception)
            {
                // Do nothing.
            }

            var seasonYear = game.SeasonYear;

            A.CallTo(() => _teamSeasonRepository.GetTeamSeasonByTeamAndSeason(game.GuestName, seasonYear))
                .MustHaveHappenedOnceExactly();
            A.CallTo(() => _teamSeasonRepository.GetTeamSeasonByTeamAndSeason(game.HostName, seasonYear))
                .MustHaveHappenedOnceExactly();
        }
    }
}
