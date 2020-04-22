using BoardGamesShared.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardGamesShared.Models
{
    public class PawnHistory : IPawnHistory
    {
        public int Turn { get; set; }
        public int PawID { get; set; }
        public int PreviusFiledID { get; set; }
        public int CurrentFiledID { get; set; }
    }
}
