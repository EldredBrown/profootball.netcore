using EldredBrown.ProFootball.NETCore.Data.Entities;

namespace EldredBrown.ProFootball.NETCore.Data.Decorators
{
    public class GameDecorator : Game, IGameDecorator
    {
        private readonly Game _game;

        public GameDecorator(Game game)
        {
            _game = game;
        }

        /// <summary>
        /// Decides the winner and loser of the wrapped <see cref="Game"/> entity.
        /// </summary>
        public void DecideWinnerAndLoser()
        {
            if (_game.GuestScore > _game.HostScore)
            {
                _game.WinnerName = _game.GuestName;
                _game.WinnerScore = _game.GuestScore;
                _game.LoserName = _game.HostName;
                _game.LoserScore = _game.HostScore;
            }
            else if (_game.HostScore > _game.GuestScore)
            {
                _game.WinnerName = _game.HostName;
                _game.WinnerScore = _game.HostScore;
                _game.LoserName = _game.GuestName;
                _game.LoserScore = _game.GuestScore;
            }
            else
            {
                _game.WinnerName = null;
                _game.LoserName = null;
            }
        }

        /// <summary>
        /// Edits the wrapped <see cref="Game"/> entity with data from another <see cref="Game"/> entity.
        /// </summary>
        /// <param name="srcGame">The <see cref="IGameDecorator"/> entity from which data will be copied.</param>
        public void Edit(IGameDecorator srcGame)
        {
            _game.Week = srcGame.Week;
            _game.GuestName = srcGame.GuestName;
            _game.GuestScore = srcGame.GuestScore;
            _game.HostName = srcGame.HostName;
            _game.HostScore = srcGame.HostScore;
            _game.WinnerName = srcGame.WinnerName;
            _game.WinnerScore = srcGame.WinnerScore;
            _game.LoserName = srcGame.LoserName;
            _game.LoserScore = srcGame.LoserScore;
            _game.IsPlayoff = srcGame.IsPlayoff;
            _game.Notes = srcGame.Notes;
        }
    }
}
