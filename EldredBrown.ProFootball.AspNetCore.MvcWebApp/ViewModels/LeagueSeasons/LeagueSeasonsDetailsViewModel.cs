using EldredBrown.ProFootball.NETCore.Data.Entities;

namespace EldredBrown.ProFootball.AspNetCore.MvcWebApp.ViewModels.LeagueSeasons
{
    /// <summary>
    /// Represents the model for a league details view.
    /// </summary>
    public class LeagueSeasonsDetailsViewModel
    {
        /// <summary>
        /// Gets or sets the title for the current <see cref="LeagueSeasonsDetailsViewModel"/> object.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the league of the current <see cref="LeagueSeasonsDetailsViewModel"/> object.
        /// </summary>
        public LeagueSeason LeagueSeason { get; set; }
    }
}
