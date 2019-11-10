using BoardGamesShared.Enums;

namespace BoardGamesShared.Interfaces
{
    public interface IPawn
    {
        PawColors Color { get; set; }
        PawType Type { get; set; }
    }
}
