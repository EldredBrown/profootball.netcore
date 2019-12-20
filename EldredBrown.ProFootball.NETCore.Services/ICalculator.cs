using EldredBrown.ProFootball.NETCore.Data.Entities;

namespace EldredBrown.ProFootball.NETCore.Services
{
    public interface ICalculator
    {
        /// <summary>
        /// Adds two values.
        /// </summary>
        /// <param name="x">The first operand.</param>
        /// <param name="y">The second operand.</param>
        /// <returns>The result of x + y.</returns>
        int Add(int x, int y);

        /// <summary>
        /// Subtracts the second value from the first.
        /// </summary>
        /// <param name="x">The first operand.</param>
        /// <param name="y">The second operand.</param>
        /// <returns>The result of x - y.</returns>
		int Subtract(int x, int y);

        /// <summary>
        /// Multiplies two values.
        /// </summary>
        /// <param name="x">The first operand.</param>
        /// <param name="y">The second operand.</param>
        /// <returns>The result of x * y.</returns>
		double Multiply(double x, double y);

        /// <summary>
        /// Divides the numerator by the denominator.
        /// </summary>
        /// <param name="x">The numerator.</param>
        /// <param name="y">The denominator.</param>
        /// <returns>The result of x / y; null if the denominator is zero.</returns>
        double? Divide(double x, double y);

        /// <summary>
        /// Calculates a team's winning percentage for the selected season.
        /// </summary>
        /// <param name="teamSeason">The <see cref="TeamSeason"/> object for which a winning percentage will be calculated.</param>
        /// <returns>The <see cref="TeamSeason"/> object's winning percentage.</returns>
        double? CalculateWinningPercentage(TeamSeason teamSeason);

        /// <summary>
        /// Calculates a team's Pythagorean Winning Percentage.
        /// </summary>
        /// <param name="teamSeason">The <see cref="TeamSeason"/> object for which a Pythagorean winning percentage will be calculated.</param>
        /// <returns>The <see cref="TeamSeason"/> object's Pythagorean winning percentage.</returns>
        double? CalculatePythagoreanWinningPercentage(TeamSeason teamSeason);
    }
}
