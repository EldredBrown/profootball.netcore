using System.Collections.Generic;
using EldredBrown.ProFootball.NETCore.Data.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EldredBrown.ProFootball.AspNetCore.MvcWebApp.ViewModels.TeamSeasons
{
    /// <summary>
    /// Represents the model for a team season list view.
    /// </summary>
    public class TeamSeasonsIndexViewModel
    {
        /// <summary>
        /// Gets or sets the title for the current view model.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the list that lets users select a season.
        /// </summary>
        public SelectList Seasons { get; set; }

        /// <summary>
        /// Gets or sets the collection of team seasons for the current view model.
        /// </summary>
        public IEnumerable<TeamSeason> TeamSeasons { get; set; }
    }
}
