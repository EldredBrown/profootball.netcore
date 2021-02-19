namespace EldredBrown.ProFootball.NETCore.Data.Decorators
{
    public interface IGameDecorator
    {
        /// <summary>
        /// Gets or sets the ID of the wrapped <see cref="Game"/> entity.
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Decides the winner and loser of the wrapped <see cref="Game"/> entity.
        /// </summary>
        void DecideWinnerAndLoser();
    }
}