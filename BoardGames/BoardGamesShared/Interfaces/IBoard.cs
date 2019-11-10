using System.Collections.Generic;

namespace BoardGamesShared.Interfaces
{
    public interface IBoard
    {
        int MaxHeight { get; set; }
        int MaxWidth { get; set; }
	    int MinHeight { get; set; }
	    int MinWidth { get; set; }
	    ICollection<IField> FieldList { get; set; }
	    IBoard Copy(); //Nie widzi mi się to
    }
}
