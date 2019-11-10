using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BoardGamesOnline.Enums;
using BoardGamesShared.Enums;

namespace BoardGamesOnline.Models
{
    public class SearchOpponent
    {
        public int  UserId { get; set; }
        public GameTypes GameType { get; set; }
        public bool IsCancel { get; set; }
    }
}
