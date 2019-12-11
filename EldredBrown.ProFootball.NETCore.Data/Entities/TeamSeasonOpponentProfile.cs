﻿using System.ComponentModel.DataAnnotations;

namespace EldredBrown.ProFootball.NETCore.Data.Entities
{
    public class TeamSeasonOpponentProfile
    {
        public string Opponent { get; set; }
        public int? GamePointsFor { get; set; }
        public int? GamePointsAgainst { get; set; }
        public int? OpponentWins { get; set; }
        public int? OpponentLosses { get; set; }
        public int? OpponentTies { get; set; }

        [DisplayFormat(DataFormatString = "{0:#.000}")]
        public decimal? OpponentWinningPercentage { get; set; }

        public int? OpponentWeightedGames { get; set; }
        public int? OpponentWeightedPointsFor { get; set; }
        public int? OpponentWeightedPointsAgainst { get; set; }
    }
}
