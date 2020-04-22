using BoardGamesOnline.Interfaces;
using BoardGamesShared.Interfaces;

namespace BoardGamesOnline.Models
{
    internal class GamePlay : IGamePlay
    {
        public IGameData GameData { get; set; }

        public Match Match { get; set; }
    }
}
