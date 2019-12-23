using System.ComponentModel;

namespace EldredBrown.ProFootball.NETCore.Data.Entities
{
    /// <summary>
    /// Represents a season for a pro football league.
    /// </summary>
    public class LeagueSeason
    {
        /// <summary>
        /// Gets or sets the ID of the current <see cref="LeagueSeason"/> entity.
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Gets or sets the name of the current <see cref="LeagueSeason"/> entity's league.
        /// </summary>
        [DisplayName("League")]
        public string LeagueName { get; set; }

        /// <summary>
        /// Gets or sets the year of the current <see cref="LeagueSeason"/> entity's season.
        /// </summary>
        [DisplayName("Season")]
        public int SeasonYear { get; set; }

        /// <summary>
        /// Gets or sets the total games of the current <see cref="LeagueSeason"/> entity.
        /// </summary>
        [DisplayName("Total Games")]
        [DefaultValue(0)]
        public int TotalGames { get; set; }

        /// <summary>
        /// Gets or sets the total points of the current <see cref="LeagueSeason"/> entity.
        /// </summary>
        [DisplayName("Total Points")]
        [DefaultValue(0)]
        public int TotalPoints { get; set; }

        /// <summary>
        /// Gets or sets the average points of the current <see cref="LeagueSeason"/> entity.
        /// </summary>
        [DisplayName("Average Points")]
        public double? AveragePoints { get; set; }
    }
}
