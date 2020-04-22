using System;
using System.Collections.Generic;
using System.Text;
using BoardGamesShared.Interfaces;

namespace BoardGamesClient.Models
{
    public class GamePlay
    {
        public IGameData GameData { get; set; }

        public Match Match { get; set; }
    }
}
