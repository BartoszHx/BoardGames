using System;
using System.Collections.Generic;
using System.Text;
using BoardGamesShared.Enums;
using BoardGamesShared.Interfaces;

namespace BoardGamesShared.Models
{
    public class GameData : IGameData
    {
        public int Turn { get; set; }
        public IPlayer PlayerTurn { get; set; }
        public IList<IPlayer> PlayerList { get; set; }
        public IBoard Board { get; set; }
        public IList<IPawnHistory> PawnHistoriesList { get; set; }
    }
}
