using System;
using EldredBrown.ProFootball.NETCore.Data.Entities;
using EldredBrown.ProFootball.NETCore.Data.Utilities;
using NUnit.Framework;

namespace EldredBrown.ProFootball.NETCore.Data.Tests
{
    public class TeamSeasonUtilityTests
    {
        private TeamSeasonUtility _teamSeasonUtility;

        [SetUp]
        public void Setup()
        {
            _teamSeasonUtility = new TeamSeasonUtility();
        }

        [Test]
        public void CalculateFinalPythagoreanWinningPercentage_AssignsNothingToTeamSeasonFinalPythagoreanWinningPercentageWhenTeamSeasonOffensiveIndexIsNull()
        {
            double? offensiveIndex = null;
            double? defensiveIndex = null;

            var teamSeason = new TeamSeason
            {
                OffensiveIndex = offensiveIndex,
                DefensiveIndex = defensiveIndex
            };

            _teamSeasonUtility.CalculateFinalPythagoreanWinningPercentage(teamSeason);

            Assert.IsNull(teamSeason.FinalPythagoreanWinningPercentage);
        }

        [Test]
        public void CalculateFinalPythagoreanWinningPercentage_AssignsNothingToTeamSeasonFinalPythagoreanWinningPercentageWhenTeamSeasonDefensiveIndexIsNull()
        {
            double? offensiveIndex = 1.2d;
            double? defensiveIndex = null;

            var teamSeason = new TeamSeason
            {
                OffensiveIndex = offensiveIndex,
                DefensiveIndex = defensiveIndex
            };

            _teamSeasonUtility.CalculateFinalPythagoreanWinningPercentage(teamSeason);

            Assert.IsNull(teamSeason.FinalPythagoreanWinningPercentage);
        }

        [Test]
        public void CalculateFinalPythagoreanWinningPercentage_AssignsCorrectlyCalculatedValueToTeamSeasonFinalPythagoreanWinningPercentageWhenTeamSeasonIndicesAreNotNull()
        {
            const double exponent = 2.37d;
            double? offensiveIndex = 1.2d;
            double? defensiveIndex = 0.8d;

            var teamSeason = new TeamSeason
            {
                OffensiveIndex = offensiveIndex,
                DefensiveIndex = defensiveIndex
            };

            _teamSeasonUtility.CalculateFinalPythagoreanWinningPercentage(teamSeason);

            var a = Math.Pow(offensiveIndex.Value, exponent);
            var b = (Math.Pow(offensiveIndex.Value, exponent) + Math.Pow(defensiveIndex.Value, exponent));
            double result = a / b;

            Assert.AreEqual(result, teamSeason.FinalPythagoreanWinningPercentage);
        }

        [Test]
        public void CalculatePythagoreanWinsAndLosses_UpdatesTeamSeasonPythagoreanWinsAndLossesWithCorrectlyCalculatedValuesWhenTeamSeasonPointsForAndPointsAgainstNotEqualZero()
        {
            var teamSeason = new TeamSeason
            {
                PointsFor = 7,
                PointsAgainst = 6,
                Games = 1
            };

            _teamSeasonUtility.CalculatePythagoreanWinsAndLosses(teamSeason);

            Assert.AreNotEqual(0, teamSeason.PythagoreanWins);
            Assert.AreNotEqual(0, teamSeason.PythagoreanLosses);
        }

        [Test]
        public void CalculatePythagoreanWinsAndLosses_UpdatesTeamSeasonPythagoreanWinsAndLossesWithCorrectlyCalculatedValuesWhenTeamSeasonPointsForNotEqualsZero()
        {
            var teamSeason = new TeamSeason
            {
                PointsFor = 7,
                PointsAgainst = 0,
                Games = 1
            };

            _teamSeasonUtility.CalculatePythagoreanWinsAndLosses(teamSeason);

            Assert.AreNotEqual(0, teamSeason.PythagoreanWins);
            Assert.AreEqual(0, teamSeason.PythagoreanLosses);
        }

        [Test]
        public void CalculatePythagoreanWinsAndLosses_UpdatesTeamSeasonPythagoreanWinsAndLossesWithCorrectlyCalculatedValuesWhenTeamSeasonPointsAgainstNotEqualsZero()
        {
            var teamSeason = new TeamSeason
            {
                PointsFor = 0,
                PointsAgainst = 7,
                Games = 1
            };

            _teamSeasonUtility.CalculatePythagoreanWinsAndLosses(teamSeason);

            Assert.AreEqual(0, teamSeason.PythagoreanWins);
            Assert.AreNotEqual(0, teamSeason.PythagoreanLosses);
        }

        [Test]
        public void CalculatePythagoreanWinsAndLosses_UpdatesTeamSeasonPythagoreanWinsAndLossesWithZeroWhenTeamSeasonPointsForAndPointsAgainstEqualZero()
        {
            var teamSeason = new TeamSeason
            {
                PointsFor = 0,
                PointsAgainst = 0,
                Games = 1
            };

            _teamSeasonUtility.CalculatePythagoreanWinsAndLosses(teamSeason);

            Assert.AreEqual(0, teamSeason.PythagoreanWins);
            Assert.AreEqual(0, teamSeason.PythagoreanLosses);
        }

        [Test]
        public void CalculateWinningPercentage_AssignsCorrectlyCalculatedValueToTeamSeasonWinningPercentageWhenTeamSeasonGamesNotEqualsZero()
        {
            var teamSeason = new TeamSeason
            {
                Wins = 8,
                Losses = 7,
                Ties = 1,
                Games = 16
            };

            _teamSeasonUtility.CalculateWinningPercentage(teamSeason);

            double expected = (2 * (double)teamSeason.Wins + (double)teamSeason.Ties) / (2 * (double)teamSeason.Games);
            Assert.AreEqual(expected, teamSeason.WinningPercentage);
        }

        [Test]
        public void CalculateWinningPercentage_AssignsNullToTeamSeasonWinningPercentageWhenTeamSeasonGamesEqualsZero()
        {
            var teamSeason = new TeamSeason
            {
                Wins = 0,
                Losses = 0,
                Ties = 0,
                Games = 0
            };

            _teamSeasonUtility.CalculateWinningPercentage(teamSeason);

            Assert.IsNull(teamSeason.WinningPercentage);
        }

        [Test]
        public void UpdateRankings_AssignsNullToTeamSeasonOffensiveAndDefensivePropertiesWhenTeamSeasonGamesEqualsZero()
        {
            var teamSeason = new TeamSeason
            {
                PointsFor = 0,
                PointsAgainst = 0,
                Games = 0
            };

            _teamSeasonUtility.UpdateRankings(teamSeason, 0, 0, 0);

            Assert.IsNull(teamSeason.OffensiveAverage);
            Assert.IsNull(teamSeason.DefensiveAverage);
            Assert.IsNull(teamSeason.OffensiveFactor);
            Assert.IsNull(teamSeason.DefensiveFactor);
            Assert.IsNull(teamSeason.OffensiveIndex);
            Assert.IsNull(teamSeason.DefensiveIndex);
            Assert.IsNull(teamSeason.FinalPythagoreanWinningPercentage);
        }

        [Test]
        public void UpdateRankings_AssignsNullToTeamSeasonOffensiveAndDefensiveFactorsAndIndicesWhenTeamSeasonScheduleAveragesEqualZero()
        {
            var teamSeason = new TeamSeason
            {
                PointsFor = 0,
                PointsAgainst = 0,
                Games = 1
            };

            _teamSeasonUtility.UpdateRankings(teamSeason, 0, 0, 0);

            Assert.IsNotNull(teamSeason.OffensiveAverage);
            Assert.IsNotNull(teamSeason.DefensiveAverage);
            Assert.IsNull(teamSeason.OffensiveFactor);
            Assert.IsNull(teamSeason.DefensiveFactor);
            Assert.IsNull(teamSeason.OffensiveIndex);
            Assert.IsNull(teamSeason.DefensiveIndex);
            Assert.IsNull(teamSeason.FinalPythagoreanWinningPercentage);
        }

        [Test]
        public void UpdateRankings_AssignsCorrectlyCalculatedValuesToTeamSeasonOffensiveAndDefensiveIndicesWhenLeagueSeasonScheduleAveragePointsEqualsZero()
        {
            var teamSeason = new TeamSeason
            {
                PointsFor = 1,
                PointsAgainst = 1,
                Games = 1
            };

            _teamSeasonUtility.UpdateRankings(teamSeason, 1, 1, 0);

            Assert.IsNotNull(teamSeason.OffensiveAverage);
            Assert.IsNotNull(teamSeason.DefensiveAverage);
            Assert.IsNotNull(teamSeason.OffensiveFactor);
            Assert.IsNotNull(teamSeason.DefensiveFactor);
            Assert.AreEqual(teamSeason.OffensiveAverage / 2d, teamSeason.OffensiveIndex);
            Assert.AreEqual(teamSeason.DefensiveAverage / 2d, teamSeason.DefensiveIndex);
            Assert.IsNotNull(teamSeason.FinalPythagoreanWinningPercentage);
        }

        [Test]
        public void UpdateRankings_AssignsCorrectlyCalculatedValuesToTeamSeasonPropertiesWhenLeagueSeasonScheduleAverageNotEqualsZero()
        {
            var leagueSeasonAveragePoints = 1;

            var teamSeason = new TeamSeason
            {
                PointsFor = 1,
                PointsAgainst = 1,
                Games = 1
            };

            _teamSeasonUtility.UpdateRankings(teamSeason, 1, 1, leagueSeasonAveragePoints);

            Assert.IsNotNull(teamSeason.OffensiveAverage);
            Assert.IsNotNull(teamSeason.DefensiveAverage);
            Assert.IsNotNull(teamSeason.OffensiveFactor);
            Assert.IsNotNull(teamSeason.DefensiveFactor);
            Assert.AreEqual((teamSeason.OffensiveAverage + teamSeason.OffensiveFactor * leagueSeasonAveragePoints) / 2d,
                teamSeason.OffensiveIndex);
            Assert.AreEqual((teamSeason.DefensiveAverage + teamSeason.DefensiveFactor * leagueSeasonAveragePoints) / 2d,
                teamSeason.DefensiveIndex);
            Assert.IsNotNull(teamSeason.FinalPythagoreanWinningPercentage);
        }
    }
}
