using EldredBrown.ProFootball.NETCore.Data.Repositories;
using FakeItEasy;
using Shouldly;
using Xunit;

namespace EldredBrown.ProFootball.NETCore.Services.Tests
{
    public class ProcessGameStrategyFactoryTest
    {
        private readonly ITeamSeasonRepository _teamSeasonRepository;

        public ProcessGameStrategyFactoryTest()
        {
            _teamSeasonRepository = A.Fake<ITeamSeasonRepository>();
        }

        [Fact]
        public void CreateStrategy_WhenDirectionIsUp_ShouldCreateAddGameStrategy()
        {
            // Arrange
            var factory = new ProcessGameStrategyFactory(_teamSeasonRepository);

            // Act
            var strategy = factory.CreateStrategy(Direction.Up);

            // Assert
            strategy.ShouldBeOfType<AddGameStrategy>();
        }

        [Fact]
        public void CreateStrategy_WhenDirectionIsDown_ShouldCreateSubtractGameStrategy()
        {
            // Arrange
            var factory = new ProcessGameStrategyFactory(_teamSeasonRepository);

            // Act
            var strategy = factory.CreateStrategy(Direction.Down);

            // Assert
            strategy.ShouldBeOfType<SubtractGameStrategy>();
        }

        [Fact]
        public void CreateStrategy_WhenDirectionIsNotUpNorDown_ShouldCreateNullGameStrategy()
        {
            // Arrange
            var factory = new ProcessGameStrategyFactory(_teamSeasonRepository);

            // Act
            var strategy = factory.CreateStrategy((Direction)3);

            // Assert
            strategy.ShouldBeOfType<NullGameStrategy>();
        }
    }
}
