using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BoardGameDatabase.Models.Entites
{
    [Table("User")]
    public class User
    {
        [Key]
	    public int UserId { get; set; }

	    public string Name { get; set; }

        [Column(TypeName = "VARCHAR")]
        [StringLength(100)]
        [Index(IsUnique = true)]
        public string Email { get; set; }

		[MaxLength(48)]
        internal string Password { get; set; }

        public virtual ICollection<MatchUser> MatchUsers { get; set; }
    }
}
