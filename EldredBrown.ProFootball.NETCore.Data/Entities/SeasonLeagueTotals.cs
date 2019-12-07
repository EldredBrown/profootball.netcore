namespace EldredBrown.ProFootball.NETCore.Data.Entities
{
    /// <summary>
    /// Represents a pro football league's totals for a season.
    /// </summary>
    public class SeasonLeagueTotals
    {
        /// <summary>
        /// Gets or sets the league's total games played.
        /// </summary>
        public int? TotalGames { get; set; }

        /// <summary>
        /// Gets or sets the league's total points scored.
        /// </summary>
        public int? TotalPoints { get; set; }
    }
}
