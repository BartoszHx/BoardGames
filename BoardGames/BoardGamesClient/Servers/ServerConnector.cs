using System;
using System.Collections.Generic;
using System.Text;
using BoardGamesGrpc.GameOnlines;
using BoardGamesGrpc.SharedModel;
using BoardGamesGrpc.Users;
using Grpc.Core;

namespace BoardGamesClient.Servers
{
    internal class ServerConnector
    {
        private GameOnlineService.GameOnlineServiceClient gameClient;
        public GameOnlineService.GameOnlineServiceClient GameClient => this.GameOnlineClientCreate();

        private UserService.UserServiceClient userClient;
        public UserService.UserServiceClient UserClient => this.UserClientCreate();

        private string Host;

        private int PortGameOnline;

        private int PortUser;


        internal ServerConnector(string host, int portGameOnline, int portUser)
        {
            Host = host;
            PortGameOnline = portGameOnline;
            PortUser = portUser;
        }

        private GameOnlineService.GameOnlineServiceClient GameOnlineClientCreate()
        {
            if (this.gameClient == null)
            {
                var channel = new Channel($"{this.Host}:{this.PortGameOnline}", ChannelCredentials.Insecure);
                gameClient = new GameOnlineService.GameOnlineServiceClient(channel);
            }
            
            return this.gameClient;
        }

        private UserService.UserServiceClient UserClientCreate()
        {
            if (this.userClient == null)
            {
                var channel = new Channel($"{this.Host}:{this.PortUser}", ChannelCredentials.Insecure);
                this.userClient = new UserService.UserServiceClient(channel);
            }

            return this.userClient;
        }
    }
}
