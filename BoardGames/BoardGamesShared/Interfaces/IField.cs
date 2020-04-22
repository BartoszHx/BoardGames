namespace BoardGamesShared.Interfaces
{
    public interface IField
    {
        int ID { get; set; }
        int Heigh { get; set; }
        int Width { get; set; }
        IPawn Pawn { get; set; }
    }
}
