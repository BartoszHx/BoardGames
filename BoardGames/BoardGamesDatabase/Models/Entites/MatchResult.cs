using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BoardGameDatabase.Models.Entites
{
    [Table("MatchResult")]
    public class MatchResult
    {
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MatchResultId { get; set; }

        [Column(TypeName = "NVARCHAR")]
        [StringLength(50)]
        [Index(IsUnique = true)]
        public string Name { get; set; }

        public virtual ICollection<MatchUser> Matchs { get; set; }
    }
}
