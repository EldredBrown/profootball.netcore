using EldredBrown.ProFootball.NETCore.Data.Repositories;
using FakeItEasy;
using NUnit.Framework;

namespace EldredBrown.ProFootball.NETCore.Services.Tests
{
    [TestFixture]
    public class ProcessGameStrategyFactoryTests
    {
        private ITeamSeasonRepository _teamSeasonRepository;

        [SetUp]
        public void Setup()
        {
            _teamSeasonRepository = A.Fake<ITeamSeasonRepository>();
        }

        [Test]
        public void CreateStrategy_CreatesAddGameStrategyWhenDirectionIsUp()
        {
            var factory = new ProcessGameStrategyFactory(_teamSeasonRepository);

            var strategy = factory.CreateStrategy(Direction.Up);

            Assert.IsInstanceOf<AddGameStrategy>(strategy);
        }

        [Test]
        public void CreateStrategy_CreatesSubtractGameStrategyWhenDirectionIsDown()
        {
            var factory = new ProcessGameStrategyFactory(_teamSeasonRepository);

            var strategy = factory.CreateStrategy(Direction.Down);

            Assert.IsInstanceOf<SubtractGameStrategy>(strategy);
        }

        [Test]
        public void CreateStrategy_CreatesNullGameStrategyWhenDirectionIsNotUpNorDown()
        {
            var factory = new ProcessGameStrategyFactory(_teamSeasonRepository);

            var strategy = factory.CreateStrategy((Direction)3);

            Assert.IsInstanceOf<NullGameStrategy>(strategy);
        }
    }
}
