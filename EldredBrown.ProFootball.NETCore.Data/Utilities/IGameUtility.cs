using EldredBrown.ProFootball.NETCore.Data.Entities;

namespace EldredBrown.ProFootball.NETCore.Data.Utilities
{
    public interface IGameUtility
    {
        /// <summary>
        /// Checks to see if a <see cref="Game"/> entity is a tie.
        /// </summary>
        /// <param name="game">The <see cref="Game"/> entity to be checked.</param>
        /// <returns>True if the <see cref="Game"/> is a tie, otherwise false.</returns>
        bool IsTie(Game game);
    }
}
