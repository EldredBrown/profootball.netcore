using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace EldredBrown.ProFootball.NETCore.Data.Entities
{
    /// <summary>
    /// Represents a season for a pro football team.
    /// </summary>
    public class TeamSeason
    {
        private const double _exponent = 2.37;

        /// <summary>
        /// Gets or sets the ID of the current <see cref="TeamSeason"/> entity.
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Gets or sets the name of the current <see cref="TeamSeason"/> entity's team.
        /// </summary>
        [Display(Name = "Team")]
        [Required(ErrorMessage = "Please enter a team name.")]
        public string TeamName { get; set; }

        /// <summary>
        /// Gets or sets the year of the current <see cref="TeamSeason"/> entity's season.
        /// </summary>
        [Display(Name = "Season")]
        [Required(ErrorMessage = "Please enter a year.")]
        public int SeasonYear { get; set; }

        /// <summary>
        /// Gets or sets the name of the current <see cref="TeamSeason"/> entity's league.
        /// </summary>
        [Display(Name = "League")]
        [Required(ErrorMessage = "Please enter a league name.")]
        public string LeagueName { get; set; }

        /// <summary>
        /// Gets or sets the name of the current <see cref="TeamSeason"/> entity's conference.
        /// </summary>
        [Display(Name = "Conference")]
        public string ConferenceName { get; set; }

        /// <summary>
        /// Gets or sets the name of the current <see cref="TeamSeason"/> entity's division.
        /// </summary>
        [Display(Name = "Division")]
        public string DivisionName { get; set; }

        /// <summary>
        /// Gets or sets the number of games played by the current <see cref="TeamSeason"/> entity.
        /// </summary>
        [DefaultValue(0)]
        public int Games { get; set; }

        /// <summary>
        /// Gets or sets the number of games won by the current <see cref="TeamSeason"/> entity.
        /// </summary>
        [DefaultValue(0)]
        public int Wins { get; set; }

        /// <summary>
        /// Gets or sets the number of games lost by the current <see cref="TeamSeason"/> entity.
        /// </summary>
        [DefaultValue(0)]
        public int Losses { get; set; }

        /// <summary>
        /// Gets or sets the number of games tied by the current <see cref="TeamSeason"/> entity.
        /// </summary>
        [DefaultValue(0)]
        public int Ties { get; set; }

        /// <summary>
        /// Gets or sets the winning percentage of the current <see cref="TeamSeason"/> entity.
        /// </summary>
        [DisplayFormat(DataFormatString = "{0:#.000}")]
        [Display(Name = "Winning Pct.")]
        public double? WinningPercentage { get; set; }

        /// <summary>
        /// Gets or sets the points scored by the current <see cref="TeamSeason"/> entity.
        /// </summary>
        [Display(Name = "Points For")]
        [DefaultValue(0)]
        public int PointsFor { get; set; }

        /// <summary>
        /// Gets or sets the points scored against the current <see cref="TeamSeason"/> entity.
        /// </summary>
        [Display(Name = "Points Against")]
        [DefaultValue(0)]
        public int PointsAgainst { get; set; }

        /// <summary>
        /// Gets or sets the Pythagorean wins for the current <see cref="TeamSeason"/> entity.
        /// </summary>
        [DisplayFormat(DataFormatString = "{0:N1}")]
        [Display(Name = "Expected Wins")]
        [DefaultValue(0)]
        public double PythagoreanWins { get; set; }

        /// <summary>
        /// Gets or sets the Pythagorean losses for the current <see cref="TeamSeason"/> entity.
        /// </summary>
        [DisplayFormat(DataFormatString = "{0:N1}")]
        [Display(Name = "Expected Losses")]
        [DefaultValue(0)]
        public double PythagoreanLosses { get; set; }

        /// <summary>
        /// Gets or sets the offensive average of the current <see cref="TeamSeason"/> entity.
        /// </summary>
        [DisplayFormat(DataFormatString = "{0:N2}")]
        [Display(Name = "Offensive Average")]
        public double? OffensiveAverage { get; set; }

        /// <summary>
        /// Gets or sets the offensive factor of the current <see cref="TeamSeason"/> entity.
        /// </summary>
        [DisplayFormat(DataFormatString = "{0:#.000}")]
        [Display(Name = "Offensive Factor")]
        public double? OffensiveFactor { get; set; }

        /// <summary>
        /// Gets or sets the offensive index of the current <see cref="TeamSeason"/> entity.
        /// </summary>
        [DisplayFormat(DataFormatString = "{0:N2}")]
        [Display(Name = "Offensive Index")]
        public double? OffensiveIndex { get; set; }

        /// <summary>
        /// Gets or sets the defensive average of the current <see cref="TeamSeason"/> entity.
        /// </summary>
        [DisplayFormat(DataFormatString = "{0:N2}")]
        [Display(Name = "Defensive Average")]
        public double? DefensiveAverage { get; set; }

        /// <summary>
        /// Gets or sets the defensive factor of the current <see cref="TeamSeason"/> entity.
        /// </summary>
        [DisplayFormat(DataFormatString = "{0:#.000}")]
        [Display(Name = "Defensive Factor")]
        public double? DefensiveFactor { get; set; }

        /// <summary>
        /// Gets or sets the defensive index of the current <see cref="TeamSeason"/> entity.
        /// </summary>
        [DisplayFormat(DataFormatString = "{0:N2}")]
        [Display(Name = "Defensive Index")]
        public double? DefensiveIndex { get; set; }

        /// <summary>
        /// Gets or sets the final Pythagorean winning percentage of the current <see cref="TeamSeason"/> entity.
        /// </summary>
        [DisplayFormat(DataFormatString = "{0:#.000}")]
        [Display(Name = "Final Exp. Win Pct.")]
        public double? FinalPythagoreanWinningPercentage { get; set; }

        /// <summary>
        /// Calculates and updates the current <see cref="TeamSeason"/> entity's Final Pythagorean Winning Percentage.
        /// </summary>
        public void CalculateFinalPythagoreanWinningPercentage()
        {
            FinalPythagoreanWinningPercentage = CalculatePythagoreanWinningPercentage(OffensiveIndex.Value, DefensiveIndex.Value);
        }

        /// <summary>
        /// Calculates and updates the current <see cref="TeamSeason"/> entity's Pythagorean wins and losses.
        /// </summary>
        public void CalculatePythagoreanWinsAndLosses()
        {
            var pythPct = CalculatePythagoreanWinningPercentage(PointsFor, PointsAgainst);

            if (pythPct.HasValue)
            {
                PythagoreanWins = pythPct.Value * Games;
                PythagoreanLosses = (1d - pythPct.Value) * Games;
            }
            else
            {
                PythagoreanWins = 0;
                PythagoreanLosses = 0;
            }
        }

        /// <summary>
        /// Calculates and updates the current <see cref="TeamSeason"/> entity's winning percentage.
        /// </summary>
        public void CalculateWinningPercentage()
        {
            WinningPercentage = Divide((2 * Wins + Ties), (2 * Games));
        }

        public void UpdateRankings(double? teamSeasonScheduleAveragePointsFor, double? teamSeasonScheduleAveragePointsAgainst,
            double? leagueSeasonAveragePoints)
        {
            OffensiveAverage = Divide(PointsFor, Games);
            DefensiveAverage = Divide(PointsAgainst, Games);

            OffensiveFactor = Divide(OffensiveAverage.Value, teamSeasonScheduleAveragePointsAgainst.Value);
            DefensiveFactor = Divide(DefensiveAverage.Value, teamSeasonScheduleAveragePointsFor.Value);

            OffensiveIndex = (OffensiveAverage + OffensiveFactor * leagueSeasonAveragePoints) / 2d;
            DefensiveIndex = (DefensiveAverage + DefensiveFactor * leagueSeasonAveragePoints) / 2d;

            CalculateFinalPythagoreanWinningPercentage();
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
