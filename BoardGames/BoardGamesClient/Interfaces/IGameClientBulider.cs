using System;
using System.Collections.Generic;
using System.Text;
using BoardGamesClient.Models;
using BoardGamesShared.Enums;
using BoardGamesShared.Interfaces;

namespace BoardGamesClient.Interfaces
{
    public interface IGameClientBulider
    {
        IGameClientBulider SetActionMessage(Action<Dictionary<string, string>> message);
        IGameClientBulider SetUser(User user);        
        IGameClientBulider SetChessGame(Action<MessageContents> alert, Func<IEnumerable<PawChess>, PawChess> chosePawUpgrade);
        IGameClientBulider SetCheckerGame(Action<MessageContents> alert);
        IGameClientBulider SetRereshViewAction(Action refreshView);
        IGameClient Bulid();
    }
}
