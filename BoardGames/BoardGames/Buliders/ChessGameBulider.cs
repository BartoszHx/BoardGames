using BoardGames.Games.Chess;
using BoardGames.Interfaces;
using BoardGamesShared.Enums;
using BoardGamesShared.Interfaces;
using System;
using System.Collections.Generic;

namespace BoardGames.Buliders
{
    public class ChessGameBulider : IChessGameBulider
    {
        public Action<MessageContents> Alert { get; private set; }
        public Func<IEnumerable<PawChess>, PawChess> ChosePawUpgrade { get; private set; }
        public IBoard Board { get; private set; }

        public IPlayer PlayerTurn { get; private set; }

        public IList<IPlayer> PlayerList { get; private set; }

        public IList<IPawnHistory> PawnHistoriesList { get; private set; }

        public int Turn { get; private set; }

        public List<PawChess> PawToChoseList { get; private set; }

        internal ChessGameBulider()
        {
            PawnHistoriesList = new List<IPawnHistory>();
            PawToChoseList = new List<PawChess>() { PawChess.Bishop, PawChess.Knight, PawChess.Queen, PawChess.Rock };
            Turn = 1;
            
        }

        public ChessGameBulider SetAlertMessage(Action<MessageContents> alert)
        {
            Alert = alert;
            return this;
        }

        public ChessGameBulider SetChosePawUpgradeFunction(Func<IEnumerable<PawChess>, PawChess> chosePawUpgrade)
        {
            ChosePawUpgrade = chosePawUpgrade;
            return this;
        }

        public ChessGameBulider SetBoard(IBoard board)
        {
            Board = board;
            return this;
        }

        public ChessGameBulider SetPlayerList(IList<IPlayer> playerList)
        {
            PlayerList = playerList;
            return this;
        }

        public ChessGameBulider SetPlayerTurn(IPlayer player)
        {
            PlayerTurn = player;
            return this;
        }

        public IChessGame Bulid()
        {
            return new ChessGame(this);
        }
    }
}
