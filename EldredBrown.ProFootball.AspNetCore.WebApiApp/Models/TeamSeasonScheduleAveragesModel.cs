namespace EldredBrown.ProFootball.AspNetCore.WebApiApp.Models
{
    public class TeamSeasonScheduleAveragesModel
    {
        /// <summary>
        /// Gets or sets the average points scored of the current <see cref="TeamSeasonScheduleAveragesModel"/> object.
        /// </summary>
        public decimal? PointsFor { get; set; }

        /// <summary>
        /// Gets or sets the average points allowed of the current <see cref="TeamSeasonScheduleAveragesModel"/> object.
        /// </summary>
        public decimal? PointsAgainst { get; set; }
    }
}
