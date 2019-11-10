using BoardGames.Extensions;
using BoardGamesShared.Interfaces;
using System.Collections.Generic;

namespace BoardGames.Models
{
    internal class BoardModel : IBoard
    {
        public int MaxHeight { get; set; }
        public int MaxWidth { get; set; }
	    public int MinHeight { get; set; }
	    public int MinWidth { get; set; }
	    public ICollection<IField> FieldList { get; set; }

        public BoardModel()
        {
			FieldList = new List<IField>();
        }
	    public IBoard Copy()
	    {
		    return this.FullCopy<BoardModel>();
	    }
    }
}
