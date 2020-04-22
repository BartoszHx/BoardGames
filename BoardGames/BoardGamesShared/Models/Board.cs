using BoardGamesShared.Interfaces;
using System.Collections.Generic;

namespace BoardGamesShared.Models
{
    public class Board : IBoard
    {
        public int MaxHeight { get; set; }
        public int MaxWidth { get; set; }
	    public int MinHeight { get; set; }
	    public int MinWidth { get; set; }
	    public ICollection<IField> FieldList { get; set; }

        public Board()
        {
			FieldList = new List<IField>();
        }
    }
}
