using System.ComponentModel.DataAnnotations;

namespace EldredBrown.ProFootball.NETCore.Data.Entities
{
    /// <summary>
    /// Represents a pro football team for one season.
    /// </summary>
    public class TeamSeason
    {
        /// <summary>
        /// Gets or sets the ID of the current <see cref="TeamSeason"/> object.
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Gets or sets the name of the current <see cref="TeamSeason"/> object's team.
        /// </summary>
        [Display(Name = "Team")]
        public string TeamName { get; set; }

        /// <summary>
        /// Gets or sets the ID of the current <see cref="TeamSeason"/> object's season.
        /// </summary>
        [Display(Name = "Season")]
        public int SeasonId { get; set; }

        /// <summary>
        /// Gets or sets the name of the current <see cref="TeamSeason"/> object's league.
        /// </summary>
        [Display(Name = "League")]
        public string LeagueName { get; set; }

        /// <summary>
        /// Gets or sets the name of the current <see cref="TeamSeason"/> object's conference.
        /// </summary>
        [Display(Name = "Conference")]
        public string ConferenceName { get; set; }

        /// <summary>
        /// Gets or sets the name of the current <see cref="TeamSeason"/> object's division.
        /// </summary>
        [Display(Name = "Division")]
        public string DivisionName { get; set; }

        /// <summary>
        /// Gets or sets the number of games played by the current <see cref="TeamSeason"/>.
        /// </summary>
        public int Games { get; set; }

        /// <summary>
        /// Gets or sets the number of games won by the current <see cref="TeamSeason"/>.
        /// </summary>
        public int Wins { get; set; }

        /// <summary>
        /// Gets or sets the number of games lost by the current <see cref="TeamSeason"/>.
        /// </summary>
        public int Losses { get; set; }

        /// <summary>
        /// Gets or sets the number of games tied by the current <see cref="TeamSeason"/>.
        /// </summary>
        public int Ties { get; set; }

        /// <summary>
        /// Gets or sets the winning percentage of the current <see cref="TeamSeason"/>.
        /// </summary>
        [DisplayFormat(DataFormatString = "{0:#.000}")]
        [Display(Name = "Winning Pct.")]
        public decimal? WinningPercentage { get; set; }

        /// <summary>
        /// Gets or sets the points scored by the current <see cref="TeamSeason"/>.
        /// </summary>
        [Display(Name = "Points For")]
        public int PointsFor { get; set; }

        /// <summary>
        /// Gets or sets the points scored against the current <see cref="TeamSeason"/>.
        /// </summary>
        [Display(Name = "Points Against")]
        public int PointsAgainst { get; set; }

        /// <summary>
        /// Gets or sets the Pythagorean wins for the current <see cref="TeamSeason"/>.
        /// </summary>
        [DisplayFormat(DataFormatString = "{0:N1}")]
        [Display(Name = "Expected Wins")]
        public decimal PythagoreanWins { get; set; }

        /// <summary>
        /// Gets or sets the Pythagorean losses for the current <see cref="TeamSeason"/>.
        /// </summary>
        [DisplayFormat(DataFormatString = "{0:N1}")]
        [Display(Name = "Expected Losses")]
        public decimal PythagoreanLosses { get; set; }

        /// <summary>
        /// Gets or sets the offensive average of the current <see cref="TeamSeason"/>.
        /// </summary>
        [DisplayFormat(DataFormatString = "{0:N2}")]
        [Display(Name = "Offensive Average")]
        public decimal? OffensiveAverage { get; set; }

        /// <summary>
        /// Gets or sets the offensive factor of the current <see cref="TeamSeason"/>.
        /// </summary>
        [DisplayFormat(DataFormatString = "{0:#.000}")]
        [Display(Name = "Offensive Factor")]
        public decimal? OffensiveFactor { get; set; }

        /// <summary>
        /// Gets or sets the offensive index of the current <see cref="TeamSeason"/>.
        /// </summary>
        [DisplayFormat(DataFormatString = "{0:N2}")]
        [Display(Name = "Offensive Index")]
        public decimal? OffensiveIndex { get; set; }

        /// <summary>
        /// Gets or sets the defensive average of the current <see cref="TeamSeason"/>.
        /// </summary>
        [DisplayFormat(DataFormatString = "{0:N2}")]
        [Display(Name = "Defensive Average")]
        public decimal? DefensiveAverage { get; set; }

        /// <summary>
        /// Gets or sets the defensive factor of the current <see cref="TeamSeason"/>.
        /// </summary>
        [DisplayFormat(DataFormatString = "{0:#.000}")]
        [Display(Name = "Defensive Factor")]
        public decimal? DefensiveFactor { get; set; }

        /// <summary>
        /// Gets or sets the defensive index of the current <see cref="TeamSeason"/>.
        /// </summary>
        [DisplayFormat(DataFormatString = "{0:N2}")]
        [Display(Name = "Defensive Index")]
        public decimal? DefensiveIndex { get; set; }

        /// <summary>
        /// Gets or sets the final Pythagorean winning percentage of the current <see cref="TeamSeason"/>.
        /// </summary>
        [DisplayFormat(DataFormatString = "{0:#.000}")]
        [Display(Name = "Final Exp. Win Pct.")]
        public decimal? FinalPythagoreanWinningPercentage { get; set; }
    }
}
