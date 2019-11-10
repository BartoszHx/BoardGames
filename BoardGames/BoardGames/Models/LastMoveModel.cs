using BoardGames.Interfaces;
using BoardGamesShared.Interfaces;

namespace BoardGames.Models
{
    internal class LastMoveModel : ILastMove
    {
        public IField OldPosition { get; set; }
        public IField NewPosition { get; set; }
        public IPawn Pawn { get; set; }

        public LastMoveModel()
        {

        }
    }
}
