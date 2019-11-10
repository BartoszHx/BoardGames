using System;
using System.Collections.Generic;
using System.Text;
using BoardGamesClient.Clients;
using BoardGamesClient.Interfaces;
using BoardGamesClient.Servers;

namespace BoardGamesClient.Buliders
{
    internal class UserClientBulider : IUserClientBulider
    {
        internal Action<Dictionary<string, string>> Message { get; private set; }
        internal ServerConnector ServerConnector { get; private set; }

        public UserClientBulider()
        {
            ServerConnector = Configurations.ServerConfiguration.GetServerConnector();
        }

        public IUserClientBulider SetActionMessage(Action<Dictionary<string, string>> message)
        {
            this.Message = message;
            return this;
        }

        public IUserClient Config()
        {
            //Tutaj ma być Ninject
            this.validBuild();
            return new UserClient(this);
        }

        private void validBuild()
        {
            if (Message == null)
            {
                throw new Exception("Message not set");
            }
        }
    }
}
