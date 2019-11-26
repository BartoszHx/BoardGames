using BoardGamesShared.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardGames.Interfaces
{
    public interface IGameBulider
    {
        IGame Bulid(BoardGamesShared.Enums.GameTypes gameType);
    }
}
