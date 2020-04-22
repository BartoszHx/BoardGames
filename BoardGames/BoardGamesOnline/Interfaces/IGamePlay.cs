using BoardGamesOnline.Models;
using BoardGamesShared.Interfaces;

namespace BoardGamesOnline.Interfaces
{
    public interface IGamePlay
    {
        IGameData GameData { get; set; }

        Match Match { get; set; }
    }
}
