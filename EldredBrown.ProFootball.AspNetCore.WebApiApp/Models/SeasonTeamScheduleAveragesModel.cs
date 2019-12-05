using EldredBrown.ProFootball.NETCore.Data.Entities;

namespace EldredBrown.ProFootball.AspNetCore.WebApiApp.Models
{
    public class SeasonTeamScheduleAveragesModel
    {
        /// <summary>
        /// Gets or sets the average points scored of the current <see cref="SeasonTeamScheduleAverages"/> object.
        /// </summary>
        public decimal? PointsFor { get; set; }

        /// <summary>
        /// Gets or sets the average points allowed of the current <see cref="SeasonTeamScheduleAverages"/> object.
        /// </summary>
        public decimal? PointsAgainst { get; set; }
    }
}
