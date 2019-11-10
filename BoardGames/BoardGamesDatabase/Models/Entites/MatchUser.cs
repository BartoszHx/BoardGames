using BoardGameDatabase.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BoardGameDatabase.Models.Entites
{
    [Table("MatchUser")]
    public class MatchUser
    {
		[Key]
	    public int MatchUserId { get; set; }

        [ForeignKey("Match")]
        public int MatchId { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }

        [ForeignKey("MatchResult")]
        public int MatchResultId { get; set; }

	    public virtual Match Match { get; set; } //Niełapie
        public virtual User User { get; set; } //Nie łapie//
        public virtual MatchResult MatchResult { get; set; }
    }
}
