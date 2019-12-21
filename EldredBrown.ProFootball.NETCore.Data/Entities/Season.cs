using System.ComponentModel;

namespace EldredBrown.ProFootball.NETCore.Data.Entities
{
    /// <summary>
    /// Represents a pro football season.
    /// </summary>
    public class Season
    {
        /// <summary>
        /// Gets or sets the ID of the current <see cref="Season"/> object.
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Gets or sets the number of weeks in the current <see cref="Season"/> object.
        /// </summary>
        public int NumOfWeeks { get; set; }

        /// <summary>
        /// Gets or sets the number of weeks completed in the current <see cref="Season"/> object.
        /// </summary>
        [DefaultValue(0)]
        public int NumOfWeeksCompleted { get; set; }
    }
}
