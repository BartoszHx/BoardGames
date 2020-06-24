using BoardGames.Buliders;
using BoardGamesClient.Interfaces;
using BoardGamesShared.Enums;
using BoardGamesShared.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BoardGamesClient.Buliders.Games
{
    internal class CheckerBulider : GameBulider
    {
        public CheckerBulider(Action<MessageContents> alert)
        {
            this.alert = alert;
        }
        public override IGame Bulid()
        {
            return new CheckerGameBulider()
                .SetAlertMessage(alert)
                .SetPlayerList(playerList)
                .Bulid();
        }
    }
}
