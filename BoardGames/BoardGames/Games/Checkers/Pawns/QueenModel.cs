using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BoardGames.Enums;

namespace BoardGames.Models.Checkers.Pawns
{
    internal class QueenModel : PawsCheckers
    {
        public QueenModel(PawColors color) : base(color)
        {
            Type = PawType.QueenCheckers;
        }
    }
}
