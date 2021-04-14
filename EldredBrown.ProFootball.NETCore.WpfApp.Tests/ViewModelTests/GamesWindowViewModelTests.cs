using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using EldredBrown.ProFootball.NETCore.Data.Entities;
using EldredBrown.ProFootball.NETCore.Data.Repositories;
using EldredBrown.ProFootball.NETCore.Services;
using EldredBrown.ProFootball.NETCore.WpfApp.ViewModels;
using FakeItEasy;
using Shouldly;
using Xunit;

namespace EldredBrown.ProFootball.NETCore.WpfApp.Tests.ViewModelTests
{
    public class GamesWindowViewModelTests
    {
        [Fact]
        public void WeekSetter_WhenValueDoesNotEqualWeek_ShouldAssignValueToWeek()
        {
            // Arrange
            var gameRepository = A.Fake<IGameRepository>();
            var seasonRepository = A.Fake<ISeasonRepository>();
            var gameService = A.Fake<IGameService>();
            var testObject = new GamesWindowViewModel(gameRepository, seasonRepository, gameService);

            // Act
            var week = 1;
            testObject.Week = week;

            // Assert
            testObject.Week.ShouldBe(week);
        }

        [Fact]
        public void GuestNameSetter_WhenValueDoesNotEqualGuestName_ShouldAssignValueToGuestName()
        {
            // Arrange
            var gameRepository = A.Fake<IGameRepository>();
            var seasonRepository = A.Fake<ISeasonRepository>();
            var gameService = A.Fake<IGameService>();
            var testObject = new GamesWindowViewModel(gameRepository, seasonRepository, gameService);

            // Act
            var guestName = "Guest";
            testObject.GuestName = guestName;

            // Assert
            testObject.GuestName.ShouldBe(guestName);
        }

        [Fact]
        public void GuestScoreSetter_WhenValueDoesNotEqualGuestScore_ShouldAssignValueToGuestScore()
        {
            // Arrange
            var gameRepository = A.Fake<IGameRepository>();
            var seasonRepository = A.Fake<ISeasonRepository>();
            var gameService = A.Fake<IGameService>();
            var testObject = new GamesWindowViewModel(gameRepository, seasonRepository, gameService);

            // Act
            var guestScore = 7;
            testObject.GuestScore = guestScore;

            // Assert
            testObject.GuestScore.ShouldBe(guestScore);
        }

        [Fact]
        public void HostNameSetter_WhenValueDoesNotEqualHostName_ShouldAssignValueToHostName()
        {
            // Arrange
            var gameRepository = A.Fake<IGameRepository>();
            var seasonRepository = A.Fake<ISeasonRepository>();
            var gameService = A.Fake<IGameService>();
            var testObject = new GamesWindowViewModel(gameRepository, seasonRepository, gameService);

            // Act
            var hostName = "Host";
            testObject.HostName = hostName;

            // Assert
            testObject.HostName.ShouldBe(hostName);
        }

        [Fact]
        public void HostScoreSetter_WhenValueDoesNotEqualHostScore_ShouldAssignValueToHostScore()
        {
            // Arrange
            var gameRepository = A.Fake<IGameRepository>();
            var seasonRepository = A.Fake<ISeasonRepository>();
            var gameService = A.Fake<IGameService>();
            var testObject = new GamesWindowViewModel(gameRepository, seasonRepository, gameService);

            // Act
            var hostScore = 7;
            testObject.HostScore = hostScore;

            // Assert
            testObject.HostScore.ShouldBe(hostScore);
        }

        [Fact]
        public void IsPlayoffSetter_WhenValueDoesNotEqualIsPlayoff_ShouldAssignValueToIsPlayoff()
        {
            // Arrange
            var gameRepository = A.Fake<IGameRepository>();
            var seasonRepository = A.Fake<ISeasonRepository>();
            var gameService = A.Fake<IGameService>();
            var testObject = new GamesWindowViewModel(gameRepository, seasonRepository, gameService);

            // Act
            testObject.IsPlayoff = true;

            // Assert
            testObject.IsPlayoff.ShouldBeTrue();
        }

        [Fact]
        public void IsPlayoffEnabledSetter_WhenValueDoesNotEqualIsPlayoffEnabled_ShouldAssignValueToIsPlayoffEnabled()
        {
            // Arrange
            var gameRepository = A.Fake<IGameRepository>();
            var seasonRepository = A.Fake<ISeasonRepository>();
            var gameService = A.Fake<IGameService>();
            var testObject = new GamesWindowViewModel(gameRepository, seasonRepository, gameService);

            // Act
            testObject.IsPlayoffEnabled = true;

            // Assert
            testObject.IsPlayoffEnabled.ShouldBeTrue();
        }

        [Fact]
        public void NotesSetter_WhenValueDoesNotEqualNotes_ShouldAssignValueToNotes()
        {
            // Arrange
            var gameRepository = A.Fake<IGameRepository>();
            var seasonRepository = A.Fake<ISeasonRepository>();
            var gameService = A.Fake<IGameService>();
            var testObject = new GamesWindowViewModel(gameRepository, seasonRepository, gameService);

            // Act
            var notes = "Notes";
            testObject.Notes = notes;

            // Assert
            testObject.Notes.ShouldBe<string>(notes);
        }

        [Fact]
        public void AddGameControlVisibilitySetter_WhenValueDoesNotEqualAddGameControlVisibility_ShouldAssignValueToAddGameControlVisibility()
        {
            // Arrange
            var gameRepository = A.Fake<IGameRepository>();
            var seasonRepository = A.Fake<ISeasonRepository>();
            var gameService = A.Fake<IGameService>();
            var testObject = new GamesWindowViewModel(gameRepository, seasonRepository, gameService);

            // Act
            var visibility = Visibility.Hidden;
            testObject.AddGameControlVisibility = visibility;

            // Assert
            testObject.AddGameControlVisibility.ShouldBe(visibility);
        }

        [Fact]
        public void EditGameControlVisibilitySetter_WhenValueDoesNotEqualEditGameControlVisibility_ShouldAssignValueToEditGameControlVisibility()
        {
            // Arrange
            var gameRepository = A.Fake<IGameRepository>();
            var seasonRepository = A.Fake<ISeasonRepository>();
            var gameService = A.Fake<IGameService>();
            var testObject = new GamesWindowViewModel(gameRepository, seasonRepository, gameService);

            // Act
            var visibility = Visibility.Hidden;
            testObject.EditGameControlVisibility = visibility;

            // Assert
            testObject.EditGameControlVisibility.ShouldBe(visibility);
        }

        [Fact]
        public void DeleteGameControlVisibilitySetter_WhenValueDoesNotEqualDeleteGameControlVisibility_ShouldAssignValueToDeleteGameControlVisibility()
        {
            // Arrange
            var gameRepository = A.Fake<IGameRepository>();
            var seasonRepository = A.Fake<ISeasonRepository>();
            var gameService = A.Fake<IGameService>();
            var testObject = new GamesWindowViewModel(gameRepository, seasonRepository, gameService);

            // Act
            var visibility = Visibility.Hidden;
            testObject.DeleteGameControlVisibility = visibility;

            // Assert
            testObject.DeleteGameControlVisibility.ShouldBe(visibility);
        }

        [Fact]
        public void GamesSetter_WhenValueIsNull_ShouldThrowArgumentNullException()
        {
            // Arrange
            var gameRepository = A.Fake<IGameRepository>();
            var seasonRepository = A.Fake<ISeasonRepository>();
            var gameService = A.Fake<IGameService>();
            var testObject = new GamesWindowViewModel(gameRepository, seasonRepository, gameService);

            // Act
            Func<ReadOnlyCollection<Game>> func = () => testObject.Games = null;

            // Assert
            var ex = func.ShouldThrow<ArgumentNullException>();
            ex.ParamName.ShouldBe<string>($"{testObject.GetType()}.{nameof(testObject.Games)}");
        }

        [Fact]
        public void GamesSetter_WhenValueDoesNotEqualGames_ShouldAssignValueToGames()
        {
            // Arrange
            var gameRepository = A.Fake<IGameRepository>();
            var seasonRepository = A.Fake<ISeasonRepository>();
            var gameService = A.Fake<IGameService>();
            var testObject = new GamesWindowViewModel(gameRepository, seasonRepository, gameService);

            // Act
            var games = new ReadOnlyCollection<Game>(new List<Game>());
            Func <ReadOnlyCollection<Game>> func = () => testObject.Games = games;

            // Assert
            func.ShouldNotThrow();
            testObject.Games.ShouldBeOfType<ReadOnlyCollection<Game>>();
            testObject.Games.ShouldBe(games);
        }

        [Fact]
        public void SelectedGameSetter_WhenValueDoesNotEqualSelectedGame_ShouldAssignValueToSelectedGame()
        {
            // Arrange
            var gameRepository = A.Fake<IGameRepository>();
            var seasonRepository = A.Fake<ISeasonRepository>();
            var gameService = A.Fake<IGameService>();
            var testObject = new GamesWindowViewModel(gameRepository, seasonRepository, gameService);

            // Act
            var game = new Game();
            testObject.SelectedGame = game;

            // Assert
            testObject.SelectedGame.ShouldBe(game);
        }

        [Fact]
        public void IsGamesReadOnlySetter_WhenValueDoesNotEqualIsGamesReadOnly_ShouldAssignValueToIsGamesReadOnly()
        {
            // Arrange
            var gameRepository = A.Fake<IGameRepository>();
            var seasonRepository = A.Fake<ISeasonRepository>();
            var gameService = A.Fake<IGameService>();
            var testObject = new GamesWindowViewModel(gameRepository, seasonRepository, gameService);

            // Act
            testObject.IsGamesReadOnly = true;

            // Assert
            testObject.IsGamesReadOnly.ShouldBeTrue();
        }

        [Fact]
        public void IsShowAllGamesEnabledSetter_WhenValueDoesNotEqualIsShowAllGamesEnabled_ShouldAssignValueToIsShowAllGamesEnabled()
        {
            // Arrange
            var gameRepository = A.Fake<IGameRepository>();
            var seasonRepository = A.Fake<ISeasonRepository>();
            var gameService = A.Fake<IGameService>();
            var testObject = new GamesWindowViewModel(gameRepository, seasonRepository, gameService);

            // Act
            testObject.IsShowAllGamesEnabled = true;

            // Assert
            testObject.IsShowAllGamesEnabled.ShouldBeTrue();
        }
    }
}
