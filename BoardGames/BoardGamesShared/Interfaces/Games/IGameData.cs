using System;
using System.Collections.Generic;
using System.Text;

namespace BoardGamesShared.Interfaces
{
    public interface IGameData
    {
        IPlayer PlayerTurn { get; set; }
        IList<IPlayer> PlayerList { get; set; }
        IBoard Board { get; set; }
    }
}
