namespace EldredBrown.ProFootball.AspNetCore.WebApiApp.Models
{
    public class TeamSeasonScheduleTotalsModel
    {
        /// <summary>
        /// Gets or sets the total games of the current <see cref="TeamSeasonScheduleTotalsModel"/> object.
        /// </summary>
        public int? Games { get; set; }

        /// <summary>
        /// Gets or sets the total points scored of the current <see cref="TeamSeasonScheduleTotalsModel"/> object.
        /// </summary>
        public int? PointsFor { get; set; }

        /// <summary>
        /// Gets or sets the total points allowed of the current <see cref="TeamSeasonScheduleTotalsModel"/> object.
        /// </summary>
        public int? PointsAgainst { get; set; }
    }
}
