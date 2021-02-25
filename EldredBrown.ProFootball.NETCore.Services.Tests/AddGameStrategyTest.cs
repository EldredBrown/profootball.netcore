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
        private ITeamSeasonRepository _teamSeasonRepository;

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

            TeamSeason guestSeason = null;
            A.CallTo(() => _teamSeasonRepository.GetTeamSeasonByTeamAndSeason(gameDecorator.GuestName, A<int>.Ignored))
                .Returns(guestSeason);

            TeamSeason hostSeason = null;
            A.CallTo(() => _teamSeasonRepository.GetTeamSeasonByTeamAndSeason(gameDecorator.HostName, A<int>.Ignored))
                .Returns(hostSeason);

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

            TeamSeason guestSeason = null;
            A.CallTo(() => _teamSeasonRepository.GetTeamSeasonByTeamAndSeason(gameDecorator.GuestName, A<int>.Ignored))
                .Returns(guestSeason);

            TeamSeason hostSeason = null;
            A.CallTo(() => _teamSeasonRepository.GetTeamSeasonByTeamAndSeason(gameDecorator.HostName, A<int>.Ignored))
                .Returns(hostSeason);

            TeamSeason winnerSeason = null;
            A.CallTo(() => _teamSeasonRepository.GetTeamSeasonByTeamAndSeason(gameDecorator.WinnerName, A<int>.Ignored))
                .Returns(winnerSeason);

            TeamSeason loserSeason = null;
            A.CallTo(() => _teamSeasonRepository.GetTeamSeasonByTeamAndSeason(gameDecorator.LoserName, A<int>.Ignored))
                .Returns(loserSeason);

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
