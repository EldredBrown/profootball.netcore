using EldredBrown.ProFootball.NETCore.Data.Entities;

namespace EldredBrown.ProFootball.AspNetCore.MvcWebApp.ViewModels
{
    /// <summary>
    /// Represents the model for a season team details view.
    /// </summary>
    public class SeasonTeamDetailsViewModel
    {
        /// <summary>
        /// Gets or sets the season team of the current view model.
        /// </summary>
        public SeasonTeam SeasonTeam { get; set; }

        /// <summary>
        /// Gets or sets the season team schedule profile of the current view model.
        /// </summary>
        public SeasonTeamScheduleProfile SeasonTeamScheduleProfile { get; set; }

        /// <summary>
        /// Gets or sets the season team schedule totals of the current view model.
        /// </summary>
        public SeasonTeamScheduleTotals SeasonTeamScheduleTotals { get; set; }

        /// <summary>
        /// Gets or sets the season team schedule averages of the current view model.
        /// </summary>
        public SeasonTeamScheduleAverages SeasonTeamScheduleAverages { get; set; }
    }
}
