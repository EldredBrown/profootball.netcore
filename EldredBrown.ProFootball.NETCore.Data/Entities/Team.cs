namespace EldredBrown.ProFootball.NETCore.Data.Entities
{
    /// <summary>
    /// Represents a pro football team.
    /// </summary>
    public class Team
    {
        /// <summary>
        /// Gets or sets the ID of the current <see cref="Team"/> object.
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Gets or sets the name of the current <see cref="Team"/> object.
        /// </summary>
        public string Name { get; set; }
    }
}
