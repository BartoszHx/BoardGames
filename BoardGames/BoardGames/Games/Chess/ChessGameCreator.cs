using BoardGames.Buliders;
using BoardGamesShared.Enums;
using BoardGamesShared.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardGames.Games.Chess
{
    public class ChessGameCreator
    {

        public IChessGame StandardGame(List<IPlayer> playerList, Action<MessageContents> alert, Func<IEnumerable<PawChess>, PawChess> chosePawUpgrade)
        {
            IBoard board = StandardChessBoard();
            IPlayer startPlayer = SetStartPlayers(playerList);

            return new ChessGameBulider()
                .SetBoard(board)
                .SetPlayerList(playerList)
                .SetPlayerTurn(startPlayer)
                .SetAlertMessage(alert) // co zrobić z tym?
                .SetChosePawUpgradeFunction(chosePawUpgrade) //co zrobić z tym?
                .Bulid();
        }

        private IPlayer SetStartPlayers(List<IPlayer> playerList)
        {
            bool isNotSetFirstPlayer = playerList.All(a => a.Color == PawColors.White)
                                    || playerList.All(a => a.Color == PawColors.Black);

            if (!isNotSetFirstPlayer)
            {
                return playerList.First(f => f.Color == PawColors.White);
            }

            Random rand = new Random();

            IPlayer playerFirst = playerList[rand.Next(0, 1)];
            IPlayer playerSecond = playerList.First(f => f != playerFirst);

            playerFirst.Color = PawColors.White;
            playerSecond.Color = PawColors.Black;

            return playerFirst;
        }

        private IBoard StandardChessBoard()
        {
            return new ChessBoardBulider()
                .SetAllPawnInHeigh(PawChess.Pawn, 2, PawColors.White)
                .SetPawnInTwoCorner(PawChess.Rock, 1, 1, PawColors.White)
                .SetPawnInTwoCorner(PawChess.Knight, 1, 2, PawColors.White)
                .SetPawnInTwoCorner(PawChess.Bishop, 1, 3, PawColors.White)
                .SetPawn(PawChess.Queen, 1, 4, PawColors.White)
                .SetPawn(PawChess.King, 1, 5, PawColors.White)
                .SetAllPawnInHeigh(PawChess.Pawn, 7, PawColors.Black)
                .SetPawnInTwoCorner(PawChess.Rock, 8, 1, PawColors.Black)
                .SetPawnInTwoCorner(PawChess.Knight, 8, 2, PawColors.Black)
                .SetPawnInTwoCorner(PawChess.Bishop, 8, 3, PawColors.Black)
                .SetPawn(PawChess.Queen, 8, 5, PawColors.Black)
                .SetPawn(PawChess.King, 8, 4, PawColors.Black)
                .Bulid();
        }
    }
}
