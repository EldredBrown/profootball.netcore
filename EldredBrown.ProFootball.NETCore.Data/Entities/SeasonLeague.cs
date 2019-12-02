namespace EldredBrown.ProFootball.NETCore.Data.Entities
{
    /// <summary>
    /// Represents a pro football league for one season.
    /// </summary>
    public class SeasonLeague
    {
        /// <summary>
        /// Gets or sets the ID of the current <see cref="SeasonLeague"/> object.
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Gets or sets the ID of the current <see cref="SeasonLeague"/> object's season.
        /// </summary>
        public int SeasonId { get; set; }

        /// <summary>
        /// Gets or sets the name of the current <see cref="SeasonLeague"/> object's league.
        /// </summary>
        public string LeagueName { get; set; }

        /// <summary>
        /// Gets or sets the total games of the current <see cref="SeasonLeague"/> object.
        /// </summary>
        public int TotalGames { get; set; }

        /// <summary>
        /// Gets or sets the total points of the current <see cref="SeasonLeague"/> object.
        /// </summary>
        public int TotalPoints { get; set; }

        /// <summary>
        /// Gets or sets the average points of the current <see cref="SeasonLeague"/> object.
        /// </summary>
        public decimal? AveragePoints { get; set; }
    }
}
