using System;
using System.Collections.Generic;
using System.Text;

namespace BoardGamesShared.Interfaces
{
    public interface IGameData
    {
        int Turn { get; set; }
        IPlayer PlayerTurn { get; set; }
        IList<IPlayer> PlayerList { get; set; }
        IBoard Board { get; set; }
        IList<IPawnHistory> PawnHistoriesList { get; set; }
    }
}
