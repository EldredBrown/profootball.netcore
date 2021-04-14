using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using EldredBrown.ProFootball.NETCore.Data.Entities;
using EldredBrown.ProFootball.NETCore.Data.Repositories;
using EldredBrown.ProFootball.NETCore.WpfApp.ViewModels;
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
            var testObject = new MainWindowViewModel(seasonRepository);

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
            var testObject = new MainWindowViewModel(seasonRepository);

            // Act
            var seasons = new ReadOnlyCollection<int>(new List<int>());
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
            var testObject = new MainWindowViewModel(seasonRepository);

            // Act
            var season = 1920;
            testObject.SelectedSeason = season;

            // Assert
            testObject.SelectedSeason.ShouldBe(season);
        }

        [Fact]
        public void ViewSeasonsCommand_ShouldAssignValueToSelectedSeason_ShouldShowSeasonAndSelectedSeasonFromDataStore()
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
            var testObject = new MainWindowViewModel(seasonRepository);

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
