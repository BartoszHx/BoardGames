using BoardGames.Games.Checkers;
using BoardGames.Games.Chess;
using BoardGamesShared.Enums;
using BoardGamesShared.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardGames.Buliders
{
    public class CheckerGameBulider
    {
        internal Action<MessageContents> Alert { get; private set; }
        internal List<IPlayer> PlayerList { get; private set; }

        public CheckerGameBulider SetAlertMessage(Action<MessageContents> alert)
        {
            Alert = alert;
            return this;
        }

        public CheckerGameBulider SetPlayerList(List<IPlayer> playerList)
        {
            PlayerList = playerList;
            return this;
        }

        public IGame Bulid()
        {
            return new CheckerGame(this);
        }

        private void ValidationBulid()
        {
            if (Alert == null)
            {
                throw new Exception("In CheckerGameBulider, Property Alert is not set");
            }
        }
    }
}
