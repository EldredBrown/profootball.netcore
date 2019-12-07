using System;
using System.Collections.Generic;
using System.Text;
using EldredBrown.ProFootball.NETCore.Data.Entities;

namespace EldredBrown.ProFootball.NETCore.Services
{
    public class Calculator : ICalculator
    {
        private const double _exponent = 2.37;

        /// <summary>
        /// Adds two values
        /// </summary>
        /// <param name="lVal"></param>
        /// <param name="rVal"></param>
        /// <returns></returns>
        public virtual double Add(double lVal, double rVal)
        {
            return lVal + rVal;
        }

        /// <summary>
        /// Calculates a team's winning percentage for the selected season
        /// </summary>
        /// <param name="seasonTeam"></param>
        /// <returns></returns>
        public virtual decimal? CalculateWinningPercentage(SeasonTeam seasonTeam)
        {
            var result = Divide((2 * seasonTeam.Wins + seasonTeam.Ties), (2 * seasonTeam.Games));
            return result;
        }

        /// <summary>
        /// Calculates a team's Pythagorean Winning Percentage
        /// </summary>
        /// <param name = "pointsFor"></param>
        /// <param name = "pointsAgainst"></param>
        /// <returns></returns>
        public virtual decimal? CalculatePythagoreanWinningPercentage(SeasonTeam seasonTeam)
        {
            var numerator = Math.Pow(seasonTeam.PointsFor, _exponent);
            var denominator = 
                Math.Pow(seasonTeam.PointsFor, _exponent) + Math.Pow(seasonTeam.PointsAgainst, _exponent);
            var pct = Divide((decimal)numerator, (decimal)denominator);
            return pct;
        }

        /// <summary>
        /// Divides the numerator by the denominator.
        /// </summary>
        /// <param name="numerator">The numerator in this operation.</param>
        /// <param name="denominator">The denominator in this operation.</param>
        /// <returns>The result of the division operation; null if the denominator is zero.</returns>
        public virtual decimal? Divide(decimal numerator, decimal denominator)
        {
            // Rather than throw an error for division by zero, 
            // this will return a default result of null if division by zero occurs.
            decimal? result = null;

            if (denominator != 0)
            {
                result = numerator / denominator;
            }

            return result;
        }

        /// <summary>
        /// Multiplies two values
        /// </summary>
        /// <param name="lVal"></param>
        /// <param name="rVal"></param>
        /// <returns></returns>
		public virtual double Multiply(double lVal, double rVal)
        {
            return lVal * rVal;
        }

        /// <summary>
        /// Subtracts the second value from the first
        /// </summary>
        /// <param name="lVal"></param>
        /// <param name="rVal"></param>
        /// <returns></returns>
		public virtual double Subtract(double lVal, double rVal)
        {
            return lVal - rVal;
        }
    }
}
