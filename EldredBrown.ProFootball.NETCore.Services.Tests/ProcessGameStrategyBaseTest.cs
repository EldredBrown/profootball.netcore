using System;
using System.Threading.Tasks;
using FakeItEasy;
using Shouldly;
using Xunit;
using EldredBrown.ProFootball.NETCore.Data.Decorators;
using EldredBrown.ProFootball.NETCore.Data.Repositories;

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

            GameDecorator? gameDecorator = null;

            // Act
            Func<Task> func = new Func<Task>(async () => await strategy.ProcessGameAsync(gameDecorator!));

            // Assert
            await func.ShouldThrowAsync<ArgumentNullException>();
        }

        [Fact]
        public async Task ProcessGame_WhenGameDecoratorArgIsNotNull_ShouldProcessGame()
        {
            // Arrange
            var strategy = new ProcessGameStrategyBase(_teamSeasonRepository);

            var gameDecorator = A.Fake<IGameDecorator>();
            gameDecorator.GuestName = "Guest";
            gameDecorator.HostName = "Host";

            // Act
            try
            {
                await strategy.ProcessGameAsync(gameDecorator);
            }
            catch (NotImplementedException)
            {
                // This test case calls a base class method that is implemented only in subclasses, thereby throwing a
                // NotImplementedException. The exception has no impact on what I expect to happen here, so it can be
                // ignored.
            }

            // Assert
            var seasonYear = gameDecorator.SeasonYear;

            A.CallTo(() => _teamSeasonRepository.GetTeamSeasonByTeamAndSeasonAsync(gameDecorator.GuestName, seasonYear))
                .MustHaveHappenedOnceExactly();
            A.CallTo(() => _teamSeasonRepository.GetTeamSeasonByTeamAndSeasonAsync(gameDecorator.HostName, seasonYear))
                .MustHaveHappenedOnceExactly();
        }
    }
}
