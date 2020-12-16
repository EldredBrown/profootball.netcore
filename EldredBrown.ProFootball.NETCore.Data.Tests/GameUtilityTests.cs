using EldredBrown.ProFootball.NETCore.Data.Entities;
using EldredBrown.ProFootball.NETCore.Data.Utilities;
using NUnit.Framework;

namespace EldredBrown.ProFootball.NETCore.Data.Tests
{
    public class GameUtilityTests
    {
        private GameUtility _gameUtility;

        [SetUp]
        public void Setup()
        {
            _gameUtility = new GameUtility();
        }

        [Test]
        public void DecideWinnerAndLoser_DeclaresGuestWinnerWhenGuestScoreGreaterThanHostScore()
        {
            var guestName = "Guest";
            var guestScore = 3;
            var hostName = "Host";
            var hostScore = 2;

            var game = new Game
            {
                GuestName = guestName,
                GuestScore = guestScore,
                HostName = hostName,
                HostScore = hostScore
            };

            var gameUtility = new GameUtility();
            gameUtility.DecideWinnerAndLoser(game);

            Assert.AreEqual(guestName, game.WinnerName);
            Assert.AreEqual(guestScore, game.WinnerScore);
            Assert.AreEqual(hostName, game.LoserName);
            Assert.AreEqual(hostScore, game.LoserScore);
        }

        [Test]
        public void DecideWinnerAndLoser_DeclaresHostWinnerWhenHostScoreGreaterThanGuestScore()
        {
            var guestName = "Guest";
            var guestScore = 2;
            var hostName = "Host";
            var hostScore = 3;

            var game = new Game
            {
                GuestName = guestName,
                GuestScore = guestScore,
                HostName = hostName,
                HostScore = hostScore
            };

            _gameUtility.DecideWinnerAndLoser(game);

            Assert.AreEqual(hostName, game.WinnerName);
            Assert.AreEqual(hostScore, game.WinnerScore);
            Assert.AreEqual(guestName, game.LoserName);
            Assert.AreEqual(guestScore, game.LoserScore);
        }

        [Test]
        public void DecideWinnerAndLoser_DeclaresNoWinnerWhenGuestScoreEqualsHostScore()
        {
            var guestName = "Guest";
            var guestScore = 3;
            var hostName = "Host";
            var hostScore = 3;

            var game = new Game
            {
                GuestName = guestName,
                GuestScore = guestScore,
                HostName = hostName,
                HostScore = hostScore
            };

            _gameUtility.DecideWinnerAndLoser(game);

            Assert.IsNull(game.WinnerName);
            Assert.IsNull(game.LoserName);
        }

        [Test]
        public void EditGame_AssignsCorrectValuesToDestinationProperties()
        {
            var week = 1;
            var guestName = "Guest";
            var guestScore = 3;
            var hostName = "Host";
            var hostScore = 7;
            var isPlayoff = false;
            var notes = "Notes";
            var srcGame = new Game
            {
                Week = week,
                GuestName = guestName,
                GuestScore = guestScore,
                HostName = hostName,
                HostScore = hostScore,
                WinnerName = hostName,
                WinnerScore = hostScore,
                LoserName = guestName,
                LoserScore = guestScore,
                IsPlayoff = isPlayoff,
                Notes = notes
            };
            var destGame = new Game();

            _gameUtility.Edit(destGame, srcGame);

            Assert.AreEqual(week, destGame.Week);
            Assert.AreEqual(guestName, destGame.GuestName);
            Assert.AreEqual(guestScore, destGame.GuestScore);
            Assert.AreEqual(hostName, destGame.HostName);
            Assert.AreEqual(hostScore, destGame.HostScore);
            Assert.AreEqual(hostName, destGame.WinnerName);
            Assert.AreEqual(hostScore, destGame.WinnerScore);
            Assert.AreEqual(guestName, destGame.LoserName);
            Assert.AreEqual(guestScore, destGame.LoserScore);
            Assert.AreEqual(isPlayoff, destGame.IsPlayoff);
            Assert.AreEqual(notes, destGame.Notes);
        }

        [Test]
        public void IsTie_ReturnsTrueWhenGuestScoreEqualsHostScore()
        {
            var game = new Game
            {
                GuestScore = 3,
                HostScore = 3
            };

            var result = _gameUtility.IsTie(game);
            Assert.IsTrue(result);
        }

        [Test]
        public void IsTie_ReturnsFalseWhenGuestScoreIsGreaterThanHostScore()
        {
            var game = new Game
            {
                GuestScore = 3,
                HostScore = 2
            };

            var result = _gameUtility.IsTie(game);
            Assert.IsFalse(result);
        }

        [Test]
        public void IsTie_ReturnsFalseWhenHostScoreIsGreaterThanGuestScore()
        {
            var game = new Game
            {
                GuestScore = 2,
                HostScore = 3
            };

            var result = _gameUtility.IsTie(game);
            Assert.IsFalse(result);
        }
    }
}