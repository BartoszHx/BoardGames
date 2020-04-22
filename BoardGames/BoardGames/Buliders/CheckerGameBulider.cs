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
        public Action<MessageContents> Alert { get; private set; }
        public IPlayer Player { get; private set; }

        public CheckerGameBulider SetAlertMessage(Action<MessageContents> alert)
        {
            Alert = alert;
            return this;
        }

        public CheckerGameBulider SetPlayer(IPlayer player)
        {
            Player = player;
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
