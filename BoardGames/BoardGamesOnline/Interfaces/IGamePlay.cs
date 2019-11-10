using BoardGamesOnline.Models;
using BoardGamesShared.Interfaces;

namespace BoardGamesOnline.Interfaces
{
    public interface IGamePlay
    {
        IGameData Game { get; set; }

        Match Match { get; set; }
    }
}
