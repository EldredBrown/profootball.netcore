using System;
using System.Threading.Tasks;
using EldredBrown.ProFootball.NETCore.Data.Entities;
using EldredBrown.ProFootball.NETCore.Data.Repositories;
using EldredBrown.ProFootball.NETCore.Data.Utilities;
using FakeItEasy;
using NUnit.Framework;

namespace EldredBrown.ProFootball.NETCore.Services.Tests
{
    [TestFixture]
    public class ProcessGameStrategyBaseTests
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
        public async Task ProcessGame_ProcessesGameWhenGameArgIsPassed()
        {
            var strategy = new ProcessGameStrategyBase(_gameUtility, _teamSeasonUtility, _teamSeasonRepository);

            var game = new Game
            {
                GuestName = "Guest",
                HostName = "Host"
            };

            var seasonYear = game.SeasonYear;

            try
            {
                await strategy.ProcessGame(game);
            }
            catch (Exception)
            {
                // Do nothing.
            }

            A.CallTo(() => _teamSeasonRepository.GetTeamSeasonByTeamAndSeason(game.GuestName, seasonYear))
                .MustHaveHappenedOnceExactly();
            A.CallTo(() => _teamSeasonRepository.GetTeamSeasonByTeamAndSeason(game.HostName, seasonYear))
                .MustHaveHappenedOnceExactly();
        }
    }
}
