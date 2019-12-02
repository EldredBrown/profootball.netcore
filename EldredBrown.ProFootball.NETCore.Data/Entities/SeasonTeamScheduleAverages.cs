using System.ComponentModel.DataAnnotations;

namespace EldredBrown.ProFootball.NETCore.Data.Entities
{
    /// <summary>
    /// Represents a team's season schedule averages.
    /// </summary>
    public class SeasonTeamScheduleAverages
    {
        /// <summary>
        /// Gets or sets the average points scored of the current <see cref="SeasonTeamScheduleAverages"/> object.
        /// </summary>
        [DisplayFormat(DataFormatString = "{0:N2}")]
        public decimal? PointsFor { get; set; }

        /// <summary>
        /// Gets or sets the average points allowed of the current <see cref="SeasonTeamScheduleAverages"/> object.
        /// </summary>
        [DisplayFormat(DataFormatString = "{0:N2}")]
        public decimal? PointsAgainst { get; set; }

        /// <summary>
        /// Gets or sets the schedule average points scored of the current <see cref="SeasonTeamScheduleAverages"/> object.
        /// </summary>
        [DisplayFormat(DataFormatString = "{0:N2}")]
        public decimal? SchedulePointsFor { get; set; }

        /// <summary>
        /// Gets or sets the schedule average points allowed of the current <see cref="SeasonTeamScheduleAverages"/> object.
        /// </summary>
        [DisplayFormat(DataFormatString = "{0:N2}")]
        public decimal? SchedulePointsAgainst { get; set; }
    }
}
