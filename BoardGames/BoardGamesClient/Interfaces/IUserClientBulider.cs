using System;
using System.Collections.Generic;
using System.Text;

namespace BoardGamesClient.Interfaces
{
    public interface IUserClientBulider
    {
        IUserClientBulider SetActionMessage(Action<Dictionary<string, string>> message);

        IUserClient Config();
    }
}
