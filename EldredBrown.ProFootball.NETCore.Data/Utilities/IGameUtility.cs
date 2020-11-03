using EldredBrown.ProFootball.NETCore.Data.Entities;

namespace EldredBrown.ProFootball.NETCore.Data.Utilities
{
    public interface IGameUtility
    {
        /// <summary>
        /// Decides the winner and loser of the specified <see cref="Game"/> entity.
        /// </summary>
        /// <param name="game">The <see cref="Game"/> entity from which data will be accessed for modification.</param>
        void DecideWinnerAndLoser(Game game);

        /// <summary>
        /// Edits a <see cref="Game"/> entity with data from another <see cref="Game"/> entity.
        /// </summary>
        /// <param name="destGame">The <see cref="Game"/> entity to which data will be copied.</param>
        /// <param name="srcGame">The <see cref="Game"/> entity from which data will be copied.</param>
        void Edit(Game destGame, Game srcGame);

        /// <summary>
        /// Checks to see if a <see cref="Game"/> entity is a tie.
        /// </summary>
        /// <param name="game">The <see cref="Game"/> entity to be checked.</param>
        /// <returns>True if the <see cref="Game"/> is a tie, otherwise false.</returns>
        bool IsTie(Game game);
    }
}
