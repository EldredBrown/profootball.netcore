using System.Collections.Generic;
using EldredBrown.ProFootball.NETCore.Data.Entities;

namespace EldredBrown.ProFootball.AspNetCore.MvcWebApp.ViewModels.Leagues
{
    /// <summary>
    /// Represents the model for a league list view.
    /// </summary>
    public class LeaguesIndexViewModel
    {
        /// <summary>
        /// Gets or sets the title for the current view model.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the collection of leagues for the current view model.
        /// </summary>
        public IEnumerable<League> Leagues { get; set; }
    }
}
