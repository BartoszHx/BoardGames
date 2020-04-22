using BoardGamesShared.Enums;

namespace BoardGamesShared.Interfaces
{
    public interface IPawn
    {
        int ID { get; set; }
        PawColors Color { get; set; }
        PawType Type { get; set; }
    }
}
