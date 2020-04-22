using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardGamesShared.Interfaces
{
    public interface IPawnHistory
    {
        int Turn { get; set; }
        int PawID { get; set; }
        int PreviusFiledID { get; set; }
        int CurrentFiledID { get; set; }
    }
}
