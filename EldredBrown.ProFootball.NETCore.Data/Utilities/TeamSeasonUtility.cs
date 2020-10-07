﻿using System;
using EldredBrown.ProFootball.NETCore.Data.Entities;

namespace EldredBrown.ProFootball.NETCore.Data.Utilities
{
    public class TeamSeasonUtility : ITeamSeasonUtility
    {
        private const double _exponent = 2.37;

        /// <summary>
        /// Calculates and updates the current <see cref="TeamSeason"/> entity's Final Pythagorean Winning Percentage.
        /// </summary>
        /// <param name="teamSeason">The <see cref="TeamSeason"/> entity to be modified.</param>
        public void CalculateFinalPythagoreanWinningPercentage(TeamSeason teamSeason)
        {
            teamSeason.FinalPythagoreanWinningPercentage = CalculatePythagoreanWinningPercentage(
                teamSeason.OffensiveIndex.Value, teamSeason.DefensiveIndex.Value);
        }

        /// <summary>
        /// Calculates and updates the current <see cref="TeamSeason"/> entity's Pythagorean wins and losses.
        /// </summary>
        /// <param name="teamSeason">The <see cref="TeamSeason"/> entity to be modified.</param>
        public void CalculatePythagoreanWinsAndLosses(TeamSeason teamSeason)
        {
            var pythPct = CalculatePythagoreanWinningPercentage(teamSeason.PointsFor, teamSeason.PointsAgainst);

            if (pythPct.HasValue)
            {
                teamSeason.PythagoreanWins = pythPct.Value * teamSeason.Games;
                teamSeason.PythagoreanLosses = (1d - pythPct.Value) * teamSeason.Games;
            }
            else
            {
                teamSeason.PythagoreanWins = 0;
                teamSeason.PythagoreanLosses = 0;
            }
        }

        /// <summary>
        /// Calculates and updates the current <see cref="TeamSeason"/> entity's winning percentage.
        /// </summary>
        /// <param name="teamSeason">The <see cref="TeamSeason"/> entity to be modified.</param>
        public void CalculateWinningPercentage(TeamSeason teamSeason)
        {
            teamSeason.WinningPercentage = Divide((2 * teamSeason.Wins + teamSeason.Ties), (2 * teamSeason.Games));
        }

        /// <summary>
        /// Updates the offensive and defensive averages, factors, and indices for the attached entity.
        /// </summary>
        /// <param name="teamSeasonScheduleAveragePointsFor"></param>
        /// <param name="teamSeasonScheduleAveragePointsAgainst"></param>
        /// <param name="leagueSeasonAveragePoints"></param>
        public void UpdateRankings(TeamSeason teamSeason, double? teamSeasonScheduleAveragePointsFor,
            double? teamSeasonScheduleAveragePointsAgainst, double? leagueSeasonAveragePoints)
        {
            teamSeason.OffensiveAverage = Divide(teamSeason.PointsFor, teamSeason.Games);
            teamSeason.DefensiveAverage = Divide(teamSeason.PointsAgainst, teamSeason.Games);

            teamSeason.OffensiveFactor = Divide(teamSeason.OffensiveAverage.Value,
                teamSeasonScheduleAveragePointsAgainst.Value);
            teamSeason.DefensiveFactor = Divide(teamSeason.DefensiveAverage.Value,
                teamSeasonScheduleAveragePointsFor.Value);

            teamSeason.OffensiveIndex = (teamSeason.OffensiveAverage +
                teamSeason.OffensiveFactor * leagueSeasonAveragePoints) / 2d;
            teamSeason.DefensiveIndex = (teamSeason.DefensiveAverage +
                teamSeason.DefensiveFactor * leagueSeasonAveragePoints) / 2d;

            CalculateFinalPythagoreanWinningPercentage(teamSeason);
        }

        private double? CalculatePythagoreanWinningPercentage(double pointsFor, double pointsAgainst)
        {
            var a = Math.Pow(pointsFor, _exponent);
            var b = (Math.Pow(pointsFor, _exponent) + Math.Pow(pointsAgainst, _exponent));

            double? result = Divide(a, b);

            return result;
        }

        private double? Divide(double a, double b)
        {
            double? result = null;

            if (b != 0)
            {
                result = a / b;
            }

            return result;
        }
    }
}
