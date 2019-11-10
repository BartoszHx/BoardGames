using BoardGames.Interfaces;
using System;
using BoardGames.Games.Chess;
using BoardGames.Models.Checkers;
using BoardGamesShared.Enums;
using BoardGamesShared.Interfaces;

namespace BoardGames.Factories
{
    public static class GameFactory
    {
        public static IGame Create(GameTypes type)
        {
            switch (type)
            {
                case GameTypes.Chess: return new ChessGame();
                case GameTypes.Checkers: return new CheckerGame();
                default: throw new NotImplementedException();
            }
        }
    }
}
