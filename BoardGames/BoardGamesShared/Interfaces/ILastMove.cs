namespace BoardGamesShared.Interfaces
{
    public interface ILastMove
    {
        IField OldPosition { get; set; }
        IField NewPosition { get; set; }
        IPawn Pawn { get; set; }
    }
}
