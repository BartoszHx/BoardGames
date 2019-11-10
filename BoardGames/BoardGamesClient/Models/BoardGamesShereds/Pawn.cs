using System;
using System.Collections.Generic;
using System.Text;
using BoardGamesShared.Enums;
using BoardGamesShared.Interfaces;

namespace BoardGamesClient.Models
{
    internal class Pawn : IPawn
    {
        public PawColors Color { get; set; }

        public PawType Type { get; set; }
    }
}
