using System;
using System.Collections.Generic;
using System.Text;

namespace BoardGamesClient.Models
{
    public class MatchUser
    {
        public int MatchUserId { get; set; }
        public int MatchId { get; set; }
        public User User { get; set; }
        public int MatchResult { get; set; } //Jest to enum
    }
}
