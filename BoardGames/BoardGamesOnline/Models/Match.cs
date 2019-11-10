using BoardGamesShared.Enums;
using System;
using System.Collections.Generic;

namespace BoardGamesOnline.Models
{
    public class Match
    {
        public int MatchId { get; set; }
        public GameTypes GameType { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime? DateEnd { get; set; }
        public string GameData { get; set; } //Iboard? history of game? Czy potrzebne?
        public List<MatchUser> MatchUsers { get; set; } 
    }
}
