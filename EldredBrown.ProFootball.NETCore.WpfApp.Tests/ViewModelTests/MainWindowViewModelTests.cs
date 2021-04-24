using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using EldredBrown.ProFootball.NETCore.Data.Entities;
using EldredBrown.ProFootball.NETCore.Data.Repositories;
using EldredBrown.ProFootball.NETCore.Services;
using EldredBrown.ProFootball.NETCore.Services.GamePredictorService;
using EldredBrown.ProFootball.NETCore.WpfApp.Main;
using EldredBrown.ProFootball.WpfApp;
using FakeItEasy;
using Shouldly;
using Xunit;

namespace EldredBrown.ProFootball.NETCore.WpfApp.Tests.ViewModelTests
{
    public class MainWindowViewModelTests
    {
        [Fact]
        public void SeasonsSetter_WhenValueIsNull_ShouldThrowArgumentNullException()
        {
            // Arrange
            var seasonRepository = A.Fake<ISeasonRepository>();
            var weeklyUpdateService = A.Fake<IWeeklyUpdateService>();
            var gamePredictorService = A.Fake<IGamePredictorService>();
            var testObject = new MainWindowViewModel(seasonRepository, weeklyUpdateService, gamePredictorService);

            // Act
            Func<ReadOnlyCollection<int>> func = () => testObject.Seasons = null;

            // Assert
            var ex = func.ShouldThrow<ArgumentNullException>();
            ex.ParamName.ShouldBe<string>($"{testObject.GetType()}.{nameof(testObject.Seasons)}");
        }

        [Fact]
        public void SeasonsSetter_WhenValueDoesNotEqualSeasons_ShouldAssignValueToSeasons()
        {
            // Arrange
            var seasonRepository = A.Fake<ISeasonRepository>();
            var weeklyUpdateService = A.Fake<IWeeklyUpdateService>();
            var gamePredictorService = A.Fake<IGamePredictorService>();
            var testObject = new MainWindowViewModel(seasonRepository, weeklyUpdateService, gamePredictorService);

            var seasons = new ReadOnlyCollection<int>(new List<int>());

            // Act
            Func<ReadOnlyCollection<int>> func = () => testObject.Seasons = seasons;

            // Assert
            func.ShouldNotThrow();
            testObject.Seasons.ShouldBeOfType<ReadOnlyCollection<int>>();
            testObject.Seasons.ShouldBe(seasons);
        }

        [Fact]
        public void SelectedSeasonsSetter_WhenValueDoesNotEqualSelectedSeason_ShouldAssignValueToSelectedSeason()
        {
            // Arrange
            var seasonRepository = A.Fake<ISeasonRepository>();
            var weeklyUpdateService = A.Fake<IWeeklyUpdateService>();
            var gamePredictorService = A.Fake<IGamePredictorService>();
            var testObject = new MainWindowViewModel(seasonRepository, weeklyUpdateService, gamePredictorService);

            var season = 1920;

            // Act
            testObject.SelectedSeason = season;

            // Assert
            testObject.SelectedSeason.ShouldBe(season);
        }

        [Fact]
        public void PredictGameScoreCommand_ShouldCallGamePredictorServicePredictGameScore()
        {
            // Arrange
            var seasonRepository = A.Fake<ISeasonRepository>();
            var weeklyUpdateService = A.Fake<IWeeklyUpdateService>();
            var gamePredictorService = A.Fake<IGamePredictorService>();
            var testObject = new MainWindowViewModel(seasonRepository, weeklyUpdateService, gamePredictorService);

            // Act
            testObject.PredictGameScoreCommand.Execute(null);

            // Assert
            A.CallTo(() => gamePredictorService.PredictGameScore()).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public void RunWeeklyUpdateCommand_ShouldCallWeeklyUpdateServiceRunWeeklyUpdate()
        {
            // Arrange
            var seasonRepository = A.Fake<ISeasonRepository>();
            var weeklyUpdateService = A.Fake<IWeeklyUpdateService>();
            var gamePredictorService = A.Fake<IGamePredictorService>();
            var testObject = new MainWindowViewModel(seasonRepository, weeklyUpdateService, gamePredictorService);

            // Act
            testObject.WeeklyUpdateCommand.Execute(null);

            // Assert
            A.CallTo(() => weeklyUpdateService.RunWeeklyUpdate(WpfGlobals.SelectedSeason))
                .MustHaveHappenedOnceExactly();
        }

        [Fact]
        public void ViewSeasonsCommand_ShouldShowSeasonAndSelectedSeasonFromDataStore()
        {
            // Arrange
            var seasonRepository = A.Fake<ISeasonRepository>();
            var years = new int[] { 1920, 1921, 1922 };
            var seasons = new List<Season>
            {
                new Season { Year = years[0] },
                new Season { Year = years[1] },
                new Season { Year = years[2] }
            };
            A.CallTo(() => seasonRepository.GetSeasons()).Returns(seasons);

            var weeklyUpdateService = A.Fake<IWeeklyUpdateService>();
            var gamePredictorService = A.Fake<IGamePredictorService>();

            var testObject = new MainWindowViewModel(seasonRepository, weeklyUpdateService, gamePredictorService);

            // Act
            testObject.ViewSeasonsCommand.Execute(null);

            // Assert
            A.CallTo(() => seasonRepository.GetSeasons()).MustHaveHappenedOnceExactly();
            testObject.Seasons.ShouldBeOfType<ReadOnlyCollection<int>>();
            testObject.Seasons.ShouldBe(years);
            testObject.SelectedSeason.ShouldBe(years[0]);
        }
    }
}
