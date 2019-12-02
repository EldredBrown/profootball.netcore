using System.ComponentModel.DataAnnotations;

namespace EldredBrown.ProFootball.NETCore.Data.Entities
{
    public class Opponent
    {
        public string Name { get; set; }
        public int? GamePointsFor { get; set; }
        public int? GamePointsAgainst { get; set; }
        public int? Wins { get; set; }
        public int? Losses { get; set; }
        public int? Ties { get; set; }

        [DisplayFormat(DataFormatString = "{0:#.000}")]
        public decimal? WinningPercentage { get; set; }

        public int? WeightedGames { get; set; }
        public int? WeightedPointsFor { get; set; }
        public int? WeightedPointsAgainst { get; set; }
    }
}
