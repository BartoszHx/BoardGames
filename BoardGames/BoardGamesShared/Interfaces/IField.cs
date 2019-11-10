namespace BoardGamesShared.Interfaces
{
    public interface IField
    {
        int Heigh { get; set; }
        int Width { get; set; }
        IPawn Pawn { get; set; }
	    IField Copy(); //Nie widzi mi się to
    }
}
