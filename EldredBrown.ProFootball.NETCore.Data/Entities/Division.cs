namespace EldredBrown.ProFootball.NETCore.Data.Entities
{
    /// <summary>
    /// Represents a pro football division.
    /// </summary>
    public class Division
    {
        /// <summary>
        /// Gets or sets the ID of the current <see cref="Division"/> object.
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Gets or sets the name of the current <see cref="Division"/> object.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the name of the current <see cref="Division"/> object's league.
        /// </summary>
        public string LeagueName { get; set; }

        /// <summary>
        /// Gets or sets the name of the current <see cref="Division"/> object's conference.
        /// </summary>
        public string ConferenceName { get; set; }

        /// <summary>
        /// Gets or sets the ID of the current <see cref="Division"/> object's first season.
        /// </summary>
        public int FirstSeasonId { get; set; }

        /// <summary>
        /// Gets or sets the ID of the current <see cref="Division"/> object's last season.
        /// </summary>
        public int? LastSeasonId { get; set; }
    }
}
