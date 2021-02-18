using System;
using System.Collections.Generic;
using System.Text;

namespace EldredBrown.ProFootball.NETCore.Data.Entities
{
    public interface IGame
    {
        /// <summary>
        /// Decides the winner and loser of the current <see cref="IGame"/> entity.
        /// </summary>
        void DecideWinnerAndLoser();
    }
}
