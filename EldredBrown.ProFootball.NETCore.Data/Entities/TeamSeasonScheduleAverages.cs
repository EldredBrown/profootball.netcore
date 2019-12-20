using System.ComponentModel.DataAnnotations;

namespace EldredBrown.ProFootball.NETCore.Data.Entities
{
    /// <summary>
    /// Represents a team's season schedule averages.
    /// </summary>
    public class TeamSeasonScheduleAverages
    {
        /// <summary>
        /// Gets or sets the average points scored of the current <see cref="TeamSeasonScheduleAverages"/> object.
        /// </summary>
        [DisplayFormat(DataFormatString = "{0:N2}")]
        public double? PointsFor { get; set; }

        /// <summary>
        /// Gets or sets the average points allowed of the current <see cref="TeamSeasonScheduleAverages"/> object.
        /// </summary>
        [DisplayFormat(DataFormatString = "{0:N2}")]
        public double? PointsAgainst { get; set; }

        /// <summary>
        /// Gets or sets the schedule average points scored of the current <see cref="TeamSeasonScheduleAverages"/> object.
        /// </summary>
        [DisplayFormat(DataFormatString = "{0:N2}")]
        public double? SchedulePointsFor { get; set; }

        /// <summary>
        /// Gets or sets the schedule average points allowed of the current <see cref="TeamSeasonScheduleAverages"/> object.
        /// </summary>
        [DisplayFormat(DataFormatString = "{0:N2}")]
        public double? SchedulePointsAgainst { get; set; }
    }
}
