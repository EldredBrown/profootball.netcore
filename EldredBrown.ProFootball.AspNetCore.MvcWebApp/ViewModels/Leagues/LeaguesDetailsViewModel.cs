using EldredBrown.ProFootball.NETCore.Data.Entities;

namespace EldredBrown.ProFootball.AspNetCore.MvcWebApp.ViewModels.Leagues
{
    /// <summary>
    /// Represents the model for a league details view.
    /// </summary>
    public class LeaguesDetailsViewModel
    {
        /// <summary>
        /// Gets or sets the title for the current view model.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the league for the current view model.
        /// </summary>
        public League League { get; set; }
    }
}
