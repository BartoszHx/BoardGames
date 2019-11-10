using System;
using System.Collections.Generic;
using System.Text;
using BoardGamesClient.Clients;
using BoardGamesClient.Interfaces;
using BoardGamesClient.Models;
using BoardGamesClient.Servers;

namespace BoardGamesClient.Buliders
{
    internal class GameClientBulider : IGameClientBulider
    {
        internal Action<Dictionary<string, string>> Message { get; private set; }
        internal ServerConnector ServerConnector { get; private set; }
        internal User User { get; private set; }

        public GameClientBulider()
        {
            ServerConnector = Configurations.ServerConfiguration.GetServerConnector();
        }

        public IGameClientBulider SetActionMessage(Action<Dictionary<string, string>> message)
        {
            this.Message = message;
            return this;
        }

        public IGameClientBulider SetUser(User user)
        {
            this.User = user;
            return this;
        }

        public IGameClient Config()
        {
            //Tutaj ma być Ninject
            this.validBuild();
            return new GameClient(this);
        }

        private void validBuild()
        {
            if (Message == null)
            {
                throw new Exception("Message not set");
            }

            if (User == null)
            {
                throw  new Exception("User not set");
            }
        }
    }
}
