using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BoardGameDatabase.Models.Entites
{
    [Table("GameType")]
    public class GameType
    {
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.None)]
	    public int GameTypeId { get; set; }

        [Column(TypeName = "VARCHAR")]
        [StringLength(50)]
		[Index(IsUnique = true)]
	    public string Name { get; set; }

        public virtual ICollection<Match> Matchs { get; set; }
    }
}
