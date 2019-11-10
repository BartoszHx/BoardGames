using BoardGameDatabase.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BoardGameDatabase.Models.Entites
{
    [Table("Match")]
    public class Match
    {
        [Key]
	    public int MatchId { get; set; }
        [ForeignKey("GameType")]
        public int GameTypeId { get; set; }
        public DateTime DateStart { get; set; }
	    public DateTime? DateEnd { get; set; }
		public string GameData { get; set; }
		public virtual ICollection<MatchUser> MatchUsers { get; set; }
        public virtual GameType GameType { get; set; }
    }
}
