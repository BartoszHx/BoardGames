using BoardGames.Games.Chess;
using BoardGamesShared.Enums;
using BoardGamesShared.Interfaces;
using BoardGamesTest.Models;
using System.Collections.Generic;

namespace BoardGamesTest.Games.Chess
{
    internal static class ChessHelper
    {
        public static IChessGame SetChessGame()
        {
            List<IPlayer> playerList = new List<IPlayer>();
            playerList.Add(new PlayerModel { Color = PawColors.White, ID = 1, Name = "Test1" });
            playerList.Add(new PlayerModel { Color = PawColors.Black, ID = 2, Name = "Test2" });

            IChessGame game = new ChessGameCreator().StandardGame(playerList, null, null);

            return game;
        }

        public static IChessGame SetChessGame(IBoard board)
        {
            List<IPlayer> playerList = new List<IPlayer>();
            playerList.Add(new PlayerModel { Color = PawColors.White, ID = 1, Name = "Test1" });
            playerList.Add(new PlayerModel { Color = PawColors.Black, ID = 2, Name = "Test2" });


            IChessGame game = new BoardGames.Buliders.ChessGameBulider()
                .SetBoard(board)
                .SetPlayerList(playerList)
                .SetPlayerTurn(playerList[0])
                .Bulid();

            return game;
        }

    }
}
