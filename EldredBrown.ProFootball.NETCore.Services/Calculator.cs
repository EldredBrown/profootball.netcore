using System;
using EldredBrown.ProFootball.NETCore.Data.Entities;

namespace EldredBrown.ProFootball.NETCore.Services
{
    public class Calculator : ICalculator
    {
        private const double _exponent = 2.37;

        /// <summary>
        /// Adds two values.
        /// </summary>
        /// <param name="x">The first operand.</param>
        /// <param name="y">The second operand.</param>
        /// <returns>The result of x + y.</returns>
        public virtual int Add(int x, int y)
        {
            return x + y;
        }

        /// <summary>
        /// Subtracts the second value from the first.
        /// </summary>
        /// <param name="x">The first operand.</param>
        /// <param name="y">The second operand.</param>
        /// <returns>The result of x - y.</returns>
		public virtual int Subtract(int x, int y)
        {
            return x - y;
        }

        /// <summary>
        /// Multiplies two values.
        /// </summary>
        /// <param name="x">The first operand.</param>
        /// <param name="y">The second operand.</param>
        /// <returns>The result of x * y.</returns>
		public virtual double Multiply(double x, double y)
        {
            return x * y;
        }

        /// <summary>
        /// Divides the numerator by the denominator.
        /// </summary>
        /// <param name="x">The numerator.</param>
        /// <param name="y">The denominator.</param>
        /// <returns>The result of x / y; null if the denominator is zero.</returns>
        public virtual double? Divide(double x, double y)
        {
            // Rather than throw an error for division by zero, 
            // this will return a default result of null if division by zero occurs.
            double? result = null;

            if (y != 0)
            {
                result = x / y;
            }

            return result;
        }

        /// <summary>
        /// Calculates a team's winning percentage for the selected season.
        /// </summary>
        /// <param name="teamSeason">The <see cref="TeamSeason"/> object for which a winning percentage will be calculated.</param>
        /// <returns>The <see cref="TeamSeason"/> object's winning percentage.</returns>
        public virtual double? CalculateWinningPercentage(TeamSeason teamSeason)
        {
            var result = Divide(2 * teamSeason.Wins + teamSeason.Ties, 2 * teamSeason.Games);

            return result;
        }

        /// <summary>
        /// Calculates a team's Pythagorean Winning Percentage.
        /// </summary>
        /// <param name="teamSeason">The <see cref="TeamSeason"/> object for which a Pythagorean winning percentage will be calculated.</param>
        /// <returns>The <see cref="TeamSeason"/> object's Pythagorean winning percentage.</returns>
        public virtual double? CalculatePythagoreanWinningPercentage(TeamSeason teamSeason)
        {
            var x = Math.Pow(teamSeason.PointsFor, _exponent);
            var y = (Math.Pow(teamSeason.PointsFor, _exponent) + Math.Pow(teamSeason.PointsAgainst, _exponent));
            var pct = Divide(x, y);

            return pct;
        }
    }
}
