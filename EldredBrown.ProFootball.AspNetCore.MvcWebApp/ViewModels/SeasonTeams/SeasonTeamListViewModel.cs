using System.Collections.Generic;
using EldredBrown.ProFootball.NETCore.Data.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EldredBrown.ProFootball.AspNetCore.MvcWebApp.ViewModels.SeasonTeams
{
    /// <summary>
    /// Represents the model for a season team list view.
    /// </summary>
    public class SeasonTeamListViewModel
    {
        /// <summary>
        /// Gets or sets the list that lets users select a season.
        /// </summary>
        public SelectList Seasons { get; set; }

        /// <summary>
        /// Gets or sets the collection of season teams for the current view model.
        /// </summary>
        public IEnumerable<SeasonTeam> SeasonTeams { get; set; }
    }
}
