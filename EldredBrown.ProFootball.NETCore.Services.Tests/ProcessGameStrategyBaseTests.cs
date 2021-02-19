using System;
using System.Threading.Tasks;
using EldredBrown.ProFootball.NETCore.Data.Decorators;
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
        public async Task ProcessGame_ProcessesGameWhenGameArgIsPassed()
        {
            var strategy = new ProcessGameStrategyBase(_teamSeasonRepository);

            var gameDecorator = A.Fake<IGameDecorator>();
            gameDecorator.GuestName = "Guest";
            gameDecorator.HostName = "Host";

            var seasonYear = gameDecorator.SeasonYear;

            try
            {
                await strategy.ProcessGame(gameDecorator);
            }
            catch (Exception)
            {
                // Do nothing.
            }

            A.CallTo(() => _teamSeasonRepository.GetTeamSeasonByTeamAndSeason(gameDecorator.GuestName, seasonYear))
                .MustHaveHappenedOnceExactly();
            A.CallTo(() => _teamSeasonRepository.GetTeamSeasonByTeamAndSeason(gameDecorator.HostName, seasonYear))
                .MustHaveHappenedOnceExactly();
        }
    }
}
