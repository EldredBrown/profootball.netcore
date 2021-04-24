using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using EldredBrown.ProFootball.NETCore.Data.Entities;
using EldredBrown.ProFootball.NETCore.Data.Repositories;
using EldredBrown.ProFootball.NETCore.Services;
using EldredBrown.ProFootball.NETCore.WpfApp.Windows;
using EldredBrown.ProFootball.NETCore.WpfApp.Windows.GameFinder;
using EldredBrown.ProFootball.NETCore.WpfApp.Windows.Games;
using EldredBrown.ProFootball.WpfApp;
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
            var gameFinderWindowFactory = A.Fake<IWindowFactory>();
            var testObject =
                new GamesWindowViewModel(gameRepository, seasonRepository, gameService, gameFinderWindowFactory);

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
            var gameFinderWindowFactory = A.Fake<IWindowFactory>();
            var testObject =
                new GamesWindowViewModel(gameRepository, seasonRepository, gameService, gameFinderWindowFactory);

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
            var gameFinderWindowFactory = A.Fake<IWindowFactory>();
            var testObject =
                new GamesWindowViewModel(gameRepository, seasonRepository, gameService, gameFinderWindowFactory);

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
            var gameFinderWindowFactory = A.Fake<IWindowFactory>();
            var testObject =
                new GamesWindowViewModel(gameRepository, seasonRepository, gameService, gameFinderWindowFactory);

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
            var gameFinderWindowFactory = A.Fake<IWindowFactory>();
            var testObject =
                new GamesWindowViewModel(gameRepository, seasonRepository, gameService, gameFinderWindowFactory);

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
            var gameFinderWindowFactory = A.Fake<IWindowFactory>();
            var testObject =
                new GamesWindowViewModel(gameRepository, seasonRepository, gameService, gameFinderWindowFactory);

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
            var gameFinderWindowFactory = A.Fake<IWindowFactory>();
            var testObject =
                new GamesWindowViewModel(gameRepository, seasonRepository, gameService, gameFinderWindowFactory);

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
            var gameFinderWindowFactory = A.Fake<IWindowFactory>();
            var testObject =
                new GamesWindowViewModel(gameRepository, seasonRepository, gameService, gameFinderWindowFactory);

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
            var gameFinderWindowFactory = A.Fake<IWindowFactory>();
            var testObject =
                new GamesWindowViewModel(gameRepository, seasonRepository, gameService, gameFinderWindowFactory);

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
            var gameFinderWindowFactory = A.Fake<IWindowFactory>();
            var testObject =
                new GamesWindowViewModel(gameRepository, seasonRepository, gameService, gameFinderWindowFactory);

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
            var gameFinderWindowFactory = A.Fake<IWindowFactory>();
            var testObject =
                new GamesWindowViewModel(gameRepository, seasonRepository, gameService, gameFinderWindowFactory);

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
            var gameFinderWindowFactory = A.Fake<IWindowFactory>();
            var testObject =
                new GamesWindowViewModel(gameRepository, seasonRepository, gameService, gameFinderWindowFactory);

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
            var gameFinderWindowFactory = A.Fake<IWindowFactory>();
            var testObject =
                new GamesWindowViewModel(gameRepository, seasonRepository, gameService, gameFinderWindowFactory);

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
            var gameFinderWindowFactory = A.Fake<IWindowFactory>();
            var testObject =
                new GamesWindowViewModel(gameRepository, seasonRepository, gameService, gameFinderWindowFactory);

            // Act
            var game = new Game();
            testObject.SelectedGame = game;

            // Assert
            testObject.SelectedGame.ShouldBe(game);
        }

        [Fact]
        public void SelectedGameSetter_WhenValueDoesNotEqualSelectedGameAndIsNull_ShouldAssignValueToSelectedGameAndClearDataEntryControls()
        {
            // Arrange
            var gameRepository = A.Fake<IGameRepository>();
            var seasonRepository = A.Fake<ISeasonRepository>();
            var gameService = A.Fake<IGameService>();
            var gameFinderWindowFactory = A.Fake<IWindowFactory>();
            var testObject =
                new GamesWindowViewModel(gameRepository, seasonRepository, gameService, gameFinderWindowFactory)
                {
                    GuestName = "Guest",
                    GuestScore = 1,
                    HostName = "Host",
                    HostScore = 1,
                    IsPlayoff = true,
                    Notes = "Notes",
                    AddGameControlVisibility = Visibility.Hidden,
                    EditGameControlVisibility = Visibility.Visible,
                    DeleteGameControlVisibility = Visibility.Visible,
                    SelectedGame = new Game()
                };

            // Act
            testObject.SelectedGame = null;

            // Assert
            testObject.SelectedGame.ShouldBeNull();
            testObject.GuestName.ShouldBe<string>(string.Empty);
            testObject.GuestScore.ShouldBe(0);
            testObject.HostName.ShouldBe<string>(string.Empty);
            testObject.HostScore.ShouldBe(0);
            testObject.IsPlayoff.ShouldBeFalse();
            testObject.Notes.ShouldBe<string>(string.Empty);
            testObject.AddGameControlVisibility.ShouldBe(Visibility.Visible);
            testObject.EditGameControlVisibility.ShouldBe(Visibility.Hidden);
            testObject.DeleteGameControlVisibility.ShouldBe(Visibility.Hidden);
        }

        [Fact]
        public void SelectedGameSetter_WhenValueDoesNotEqualSelectedGameAndIsNotNull_ShouldAssignValueToSelectedGameAndPopulateDataEntryControls()
        {
            // Arrange
            var gameRepository = A.Fake<IGameRepository>();
            var seasonRepository = A.Fake<ISeasonRepository>();
            var gameService = A.Fake<IGameService>();
            var gameFinderWindowFactory = A.Fake<IWindowFactory>();
            var testObject =
                new GamesWindowViewModel(gameRepository, seasonRepository, gameService, gameFinderWindowFactory)
                {
                    GuestName = string.Empty,
                    GuestScore = 0,
                    HostName = string.Empty,
                    HostScore = 0,
                    IsPlayoff = true,
                    Notes = string.Empty,
                    AddGameControlVisibility = Visibility.Visible,
                    EditGameControlVisibility = Visibility.Hidden,
                    DeleteGameControlVisibility = Visibility.Hidden,
                    SelectedGame = new Game()
                };

            // Act
            var guestName = "Guest";
            var guestScore = 1;
            var hostName = "Host";
            var hostScore = 1;
            var notes = "Notes";
            var game = new Game
            {
                GuestName = guestName,
                GuestScore = guestScore,
                HostName = hostName,
                HostScore = hostScore,
                IsPlayoff = false,
                Notes = notes
            };
            testObject.SelectedGame = game;

            // Assert
            testObject.SelectedGame.ShouldBe(game);
            testObject.GuestName.ShouldBe<string>(guestName);
            testObject.GuestScore.ShouldBe(guestScore);
            testObject.HostName.ShouldBe<string>(hostName);
            testObject.HostScore.ShouldBe(hostScore);
            testObject.IsPlayoff.ShouldBeFalse();
            testObject.Notes.ShouldBe<string>(notes);
            testObject.AddGameControlVisibility.ShouldBe(Visibility.Hidden);
            testObject.EditGameControlVisibility.ShouldBe(Visibility.Visible);
            testObject.DeleteGameControlVisibility.ShouldBe(Visibility.Visible);
        }

        [Fact]
        public void IsGamesReadOnlySetter_WhenValueDoesNotEqualIsGamesReadOnly_ShouldAssignValueToIsGamesReadOnly()
        {
            // Arrange
            var gameRepository = A.Fake<IGameRepository>();
            var seasonRepository = A.Fake<ISeasonRepository>();
            var gameService = A.Fake<IGameService>();
            var gameFinderWindowFactory = A.Fake<IWindowFactory>();
            var testObject =
                new GamesWindowViewModel(gameRepository, seasonRepository, gameService, gameFinderWindowFactory);

            // Act
            testObject.IsGamesReadOnly = true;

            // Assert
            testObject.IsGamesReadOnly.ShouldBeTrue();
        }

        [Fact]
        public void ShowAllGamesEnabledSetter_WhenValueDoesNotEqualIsShowAllGamesEnabled_ShouldAssignValueToIsShowAllGamesEnabled()
        {
            // Arrange
            var gameRepository = A.Fake<IGameRepository>();
            var seasonRepository = A.Fake<ISeasonRepository>();
            var gameService = A.Fake<IGameService>();
            var gameFinderWindowFactory = A.Fake<IWindowFactory>();
            var testObject =
                new GamesWindowViewModel(gameRepository, seasonRepository, gameService, gameFinderWindowFactory);

            // Act
            testObject.ShowAllGamesEnabled = true;

            // Assert
            testObject.ShowAllGamesEnabled.ShouldBeTrue();
        }

        [Fact]
        public void AddGameCommand_WhenGuestNameIsNull_ShouldShowErrorMessageBox()
        {
            // Arrange
            var gameRepository = A.Fake<IGameRepository>();
            var seasonRepository = A.Fake<ISeasonRepository>();
            var gameService = A.Fake<IGameService>();
            var gameFinderWindowFactory = A.Fake<IWindowFactory>();
            var testObject =
                new GamesWindowViewModel(gameRepository, seasonRepository, gameService, gameFinderWindowFactory)
                {
                    GuestName = null
                };

            // Act
            testObject.AddGameCommand.Execute(null);

            // Assert
            A.CallTo(() => gameService.AddGame(A<Game>.Ignored)).MustNotHaveHappened();
            A.CallTo(() => gameRepository.GetGamesBySeason(A<int>.Ignored)).MustNotHaveHappened();
        }

        [Fact]
        public void AddGameCommand_WhenGuestNameIsEmpty_ShouldShowErrorMessageBox()
        {
            // Arrange
            var gameRepository = A.Fake<IGameRepository>();
            var seasonRepository = A.Fake<ISeasonRepository>();
            var gameService = A.Fake<IGameService>();
            var gameFinderWindowFactory = A.Fake<IWindowFactory>();
            var testObject =
                new GamesWindowViewModel(gameRepository, seasonRepository, gameService, gameFinderWindowFactory)
                {
                    GuestName = string.Empty
                };

            // Act
            testObject.AddGameCommand.Execute(null);

            // Assert
            A.CallTo(() => gameService.AddGame(A<Game>.Ignored)).MustNotHaveHappened();
            A.CallTo(() => gameRepository.GetGamesBySeason(A<int>.Ignored)).MustNotHaveHappened();
        }

        [Fact]
        public void AddGameCommand_WhenGuestNameIsWhiteSpace_ShouldShowErrorMessageBox()
        {
            // Arrange
            var gameRepository = A.Fake<IGameRepository>();
            var seasonRepository = A.Fake<ISeasonRepository>();
            var gameService = A.Fake<IGameService>();
            var gameFinderWindowFactory = A.Fake<IWindowFactory>();
            var testObject =
                new GamesWindowViewModel(gameRepository, seasonRepository, gameService, gameFinderWindowFactory)
                {
                    GuestName = " "
                };

            // Act
            testObject.AddGameCommand.Execute(null);

            // Assert
            A.CallTo(() => gameService.AddGame(A<Game>.Ignored)).MustNotHaveHappened();
            A.CallTo(() => gameRepository.GetGamesBySeason(A<int>.Ignored)).MustNotHaveHappened();
        }

        [Fact]
        public void AddGameCommand_WhenHostNameIsNull_ShouldShowErrorMessageBox()
        {
            // Arrange
            var gameRepository = A.Fake<IGameRepository>();
            var seasonRepository = A.Fake<ISeasonRepository>();
            var gameService = A.Fake<IGameService>();
            var gameFinderWindowFactory = A.Fake<IWindowFactory>();
            var testObject =
                new GamesWindowViewModel(gameRepository, seasonRepository, gameService, gameFinderWindowFactory)
                {
                    GuestName = "Team",
                    HostName = null
                };

            // Act
            testObject.AddGameCommand.Execute(null);

            // Assert
            A.CallTo(() => gameService.AddGame(A<Game>.Ignored)).MustNotHaveHappened();
            A.CallTo(() => gameRepository.GetGamesBySeason(A<int>.Ignored)).MustNotHaveHappened();
        }

        [Fact]
        public void AddGameCommand_WhenHostNameIsEmpty_ShouldShowErrorMessageBox()
        {
            // Arrange
            var gameRepository = A.Fake<IGameRepository>();
            var seasonRepository = A.Fake<ISeasonRepository>();
            var gameService = A.Fake<IGameService>();
            var gameFinderWindowFactory = A.Fake<IWindowFactory>();
            var testObject =
                new GamesWindowViewModel(gameRepository, seasonRepository, gameService, gameFinderWindowFactory)
                {
                    GuestName = "Team",
                    HostName = string.Empty
                };

            // Act
            testObject.AddGameCommand.Execute(null);

            // Assert
            A.CallTo(() => gameService.AddGame(A<Game>.Ignored)).MustNotHaveHappened();
            A.CallTo(() => gameRepository.GetGamesBySeason(A<int>.Ignored)).MustNotHaveHappened();
        }

        [Fact]
        public void AddGameCommand_WhenHostNameIsWhiteSpace_ShouldShowErrorMessageBox()
        {
            // Arrange
            var gameRepository = A.Fake<IGameRepository>();
            var seasonRepository = A.Fake<ISeasonRepository>();
            var gameService = A.Fake<IGameService>();
            var gameFinderWindowFactory = A.Fake<IWindowFactory>();
            var testObject =
                new GamesWindowViewModel(gameRepository, seasonRepository, gameService, gameFinderWindowFactory)
                {
                    GuestName = "Team",
                    HostName = " "
                };

            // Act
            testObject.AddGameCommand.Execute(null);

            // Assert
            A.CallTo(() => gameService.AddGame(A<Game>.Ignored)).MustNotHaveHappened();
            A.CallTo(() => gameRepository.GetGamesBySeason(A<int>.Ignored)).MustNotHaveHappened();
        }

        [Fact]
        public void AddGameCommand_WhenGuestNameAndHostNameAreSame_ShouldShowErrorMessageBox()
        {
            // Arrange
            var gameRepository = A.Fake<IGameRepository>();
            var seasonRepository = A.Fake<ISeasonRepository>();
            var gameService = A.Fake<IGameService>();
            var gameFinderWindowFactory = A.Fake<IWindowFactory>();
            var testObject =
                new GamesWindowViewModel(gameRepository, seasonRepository, gameService, gameFinderWindowFactory)
                {
                    GuestName = "Team",
                    HostName = "Team"
                };

            // Act
            testObject.AddGameCommand.Execute(null);

            // Assert
            A.CallTo(() => gameService.AddGame(A<Game>.Ignored)).MustNotHaveHappened();
            A.CallTo(() => gameRepository.GetGamesBySeason(A<int>.Ignored)).MustNotHaveHappened();
        }

        [Fact]
        public void AddGameCommand_WhenDataEntryIsValid_ShouldShowErrorMessageBox()
        {
            // Arrange
            var gameRepository = A.Fake<IGameRepository>();
            var games = new List<Game>();
            A.CallTo(() => gameRepository.GetGamesBySeason(A<int>.Ignored)).Returns(games);

            var seasonRepository = A.Fake<ISeasonRepository>();
            var gameService = A.Fake<IGameService>();
            var gameFinderWindowFactory = A.Fake<IWindowFactory>();
            var testObject =
                new GamesWindowViewModel(gameRepository, seasonRepository, gameService, gameFinderWindowFactory)
                {
                    GuestName = "Guest",
                    HostName = "Host"
                };

            // Act
            testObject.AddGameCommand.Execute(null);

            // Assert
            A.CallTo(() => gameService.AddGame(A<Game>.Ignored)).MustHaveHappenedOnceExactly();
            A.CallTo(() => gameRepository.GetGamesBySeason(WpfGlobals.SelectedSeason)).MustHaveHappenedOnceExactly();
            testObject.Games.ShouldBeOfType<ReadOnlyCollection<Game>>();
            testObject.Games.ShouldBe(games);
            testObject.SelectedGame.ShouldBeNull();
        }

        [Fact]
        public void DeleteGameCommand_ShouldDeleteGameAndRefreshGamesCollection()
        {
            // Arrange
            var gameRepository = A.Fake<IGameRepository>();
            var games = new List<Game>();
            A.CallTo(() => gameRepository.GetGamesBySeason(A<int>.Ignored)).Returns(games);

            var seasonRepository = A.Fake<ISeasonRepository>();
            var gameService = A.Fake<IGameService>();
            var gameFinderWindowFactory = A.Fake<IWindowFactory>();
            var testObject =
                new GamesWindowViewModel(gameRepository, seasonRepository, gameService, gameFinderWindowFactory)
                {
                    SelectedGame = new Game()
                };

            // Act
            testObject.DeleteGameCommand.Execute(null);

            // Assert
            A.CallTo(() => gameService.DeleteGame(A<int>.Ignored)).MustHaveHappenedOnceExactly();
            A.CallTo(() => gameRepository.GetGamesBySeason(WpfGlobals.SelectedSeason)).MustHaveHappenedOnceExactly();
            testObject.Games.ShouldBeOfType<ReadOnlyCollection<Game>>();
            testObject.Games.ShouldBe(games);
            testObject.SelectedGame.ShouldBeNull();
        }

        [Fact]
        public void EditGameCommand_WhenDataEntryIsNotValid_ShouldShowErrorMessageBox()
        {
            // Arrange
            var gameRepository = A.Fake<IGameRepository>();
            var seasonRepository = A.Fake<ISeasonRepository>();
            var gameService = A.Fake<IGameService>();
            var gameFinderWindowFactory = A.Fake<IWindowFactory>();
            var testObject =
                new GamesWindowViewModel(gameRepository, seasonRepository, gameService, gameFinderWindowFactory)
                {
                    GuestName = null,
                    HostName = null
                };

            // Act
            testObject.EditGameCommand.Execute(null);

            // Assert
            A.CallTo(() => gameService.EditGame(A<Game>.Ignored, A<Game>.Ignored)).MustNotHaveHappened();
            A.CallTo(() => gameRepository.GetGamesBySeason(A<int>.Ignored)).MustNotHaveHappened();
        }

        [Fact]
        public void EditGameCommand_WhenDataEntryIsValidAndFindGameFilterAppliedIsTrue_ShouldEditGameAndApplyFindGameFilter()
        {
            // Arrange
            var gameRepository = A.Fake<IGameRepository>();
            var games = new List<Game>();
            A.CallTo(() => gameRepository.GetGamesBySeason(A<int>.Ignored)).Returns(games);

            var seasonRepository = A.Fake<ISeasonRepository>();
            var gameService = A.Fake<IGameService>();
            var gameFinderWindowFactory = A.Fake<IWindowFactory>();
            var testObject =
                new GamesWindowViewModel(gameRepository, seasonRepository, gameService, gameFinderWindowFactory)
                {
                    SelectedGame = new Game(),
                    GuestName = "Guest",
                    HostName = "Host",
                    FindGameFilterApplied = true
                };

            // Act
            testObject.EditGameCommand.Execute(null);

            // Assert
            A.CallTo(() => gameService.EditGame(A<Game>.Ignored, A<Game>.Ignored)).MustHaveHappenedOnceExactly();
            A.CallTo(() => gameRepository.GetGamesBySeason(WpfGlobals.SelectedSeason)).MustHaveHappenedOnceExactly();
            testObject.Games.ShouldBeOfType<ReadOnlyCollection<Game>>();
            testObject.Games.ShouldBe(games);
            testObject.FindGameFilterApplied.ShouldBeTrue();
        }

        [Fact]
        public void EditGameCommand_WhenDataEntryIsValidAndFindGameFilterAppliedIsFalse_ShouldEditGameAndShowAllGames()
        {
            // Arrange
            var gameRepository = A.Fake<IGameRepository>();
            var games = new List<Game>();
            A.CallTo(() => gameRepository.GetGamesBySeason(A<int>.Ignored)).Returns(games);

            var seasonRepository = A.Fake<ISeasonRepository>();
            var gameService = A.Fake<IGameService>();
            var gameFinderWindowFactory = A.Fake<IWindowFactory>();
            var testObject =
                new GamesWindowViewModel(gameRepository, seasonRepository, gameService, gameFinderWindowFactory)
                {
                    SelectedGame = new Game(),
                    GuestName = "Guest",
                    HostName = "Host",
                    FindGameFilterApplied = false
                };

            // Act
            testObject.EditGameCommand.Execute(null);

            // Assert
            A.CallTo(() => gameService.EditGame(A<Game>.Ignored, A<Game>.Ignored)).MustHaveHappenedOnceExactly();
            A.CallTo(() => gameRepository.GetGamesBySeason(WpfGlobals.SelectedSeason)).MustHaveHappenedOnceExactly();
            testObject.Games.ShouldBeOfType<ReadOnlyCollection<Game>>();
            testObject.Games.ShouldBe(games);
            testObject.FindGameFilterApplied.ShouldBeFalse();
        }

        [Fact]
        public void FindGameCommand_WhenGameFinderShowDialogReturnsFalse_ShouldReturnEarly()
        {
            // Arrange
            var gameRepository = A.Fake<IGameRepository>();
            var games = new List<Game>();
            A.CallTo(() => gameRepository.GetGamesBySeason(WpfGlobals.SelectedSeason)).Returns(games);

            var seasonRepository = A.Fake<ISeasonRepository>();
            var gameService = A.Fake<IGameService>();

            var gameFinderWindowFactory = A.Fake<IWindowFactory>();
            var gameFinderWindow = A.Fake<IGameFinderWindow>();
            gameFinderWindow.DataContext = new GameFinderWindowViewModel { GuestName = "Guest", HostName = "Host" };
            A.CallTo(() => gameFinderWindow.ShowDialog()).Returns(false);
            A.CallTo(() => gameFinderWindowFactory.CreateGameFinderWindow()).Returns(gameFinderWindow);

            var testObject =
                new GamesWindowViewModel(gameRepository, seasonRepository, gameService, gameFinderWindowFactory);

            // Act
            testObject.FindGameCommand.Execute(null);

            // Assert
            A.CallTo(() => gameRepository.GetGamesBySeason(WpfGlobals.SelectedSeason)).MustNotHaveHappened();
            testObject.Games.ShouldBeNull();
            testObject.FindGameFilterApplied.ShouldBeFalse();
            testObject.IsGamesReadOnly.ShouldBeFalse();
            testObject.ShowAllGamesEnabled.ShouldBeFalse();
            testObject.SelectedGame.ShouldBeNull();
            testObject.AddGameControlVisibility.ShouldBe(Visibility.Visible);
        }

        [Fact]
        public void FindGameCommand_WhenGamesCountIsZero_ShouldSetSelectedGameToNull()
        {
            // Arrange
            var gameRepository = A.Fake<IGameRepository>();
            var games = new List<Game>();
            A.CallTo(() => gameRepository.GetGamesBySeason(WpfGlobals.SelectedSeason)).Returns(games);

            var seasonRepository = A.Fake<ISeasonRepository>();
            var gameService = A.Fake<IGameService>();

            var gameFinderWindowFactory = A.Fake<IWindowFactory>();
            var gameFinderWindow = A.Fake<IGameFinderWindow>();
            gameFinderWindow.DataContext = new GameFinderWindowViewModel { GuestName = "Guest", HostName = "Host" };
            A.CallTo(() => gameFinderWindow.ShowDialog()).Returns(true);
            A.CallTo(() => gameFinderWindowFactory.CreateGameFinderWindow()).Returns(gameFinderWindow);

            var testObject =
                new GamesWindowViewModel(gameRepository, seasonRepository, gameService, gameFinderWindowFactory);

            // Act
            testObject.FindGameCommand.Execute(null);

            // Assert
            A.CallTo(() => gameRepository.GetGamesBySeason(WpfGlobals.SelectedSeason)).MustHaveHappenedOnceExactly();
            testObject.Games.ShouldBeOfType<ReadOnlyCollection<Game>>();
            testObject.Games.ShouldBe(games);
            testObject.FindGameFilterApplied.ShouldBeTrue();
            testObject.IsGamesReadOnly.ShouldBeTrue();
            testObject.ShowAllGamesEnabled.ShouldBeTrue();
            testObject.SelectedGame.ShouldBeNull();
            testObject.AddGameControlVisibility.ShouldBe(Visibility.Hidden);
        }

        [Fact]
        public void FindGameCommand_WhenGamesCountIsNotZero_ShouldSetSelectedGameToOtherThanNull()
        {
            // Arrange
            var gameRepository = A.Fake<IGameRepository>();
            var games = new List<Game>
            {
                new Game
                {
                    GuestName = "Guest",
                    HostName = "Host"
                }
            };
            A.CallTo(() => gameRepository.GetGamesBySeason(WpfGlobals.SelectedSeason)).Returns(games);

            var seasonRepository = A.Fake<ISeasonRepository>();
            var gameService = A.Fake<IGameService>();

            var gameFinderWindowFactory = A.Fake<IWindowFactory>();
            var gameFinderWindow = A.Fake<IGameFinderWindow>();
            gameFinderWindow.DataContext = new GameFinderWindowViewModel { GuestName = "Guest", HostName = "Host" };
            A.CallTo(() => gameFinderWindow.ShowDialog()).Returns(true);
            A.CallTo(() => gameFinderWindowFactory.CreateGameFinderWindow()).Returns(gameFinderWindow);

            var testObject =
                new GamesWindowViewModel(gameRepository, seasonRepository, gameService, gameFinderWindowFactory)
                {
                    Games = new ReadOnlyCollection<Game>(games),
                    SelectedGame = games[0]
                };

            // Act
            testObject.FindGameCommand.Execute(null);

            // Assert
            A.CallTo(() => gameRepository.GetGamesBySeason(WpfGlobals.SelectedSeason)).MustHaveHappenedOnceExactly();
            testObject.Games.ShouldBeOfType<ReadOnlyCollection<Game>>();
            testObject.Games.ShouldBe(games);
            testObject.FindGameFilterApplied.ShouldBeTrue();
            testObject.IsGamesReadOnly.ShouldBeTrue();
            testObject.ShowAllGamesEnabled.ShouldBeTrue();
            testObject.SelectedGame.ShouldNotBeNull();
            testObject.AddGameControlVisibility.ShouldBe(Visibility.Hidden);
        }

        [Fact]
        public void ShowAllGamesCommand_WhenSeasonIsNotNull_ShouldSetSelectedFlagsToFalse()
        {
            // Arrange
            var gameRepository = A.Fake<IGameRepository>();
            var games = new List<Game>();
            A.CallTo(() => gameRepository.GetGamesBySeason(A<int>.Ignored)).Returns(games);

            var seasonRepository = A.Fake<ISeasonRepository>();
            int numOfWeeksCompleted = 1;
            var season = new Season
            {
                NumOfWeeksCompleted = numOfWeeksCompleted
            };
            A.CallTo(() => seasonRepository.GetSeasonByYear(A<int>.Ignored)).Returns(season);

            var gameService = A.Fake<IGameService>();
            var gameFinderWindowFactory = A.Fake<IWindowFactory>();
            var testObject =
                new GamesWindowViewModel(gameRepository, seasonRepository, gameService, gameFinderWindowFactory);

            // Act
            testObject.ShowAllGamesCommand.Execute(null);

            // Assert
            A.CallTo(() => gameRepository.GetGamesBySeason(WpfGlobals.SelectedSeason)).MustHaveHappenedOnceExactly();
            testObject.Games.ShouldBeOfType<ReadOnlyCollection<Game>>();
            testObject.Games.ShouldBe(games);
            testObject.SelectedGame.ShouldBeNull();
            A.CallTo(() => seasonRepository.GetSeasonByYear(WpfGlobals.SelectedSeason)).MustHaveHappenedOnceExactly();
            testObject.Week.ShouldBe(numOfWeeksCompleted);
            testObject.FindGameFilterApplied.ShouldBeFalse();
            testObject.IsGamesReadOnly.ShouldBeFalse();
            testObject.ShowAllGamesEnabled.ShouldBeFalse();
            testObject.SelectedGame.ShouldBeNull();
        }

        [Fact]
        public void ViewGamesCommand_WhenSeasonIsNull_ShouldNotAssignToWeek()
        {
            // Arrange
            var gameRepository = A.Fake<IGameRepository>();
            var games = new List<Game>();
            A.CallTo(() => gameRepository.GetGamesBySeason(A<int>.Ignored)).Returns(games);

            var seasonRepository = A.Fake<ISeasonRepository>();
            A.CallTo(() => seasonRepository.GetSeasonByYear(A<int>.Ignored)).Returns(null);

            var gameService = A.Fake<IGameService>();
            var gameFinderWindowFactory = A.Fake<IWindowFactory>();
            var testObject =
                new GamesWindowViewModel(gameRepository, seasonRepository, gameService, gameFinderWindowFactory);

            // Act
            testObject.ViewGamesCommand.Execute(null);

            // Assert
            A.CallTo(() => gameRepository.GetGamesBySeason(A<int>.Ignored)).MustHaveHappenedOnceExactly();
            testObject.Games.ShouldBeOfType<ReadOnlyCollection<Game>>();
            testObject.Games.ShouldBe(games);
            testObject.SelectedGame.ShouldBeNull();
            A.CallTo(() => seasonRepository.GetSeasonByYear(A<int>.Ignored)).MustHaveHappenedOnceExactly();
            testObject.Week.ShouldBe(0);
        }

        [Fact]
        public void ViewGamesCommand_WhenSeasonIsNotNull_ShouldAssignToWeek()
        {
            // Arrange
            var gameRepository = A.Fake<IGameRepository>();
            var games = new List<Game>();
            A.CallTo(() => gameRepository.GetGamesBySeason(A<int>.Ignored)).Returns(games);

            var seasonRepository = A.Fake<ISeasonRepository>();
            int numOfWeeksCompleted = 1;
            var season = new Season
            {
                NumOfWeeksCompleted = numOfWeeksCompleted
            };
            A.CallTo(() => seasonRepository.GetSeasonByYear(A<int>.Ignored)).Returns(season);

            var gameService = A.Fake<IGameService>();
            var gameFinderWindowFactory = A.Fake<IWindowFactory>();
            var testObject =
                new GamesWindowViewModel(gameRepository, seasonRepository, gameService, gameFinderWindowFactory);

            // Act
            testObject.ViewGamesCommand.Execute(null);

            // Assert
            A.CallTo(() => gameRepository.GetGamesBySeason(WpfGlobals.SelectedSeason)).MustHaveHappenedOnceExactly();
            testObject.Games.ShouldBeOfType<ReadOnlyCollection<Game>>();
            testObject.Games.ShouldBe(games);
            testObject.SelectedGame.ShouldBeNull();
            A.CallTo(() => seasonRepository.GetSeasonByYear(WpfGlobals.SelectedSeason)).MustHaveHappenedOnceExactly();
            testObject.Week.ShouldBe(numOfWeeksCompleted);
        }
    }
}
