using BoardGamesOnline.Interfaces;
using BoardGamesShared.Interfaces;

namespace BoardGamesOnline.Models
{
    internal class GamePlay : IGamePlay
    {
        public IGameData Game { get; set; }

        public Match Match { get; set; }
    }
}
