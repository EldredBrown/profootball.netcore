using EldredBrown.ProFootball.NETCore.Data.Entities;
using EldredBrown.ProFootball.NETCore.Data.Utilities;
using NUnit.Framework;

namespace EldredBrown.ProFootball.NETCore.Data.Tests
{
    public class LeagueSeasonUtilityTests
    {
        private LeagueSeasonUtility _leagueSeasonUtility;

        [SetUp]
        public void Setup()
        {
            _leagueSeasonUtility = new LeagueSeasonUtility();
        }

        [Test]
        public void UpdateGamesAndPoints_AssignsNullToLeagueSeasonAveragePointsWhenTotalGamesEqualsZero()
        {
            var leagueSeason = new LeagueSeason();

            var totalGames = 0;
            var totalPoints = 0;
            _leagueSeasonUtility.UpdateGamesAndPoints(leagueSeason, totalGames, totalPoints);

            Assert.AreEqual(totalGames, leagueSeason.TotalGames);
            Assert.AreEqual(totalPoints, leagueSeason.TotalPoints);
            Assert.IsNull(leagueSeason.AveragePoints);
        }

        [Test]
        public void UpdateGamesAndPoints_AssignsCorrectlyCalculatedValueToLeagueSeasonAveragePointsWhenTotalGamesNotEqualsZero()
        {
            var leagueSeason = new LeagueSeason();

            var totalGames = 1;
            var totalPoints = 20;
            _leagueSeasonUtility.UpdateGamesAndPoints(leagueSeason, totalGames, totalPoints);

            Assert.AreEqual(totalGames, leagueSeason.TotalGames);
            Assert.AreEqual(totalPoints, leagueSeason.TotalPoints);
            Assert.AreEqual((double)(totalPoints / totalGames), leagueSeason.AveragePoints);
        }
    }
}
