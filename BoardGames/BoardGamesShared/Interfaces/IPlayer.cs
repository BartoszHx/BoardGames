namespace BoardGamesShared.Interfaces
{
    public interface IPlayer
    {
        int ID { get; set; }
        string Name { get; set; }
        Enums.PawColors Color { get; set; }
    }
}
