using EldredBrown.ProFootball.NETCore.Data.Entities;

namespace EldredBrown.ProFootball.NETCore.Data.Utilities
{
    public class GameUtility : IGameUtility
    {
        /// <summary>
        /// Decides the winner and loser of the specified <see cref="Game"/> entity.
        /// </summary>
        /// <param name="game">The <see cref="Game"/> entity from which data will be accessed for modification.</param>
        public void DecideWinnerAndLoser(Game game)
        {
            if (game.GuestScore > game.HostScore)
            {
                game.WinnerName = game.GuestName;
                game.WinnerScore = game.GuestScore;
                game.LoserName = game.HostName;
                game.LoserScore = game.HostScore;
            }
            else if (game.HostScore > game.GuestScore)
            {
                game.WinnerName = game.HostName;
                game.WinnerScore = game.HostScore;
                game.LoserName = game.GuestName;
                game.LoserScore = game.GuestScore;
            }
            else
            {
                game.WinnerName = null;
                game.LoserName = null;
            }
        }

        /// <summary>
        /// Edits a <see cref="Game"/> entity with data from another <see cref="Game"/> entity.
        /// </summary>
        /// <param name="destGame">The <see cref="Game"/> entity to which data will be copied.</param>
        /// <param name="srcGame">The <see cref="Game"/> entity from which data will be copied.</param>
        public void Edit(Game destGame, Game srcGame)
        {
            destGame.Week = srcGame.Week;
            destGame.GuestName = srcGame.GuestName;
            destGame.GuestScore = srcGame.GuestScore;
            destGame.HostName = srcGame.HostName;
            destGame.HostScore = srcGame.HostScore;
            destGame.WinnerName = srcGame.WinnerName;
            destGame.WinnerScore = srcGame.WinnerScore;
            destGame.LoserName = srcGame.LoserName;
            destGame.LoserScore = srcGame.LoserScore;
            destGame.IsPlayoff = srcGame.IsPlayoff;
            destGame.Notes = srcGame.Notes;
        }

        /// <summary>
        /// Checks to see if a <see cref="Game"/> entity is a tie.
        /// </summary>
        /// <param name="game">The <see cref="Game"/> entity to be checked.</param>
        /// <returns>True if the <see cref="Game"/> is a tie, otherwise false.</returns>
        public bool IsTie(Game game)
        {
            return game.GuestScore == game.HostScore;
        }
    }
}
