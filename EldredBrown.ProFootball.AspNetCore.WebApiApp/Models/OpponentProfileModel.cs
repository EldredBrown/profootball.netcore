namespace EldredBrown.ProFootball.AspNetCore.WebApiApp.Models
{
    public class OpponentProfileModel
    {
        public string Name { get; set; }
        public int? GamePointsFor { get; set; }
        public int? GamePointsAgainst { get; set; }
        public int? OpponentWins { get; set; }
        public int? OpponentLosses { get; set; }
        public int? OpponentTies { get; set; }
        public decimal? OpponentWinningPercentage { get; set; }
        public int? OpponentWeightedGames { get; set; }
        public int? OpponentWeightedPointsFor { get; set; }
        public int? OpponentWeightedPointsAgainst { get; set; }
    }
}
