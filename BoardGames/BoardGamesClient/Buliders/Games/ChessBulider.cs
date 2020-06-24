using BoardGames.Games.Chess;
using BoardGamesClient.Interfaces;
using BoardGamesShared.Enums;
using BoardGamesShared.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BoardGamesClient.Buliders.Games
{
    internal class ChessBulider : GameBulider
    {
        protected Func<IEnumerable<PawChess>, PawChess> chosePawUpgrade;

        public ChessBulider(Action<MessageContents> alert, Func<IEnumerable<PawChess>, PawChess> chosePawUpgrade)
        {
            this.alert = alert;
            this.chosePawUpgrade = chosePawUpgrade;
        }

        public override IGame Bulid()
        {
            return new ChessGameCreator().StandardGame(playerList, alert, chosePawUpgrade);
        }
    }
}
