using System;
using System.Collections.Generic;
using System.Text;
using BoardGamesClient.Buliders;
using BoardGamesClient.Interfaces;
using BoardGamesClient.Servers;

namespace BoardGamesClient.Configurations
{
    internal static class ServerConfiguration
    {
        public static ServerConnector GetServerConnector()
        {
            return new ServerConnectorBulider()
                .Host("localhost")
                .PortUser(50051)
                .PortGameOnline(50052)
                .Build();
        }
    }
}
