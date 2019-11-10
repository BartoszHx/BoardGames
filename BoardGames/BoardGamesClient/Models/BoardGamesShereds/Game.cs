using System;
using System.Collections.Generic;
using System.Text;
using BoardGamesShared.Enums;
using BoardGamesShared.Interfaces;

namespace BoardGamesClient.Models
{
    internal class Game : IGameData
    {
        public IPlayer PlayerTurn { get; set; }

        public IList<IPlayer> PlayerList { get; set; }

        public IBoard Board { get; set; }

    }
}
