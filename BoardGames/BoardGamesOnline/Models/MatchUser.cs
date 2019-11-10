using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BoardGamesOnline.Enums;

namespace BoardGamesOnline.Models
{
    public class MatchUser
    {
        public int MatchUserId { get; set; }
        public int MatchId { get; set; }
        public User User { get; set; }
        public MatchResults MatchResult { get; set; }
    }
}
