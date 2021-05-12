using System.Collections.Generic;
using EldredBrown.ProFootball.NETCore.Data.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EldredBrown.ProFootball.AspNetCore.MvcWebApp.ViewModels.Games
{
    /// <summary>
    /// Represents the model for a game list view.
    /// </summary>
    public class GamesIndexViewModel : IGamesIndexViewModel
    {
        /// <summary>
        /// Gets or sets the list that lets users select a season.
        /// </summary>
        public SelectList? Seasons { get; set; }

        /// <summary>
        /// Gets or sets the year of the selected season for the current view model.
        /// </summary>
        public int SelectedSeasonYear { get; set; }

        /// <summary>
        /// Gets or sets the list that lets users select a week.
        /// </summary>
        public SelectList? Weeks { get; set; }

        /// <summary>
        /// Gets or sets the selected week for the current view model.
        /// </summary>
        public int? SelectedWeek { get; set; }

        /// <summary>
        /// Gets or sets the collection of games for the current view model.
        /// </summary>
        public IEnumerable<Game>? Games { get; set; }
    }
}
