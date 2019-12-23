namespace EldredBrown.ProFootball.AspNetCore.WebApiApp.Models
{
    /// <summary>
    /// Represents a model of a pro football season.
    /// </summary>
    public class SeasonModel
    {
        /// <summary>
        /// Gets or sets the year of the current <see cref="SeasonModel"/> object.
        /// </summary>
        public int Year { get; set; }

        /// <summary>
        /// Gets or sets the number of weeks in the current <see cref="SeasonModel"/> object.
        /// </summary>
        public int NumOfWeeks { get; set; }

        /// <summary>
        /// Gets or sets the number of weeks completed in the current <see cref="SeasonModel"/> object.
        /// </summary>
        public int NumOfWeeksCompleted { get; set; }
    }
}
