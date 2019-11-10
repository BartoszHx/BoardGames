using System;
using System.Collections.Generic;
using System.Text;
using BoardGamesClient.Buliders;
using BoardGamesClient.Clients;
using BoardGamesClient.Interfaces;

namespace BoardGamesClient
{
    //todo Dodać ninject
    public static class ClientBulider
    {
        public static IUserClientBulider GetUser()
        {
            return new UserClientBulider();
        }

        public static IGameClientBulider GetGame()
        {
            return new GameClientBulider();
        }
    }
}
