using BoardGamesShared.Enums;
using BoardGamesShared.Interfaces;

namespace BoardGamesShared.Models
{
    public class Pawn : IPawn
    {
        public int ID { get; set; }
        public PawColors Color { get; set; }
	    public PawType Type { get; set; }
    }
}
