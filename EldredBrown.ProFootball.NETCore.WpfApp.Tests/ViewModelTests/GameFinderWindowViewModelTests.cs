using EldredBrown.ProFootball.NETCore.WpfApp.Windows.GameFinder;
using Shouldly;
using Xunit;

namespace EldredBrown.ProFootball.NETCore.WpfApp.Tests.ViewModelTests
{
    public class GameFinderWindowViewModelTests
    {
        [Fact]
        public void GuestNameSetter_WhenValueDoesNotEqualGuestName_ShouldAssignValueToGuestName()
        {
            // Arrange
            var testObject = new GameFinderWindowViewModel();

            var guestName = "Guest";

            // Act
            testObject.GuestName = guestName;

            // Assert
            testObject.GuestName.ShouldBe(guestName);
        }

        [Fact]
        public void HostNameSetter_WhenValueDoesNotEqualGuestName_ShouldAssignValueToHostName()
        {
            // Arrange
            var testObject = new GameFinderWindowViewModel();

            var hostName = "Host";

            // Act
            testObject.HostName = hostName;

            // Assert
            testObject.HostName.ShouldBe(hostName);
        }

        [Fact]
        public void ValidateDataEntry_WhenGuestNameIsNull_ShouldReturnFalseWithBothTeamsNeededErrorMessage()
        {
            // Arrange
            var testObject = new GameFinderWindowViewModel()
            {
                GuestName = null,
                HostName = null
            };

            // Act
            var (valid, message) = testObject.ValidateDataEntry();

            // Assert
            valid.ShouldBeFalse();
            message.ShouldBe<string>("Please enter names for both teams.");
        }

        [Fact]
        public void ValidateDataEntry_WhenGuestNameIsEmpty_ShouldReturnFalseWithBothTeamsNeededErrorMessage()
        {
            // Arrange
            var testObject = new GameFinderWindowViewModel()
            {
                GuestName = string.Empty,
                HostName = null
            };

            // Act
            var (valid, message) = testObject.ValidateDataEntry();

            // Assert
            valid.ShouldBeFalse();
            message.ShouldBe<string>("Please enter names for both teams.");
        }

        [Fact]
        public void ValidateDataEntry_WhenGuestNameIsWhiteSpace_ShouldReturnFalseWithBothTeamsNeededErrorMessage()
        {
            // Arrange
            var testObject = new GameFinderWindowViewModel()
            {
                GuestName = " ",
                HostName = null
            };

            // Act
            var (valid, message) = testObject.ValidateDataEntry();

            // Assert
            valid.ShouldBeFalse();
            message.ShouldBe<string>("Please enter names for both teams.");
        }

        [Fact]
        public void ValidateDataEntry_WhenHostNameIsNull_ShouldReturnFalseWithBothTeamsNeededErrorMessage()
        {
            // Arrange
            var testObject = new GameFinderWindowViewModel()
            {
                GuestName = "Team",
                HostName = null
            };

            // Act
            var (valid, message) = testObject.ValidateDataEntry();

            // Assert
            valid.ShouldBeFalse();
            message.ShouldBe<string>("Please enter names for both teams.");
        }

        [Fact]
        public void ValidateDataEntry_WhenHostNameIsEmpty_ShouldReturnFalseWithBothTeamsNeededErrorMessage()
        {
            // Arrange
            var testObject = new GameFinderWindowViewModel()
            {
                GuestName = "Team",
                HostName = string.Empty
            };

            // Act
            var (valid, message) = testObject.ValidateDataEntry();

            // Assert
            valid.ShouldBeFalse();
            message.ShouldBe<string>("Please enter names for both teams.");
        }

        [Fact]
        public void ValidateDataEntry_WhenHostNameIsWhiteSpace_ShouldReturnFalseWithBothTeamsNeededErrorMessage()
        {
            // Arrange
            var testObject = new GameFinderWindowViewModel()
            {
                GuestName = "Team",
                HostName = " "
            };

            // Act
            var (valid, message) = testObject.ValidateDataEntry();

            // Assert
            valid.ShouldBeFalse();
            message.ShouldBe<string>("Please enter names for both teams.");
        }

        [Fact]
        public void ValidateDataEntry_WhenGuestNameAndHostNameAreSame_ShouldReturnFalseWithDifferentTeamsNeededErrorMessage()
        {
            // Arrange
            var testObject = new GameFinderWindowViewModel()
            {
                GuestName = "Team",
                HostName = "Team"
            };

            // Act
            var (valid, message) = testObject.ValidateDataEntry();

            // Assert
            valid.ShouldBeFalse();
            message.ShouldBe<string>("Please enter a different name for each team.");
        }

        [Fact]
        public void ValidateDataEntry_WhenGuestNameAndHostNameAreDifferent_ShouldReturnTrueWithNullMessage()
        {
            // Arrange
            var testObject = new GameFinderWindowViewModel()
            {
                GuestName = "Guest",
                HostName = "Host"
            };

            // Act
            var (valid, message) = testObject.ValidateDataEntry();

            // Assert
            valid.ShouldBeTrue();
            message.ShouldBeNull();
        }
    }
}
