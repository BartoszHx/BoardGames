using System;
using System.Collections.Generic;
using System.Text;
using BoardGamesShared.Interfaces;

namespace BoardGamesClient.Models
{
    internal class Field : IField
    {
        public int Heigh { get; set; }

        public int Width { get; set; }

        public IPawn Pawn { get; set; }

        public IField Copy() //Nie ma być
        {
            throw new NotImplementedException();
        }
    }
}
