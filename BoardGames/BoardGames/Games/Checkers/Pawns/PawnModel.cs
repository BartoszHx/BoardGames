using BoardGames.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BoardGames.Enums;

namespace BoardGames.Models.Checkers.Pawns
{
    internal class PawnModel : PawsCheckers
    {
        public PawnModel(PawColors color) : base(color)
        {
            Type = PawType.PawnCheckers;
        }
    }
}
