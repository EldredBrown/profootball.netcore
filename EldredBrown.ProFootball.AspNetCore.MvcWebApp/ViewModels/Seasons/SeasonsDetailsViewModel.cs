using EldredBrown.ProFootball.NETCore.Data.Entities;

namespace EldredBrown.ProFootball.AspNetCore.MvcWebApp.ViewModels.Seasons
{
    /// <summary>
    /// Represents the model for a season details view.
    /// </summary>
    public class SeasonsDetailsViewModel
    {
        /// <summary>
        /// Gets or sets the title for the current view model.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the game of the current view model.
        /// </summary>
        public Season Season { get; set; }
    }
}
