namespace EldredBrown.ProFootball.NETCore.Data.Entities
{
    /// <summary>
    /// Represents the count of weeks completed in a pro football season.
    /// </summary>
    public class WeekCount
    {
        /// <summary>
        /// Gets or sets the season ID of the current <see cref="WeekCount"/> object.
        /// </summary>
        public int SeasonId { get; set; }

        /// <summary>
        /// Gets or sets the count of the current <see cref="WeekCount"/> object.
        /// </summary>
        public int Count { get; set; }
    }
}
