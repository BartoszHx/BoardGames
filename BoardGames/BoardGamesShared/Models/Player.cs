using System;
using System.Collections.Generic;
using System.Text;
using BoardGamesShared.Enums;
using BoardGamesShared.Interfaces;

namespace BoardGamesShared.Models
{
    public class Player : IPlayer
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public PawColors Color { get; set; }
    }
}
