using BoardGames.Interfaces;
using BoardGames.Models.Games;
using System;

namespace BoardGames.Games
{
    public static class GameFactory
    {
        public static IGame Create(Enums.GameType type)
        {
            switch (type)
            {
                case Enums.GameType.Chess: return new ChessGame();
                case Enums.GameType.Checkers: return new CheckerGame();
                default: throw new NotImplementedException();
            }
        }
    }
}
