using BoardGames.Extensions;
using BoardGamesShared.Interfaces;

namespace BoardGames.Models
{
    internal class FieldModel : IField
    {
        public int Heigh { get; set; }
        public int Width { get; set; }
        public IPawn Pawn { get; set; }
	    public IField Copy()
	    {
		    return this.Copy<FieldModel>();
	    }
    }
}
