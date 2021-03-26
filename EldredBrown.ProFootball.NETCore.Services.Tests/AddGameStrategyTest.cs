using System.Threading.Tasks;
using EldredBrown.ProFootball.NETCore.Data.Decorators;
using EldredBrown.ProFootball.NETCore.Data.Entities;
using EldredBrown.ProFootball.NETCore.Data.Repositories;
using FakeItEasy;
using Xunit;

namespace EldredBrown.ProFootball.NETCore.Services.Tests
{
    public class AddGameStrategyTest
    {
        private readonly ITeamSeasonRepository _teamSeasonRepository;

        public AddGameStrategyTest()
        {
            _teamSeasonRepository = A.Fake<ITeamSeasonRepository>();
        }

        [Fact]
        public async Task ProcessGame_WhenGameIsATie_ShouldUpdateTiesForTeamSeasons()
        {
            // Arrange
            var strategy = new AddGameStrategy(_teamSeasonRepository);

            var gameDecorator = A.Fake<IGameDecorator>();
            gameDecorator.GuestName = "Guest";
            gameDecorator.HostName = "Host";
            gameDecorator.WinnerName = "Winner";
            gameDecorator.LoserName = "Loser";
            gameDecorator.SeasonYear = 1920;
            A.CallTo(() => gameDecorator.IsTie()).Returns(true);

            A.CallTo(() => _teamSeasonRepository.GetTeamSeasonByTeamAndSeason(A<string>.Ignored, A<int>.Ignored))
                .Returns<TeamSeason?>(null);

            // Act
            await strategy.ProcessGame(gameDecorator);

            // Assert
            var seasonYear = gameDecorator.SeasonYear;

            A.CallTo(() => _teamSeasonRepository.GetTeamSeasonByTeamAndSeason(gameDecorator.GuestName, seasonYear))
                .MustHaveHappenedOnceExactly();
            A.CallTo(() => _teamSeasonRepository.GetTeamSeasonByTeamAndSeason(gameDecorator.HostName, seasonYear))
                .MustHaveHappenedOnceExactly();
            A.CallTo(() => _teamSeasonRepository.GetTeamSeasonByTeamAndSeason(gameDecorator.WinnerName, seasonYear))
                .MustNotHaveHappened();
            A.CallTo(() => _teamSeasonRepository.GetTeamSeasonByTeamAndSeason(gameDecorator.LoserName, seasonYear))
                .MustNotHaveHappened();
        }

        [Fact]
        public async Task ProcessGame_WhenGameIsNotATie_ShouldUpdateWinsAndLossesForTeamSeasons()
        {
            // Arrange
            var strategy = new AddGameStrategy(_teamSeasonRepository);

            var gameDecorator = A.Fake<IGameDecorator>();
            gameDecorator.GuestName = "Guest";
            gameDecorator.HostName = "Host";
            gameDecorator.WinnerName = "Winner";
            gameDecorator.LoserName = "Loser";
            gameDecorator.SeasonYear = 1920;
            A.CallTo(() => gameDecorator.IsTie()).Returns(false);

            A.CallTo(() => _teamSeasonRepository.GetTeamSeasonByTeamAndSeason(A<string>.Ignored, A<int>.Ignored))
                .Returns<TeamSeason?>(null);

            // Act
            await strategy.ProcessGame(gameDecorator);

            // Assert
            var seasonYear = gameDecorator.SeasonYear;

            A.CallTo(() => _teamSeasonRepository.GetTeamSeasonByTeamAndSeason(gameDecorator.GuestName, seasonYear))
                .MustHaveHappened();
            A.CallTo(() => _teamSeasonRepository.GetTeamSeasonByTeamAndSeason(gameDecorator.HostName, seasonYear))
                .MustHaveHappened();
            A.CallTo(() => _teamSeasonRepository.GetTeamSeasonByTeamAndSeason(gameDecorator.WinnerName, seasonYear))
                .MustHaveHappened();
            A.CallTo(() => _teamSeasonRepository.GetTeamSeasonByTeamAndSeason(gameDecorator.LoserName, seasonYear))
                .MustHaveHappened();
        }
    }
}
