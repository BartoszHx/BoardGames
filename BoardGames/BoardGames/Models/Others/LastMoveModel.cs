using BoardGames.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardGames.Models.Others
{
    internal class LastMoveModel : ILastMove
    {
        public IField OldPosition { get; set; }
        public IField NewPosition { get; set; }
        public IPawn Pawn { get; set; }

        public LastMoveModel()
        {

        }
    }
}
