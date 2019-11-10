using BoardGames.Interfaces;
using BoardGames.Extensions;

namespace BoardGames.Models.Chess
{
    internal class FieldChess : IField
    {
        public int Heigh { get; set; }
        public int Width { get; set; }
        public IPawn Pawn { get; set; }
	    public IField Copy()
	    {
		    return this.Copy<FieldChess>();
	    }
    }
}
