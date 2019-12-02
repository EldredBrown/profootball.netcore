using System.ComponentModel.DataAnnotations;

namespace EldredBrown.ProFootball.NETCore.Data.Entities
{
    /// <summary>
    /// Represents a pro football team for one season.
    /// </summary>
    public class SeasonTeam
    {
        /// <summary>
        /// Gets or sets the ID of the current <see cref="SeasonTeam"/> object.
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Gets or sets the ID of the current <see cref="SeasonTeam"/> object's season.
        /// </summary>
        [Display(Name = "Season")]
        public int SeasonId { get; set; }

        /// <summary>
        /// Gets or sets the name of the current <see cref="SeasonTeam"/> object's team.
        /// </summary>
        [Display(Name = "Name")]
        public string TeamName { get; set; }

        /// <summary>
        /// Gets or sets the name of the current <see cref="SeasonTeam"/> object's league.
        /// </summary>
        public string LeagueName { get; set; }

        /// <summary>
        /// Gets or sets the name of the current <see cref="SeasonTeam"/> object's conference.
        /// </summary>
        public string ConferenceName { get; set; }

        /// <summary>
        /// Gets or sets the name of the current <see cref="SeasonTeam"/> object's division.
        /// </summary>
        public string DivisionName { get; set; }

        /// <summary>
        /// Gets or sets the number of games played by the current <see cref="SeasonTeam"/>.
        /// </summary>
        public int Games { get; set; }

        /// <summary>
        /// Gets or sets the number of games won by the current <see cref="SeasonTeam"/>.
        /// </summary>
        public int Wins { get; set; }

        /// <summary>
        /// Gets or sets the number of games lost by the current <see cref="SeasonTeam"/>.
        /// </summary>
        public int Losses { get; set; }

        /// <summary>
        /// Gets or sets the number of games tied by the current <see cref="SeasonTeam"/>.
        /// </summary>
        public int Ties { get; set; }

        /// <summary>
        /// Gets or sets the winning percentage of the current <see cref="SeasonTeam"/>.
        /// </summary>
        [DisplayFormat(DataFormatString = "{0:#.000}")]
        public decimal? WinningPercentage { get; set; }

        /// <summary>
        /// Gets or sets the points scored by the current <see cref="SeasonTeam"/>.
        /// </summary>
        public int PointsFor { get; set; }

        /// <summary>
        /// Gets or sets the points scored against the current <see cref="SeasonTeam"/>.
        /// </summary>
        public int PointsAgainst { get; set; }

        /// <summary>
        /// Gets or sets the Pythagorean wins for the current <see cref="SeasonTeam"/>.
        /// </summary>
        [DisplayFormat(DataFormatString = "{0:N1}")]
        public decimal PythagoreanWins { get; set; }

        /// <summary>
        /// Gets or sets the Pythagorean losses for the current <see cref="SeasonTeam"/>.
        /// </summary>
        [DisplayFormat(DataFormatString = "{0:N1}")]
        public decimal PythagoreanLosses { get; set; }

        /// <summary>
        /// Gets or sets the offensive average of the current <see cref="SeasonTeam"/>.
        /// </summary>
        [DisplayFormat(DataFormatString = "{0:N2}")]
        public decimal? OffensiveAverage { get; set; }

        /// <summary>
        /// Gets or sets the offensive factor of the current <see cref="SeasonTeam"/>.
        /// </summary>
        [DisplayFormat(DataFormatString = "{0:#.000}")]
        public decimal? OffensiveFactor { get; set; }

        /// <summary>
        /// Gets or sets the offensive index of the current <see cref="SeasonTeam"/>.
        /// </summary>
        [DisplayFormat(DataFormatString = "{0:N2}")]
        public decimal? OffensiveIndex { get; set; }

        /// <summary>
        /// Gets or sets the defensive average of the current <see cref="SeasonTeam"/>.
        /// </summary>
        [DisplayFormat(DataFormatString = "{0:N2}")]
        public decimal? DefensiveAverage { get; set; }

        /// <summary>
        /// Gets or sets the defensive factor of the current <see cref="SeasonTeam"/>.
        /// </summary>
        [DisplayFormat(DataFormatString = "{0:#.000}")]
        public decimal? DefensiveFactor { get; set; }

        /// <summary>
        /// Gets or sets the defensive index of the current <see cref="SeasonTeam"/>.
        /// </summary>
        [DisplayFormat(DataFormatString = "{0:N2}")]
        public decimal? DefensiveIndex { get; set; }

        /// <summary>
        /// Gets or sets the final Pythagorean winning percentage of the current <see cref="SeasonTeam"/>.
        /// </summary>
        [DisplayFormat(DataFormatString = "{0:#.000}")]
        public decimal? FinalPythagoreanWinningPercentage { get; set; }

        //public virtual League League { get; set; }
        //public virtual Conference Conference { get; set; }
        //public virtual Division Division { get; set; }
        //public virtual Season Season { get; set; }
        //public virtual Team Team { get; set; }
    }
}
