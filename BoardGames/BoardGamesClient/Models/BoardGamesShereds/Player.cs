using System;
using System.Collections.Generic;
using System.Text;
using BoardGamesShared.Enums;
using BoardGamesShared.Interfaces;

namespace BoardGamesClient.Models
{
    internal class Player : IPlayer
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public PawColors Color { get; set; }
    }
}
