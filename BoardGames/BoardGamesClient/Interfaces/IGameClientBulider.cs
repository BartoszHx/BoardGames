using System;
using System.Collections.Generic;
using System.Text;
using BoardGamesClient.Models;

namespace BoardGamesClient.Interfaces
{
    public interface IGameClientBulider
    {
        IGameClientBulider SetActionMessage(Action<Dictionary<string, string>> message);
        IGameClientBulider SetUser(User user);
        IGameClient Config();
    }
}
