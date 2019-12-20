using System.ComponentModel.DataAnnotations;

namespace EldredBrown.ProFootball.NETCore.Data.Entities
{
    /// <summary>
    /// Represents a team's season schedule totals.
    /// </summary>
    public class TeamSeasonScheduleTotals
    {
        /// <summary>
        /// Gets or sets the total games of the current <see cref="TeamSeasonScheduleTotals"/> object.
        /// </summary>
        public int? Games { get; set; }

        /// <summary>
        /// Gets or sets the total points scored of the current <see cref="TeamSeasonScheduleTotals"/> object.
        /// </summary>
        public int? PointsFor { get; set; }

        /// <summary>
        /// Gets or sets the total points allowed of the current <see cref="TeamSeasonScheduleTotals"/> object.
        /// </summary>
        public int? PointsAgainst { get; set; }

        /// <summary>
        /// Gets or sets the total schedule wins of the current <see cref="TeamSeasonScheduleTotals"/> object.
        /// </summary>
        public int? ScheduleWins { get; set; }

        /// <summary>
        /// Gets or sets the total schedule losses of the current <see cref="TeamSeasonScheduleTotals"/> object.
        /// </summary>
        public int? ScheduleLosses { get; set; }

        /// <summary>
        /// Gets or sets the total schedule ties of the current <see cref="TeamSeasonScheduleTotals"/> object.
        /// </summary>
        public int? ScheduleTies { get; set; }

        /// <summary>
        /// Gets or sets the schedule winning percentage of the current <see cref="TeamSeasonScheduleTotals"/> object.
        /// </summary>
        [DisplayFormat(DataFormatString = "{0:#.000}")]
        public double? ScheduleWinningPercentage { get; set; }

        /// <summary>
        /// Gets or sets the total schedule games of the current <see cref="TeamSeasonScheduleTotals"/> object.
        /// </summary>
        public int? ScheduleGames { get; set; }

        /// <summary>
        /// Gets or sets the total schedule points scored of the current <see cref="TeamSeasonScheduleTotals"/> object.
        /// </summary>
        public int? SchedulePointsFor { get; set; }

        /// <summary>
        /// Gets or sets the total schedule points allowed of the current <see cref="TeamSeasonScheduleTotals"/> object.
        /// </summary>
        public int? SchedulePointsAgainst { get; set; }
    }
}
