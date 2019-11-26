using BoardGames.Models.Checkers;
using BoardGamesShared.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardGames.Factories
{
    public static class GameFactory
    {
        public static IGame Create(BoardGamesShared.Enums.GameTypes gameType)
        {
            switch (gameType)
            {
                case BoardGamesShared.Enums.GameTypes.Chess:
                    return new Games.Chess.ChessGame();
                case BoardGamesShared.Enums.GameTypes.Checkers:
                    return new Games.Checkers.CheckerGame();
                default:
                    throw new NotImplementedException();
            }
        }
    }
}
