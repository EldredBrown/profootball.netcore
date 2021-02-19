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
    }
}
