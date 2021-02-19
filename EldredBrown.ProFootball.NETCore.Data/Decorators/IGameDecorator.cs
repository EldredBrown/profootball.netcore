namespace EldredBrown.ProFootball.NETCore.Data.Decorators
{
    public interface IGameDecorator
    {
        /// <summary>
        /// Decides the winner and loser of the wrapped <see cref="Game"/> entity.
        /// </summary>
        void DecideWinnerAndLoser();
    }
}