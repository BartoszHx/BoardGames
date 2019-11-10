using BoardGamesGrpc.GameOnlines;
using BoardGamesGrpc.Users;
using Grpc.Core;
using System;
using AutoMapper;
using BoardGamesServer.Configurations;

namespace BoardGamesServer
{
    internal class Program
    {
        const string Host = "localhost";
        const int PortUser = 50051;
        const int PortGameOnline = 50052;


        public static void Main(string[] args)
        {
            MapperInit();

            // Build a server
            var serverUser = new Server
                             {
                                 Services = { UserService.BindService(new UserServer(null))}, //Mapper!!!
                                 Ports = { new ServerPort(Host, PortUser, ServerCredentials.Insecure) }
                             };

            var serverGameOnline = new Server
                                 {
                                     Services = { GameOnlineService.BindService(new GameOnlineServer()) },
                                     Ports = { new ServerPort(Host, PortGameOnline, ServerCredentials.Insecure) }
                                 };

            // Start server
            serverUser.Start();
            Console.WriteLine(" listening on port User " + PortUser);

            serverGameOnline.Start();
            Console.WriteLine(" listening on port GameOnline " + PortGameOnline);


            Console.ReadKey();

            serverUser.ShutdownAsync().Wait();
        }

        public static void MapperInit()
        {
            /*
            Mapper.Initialize(cfg =>
                {
                    cfg.AddProfile<BoardGamesOnlineMapperProfile>();
                });
                */
        }
    }
}
