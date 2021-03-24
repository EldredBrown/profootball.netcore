using System;
using System.Threading.Tasks;
using EldredBrown.ProFootball.NETCore.Data.Decorators;
using EldredBrown.ProFootball.NETCore.Data.Repositories;
using FakeItEasy;
using Xunit;

namespace EldredBrown.ProFootball.NETCore.Services.Tests
{
    public class ProcessGameStrategyBaseTest
    {
        private readonly ITeamSeasonRepository _teamSeasonRepository;

        public ProcessGameStrategyBaseTest()
        {
            _teamSeasonRepository = A.Fake<ITeamSeasonRepository>();
        }

        [Fact]
        public async Task ProcessGame_WhenGameDecoratorArgIsNull_ShouldThrowArgumentNullException()
        {
            // Arrange
            var strategy = new ProcessGameStrategyBase(_teamSeasonRepository);

            GameDecorator gameDecorator = null;

            // Act
            Func<Task> func = new Func<Task>(async () => await strategy.ProcessGame(gameDecorator));

            // Assert
            await Assert.ThrowsAsync<ArgumentNullException>(func);
        }

        [Fact]
        public async Task ProcessGame_WhenGameDecoratorArgIsNotNull_ShouldProcessGame()
        {
            // Arrange
            var strategy = new ProcessGameStrategyBase(_teamSeasonRepository);

            var gameDecorator = A.Fake<IGameDecorator>();
            gameDecorator.GuestName = "Guest";
            gameDecorator.HostName = "Host";

            var seasonYear = gameDecorator.SeasonYear;

            // Act
            try
            {
                await strategy.ProcessGame(gameDecorator);
            }
            catch (Exception)
            {
                // Do nothing.
            }

            // Assert
            A.CallTo(() => _teamSeasonRepository.GetTeamSeasonByTeamAndSeason(gameDecorator.GuestName, seasonYear))
                .MustHaveHappenedOnceExactly();
            A.CallTo(() => _teamSeasonRepository.GetTeamSeasonByTeamAndSeason(gameDecorator.HostName, seasonYear))
                .MustHaveHappenedOnceExactly();
        }
    }
}
