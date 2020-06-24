using BoardGamesShared.Enums;
using BoardGamesShared.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BoardGamesClient.Buliders.Games
{
    internal abstract class GameBulider
    {
        protected Action<MessageContents> alert;
        protected List<IPlayer> playerList;

        public virtual GameBulider SetPlayerList(List<IPlayer> playerList)
        {
            this.playerList = playerList;
            return this;
        }

        public abstract IGame Bulid();
    }
}
