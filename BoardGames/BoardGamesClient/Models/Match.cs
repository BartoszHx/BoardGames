using System;
using System.Collections.Generic;
using System.Text;

namespace BoardGamesClient.Models
{
    public class Match
    {
        public int MatchId { get; set; }
        public int GameType { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        //google.protobuf.Timestamp DateStart = 3;
        //google.protobuf.Timestamp DateEnd = 4;
        public List<User> MatchUsers { get; set; }
    }
}
