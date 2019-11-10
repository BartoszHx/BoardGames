using BoardGames.Interfaces;
using System.Collections.Generic;
using BoardGames.Extensions;

namespace BoardGames.Models.Chess
{
    internal class BoardChess : IBoard
    {
        public int MaxHeight { get; set; }
        public int MaxWidth { get; set; }
        public ICollection<IField> FieldList { get; set; }
	    public int MinHeight { get; set; }
	    public int MinWidth { get; set; }

	    public BoardChess()
        {
			FieldList = new List<IField>();
            MaxHeight = 8;
            MaxWidth = 8;
	        MinHeight = 1;
	        MinWidth = 1;
        }
	    public IBoard Copy()
	    {
		    return this.FullCopy<BoardChess>();
	    }
    }
}
