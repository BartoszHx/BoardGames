using System;
using System.Collections.Generic;
using System.Linq;
using BoardGames.Games.Chess;
using BoardGames.Interfaces;
using BoardGamesShared.Enums;
using BoardGamesShared.Interfaces;

namespace BoardGames.Buliders
{
    public class ChessGameStandardBulider : IChessGameBulider
    {
        public Action<MessageContents> Alert { get; private set; }
        public Func<IEnumerable<PawChess>, PawChess> ChosePawUpgrade { get; private set; }
        public IBoard Board { get; private set; }
        public IPlayer PlayerTurn { get; private set; }
        public IList<IPlayer> PlayerList { get; private set; }
        public IList<IPawnHistory> PawnHistoriesList { get; private set; }
        public int Turn { get; private set; }
        public List<PawChess> PawToChoseList { get; private set; }


        public ChessGameStandardBulider SetAlertMessage(Action<MessageContents> alert)
        {
            Alert = alert;
            return this;
        }

        public ChessGameStandardBulider SetChosePawUpgradeFunction(Func<IEnumerable<PawChess>, PawChess> chosePawUpgrade)
        {
            ChosePawUpgrade = chosePawUpgrade;
            return this;
        }

        public ChessGameStandardBulider SetPlayerList(IList<IPlayer> playerList)
        {
            PlayerList = playerList;
            return this;
        }

        public IChessGame Bulid()
        {
            Board = new ChessBoardBulider().Bulid();
            PawnHistoriesList = new List<IPawnHistory>();
            PawToChoseList = new List<PawChess>() { PawChess.Bishop, PawChess.Knight, PawChess.Queen, PawChess.Rock };
            Turn = 1;

            SetStartPlayers();

            var game = new ChessGame(this);
            game.SetStartPositionPaws();
            return game;
        }

        private void SetStartPlayers()
        {
            bool isNotSetFirstPlayer = PlayerList.All(a => a.Color == PawColors.White)
                                    || PlayerList.All(a => a.Color == PawColors.Black);

            if (!isNotSetFirstPlayer)
            {
                PlayerTurn = PlayerList.First(f => f.Color == PawColors.White);
                return;
            }

            Random rand = new Random();

            var playerFirst = PlayerList[rand.Next(0, 1)];
            var playerSecond = PlayerList.First(f => f != playerFirst);

            playerFirst.Color = PawColors.White;
            playerSecond.Color = PawColors.Black;

            PlayerTurn = playerFirst;
        }
    }
}
