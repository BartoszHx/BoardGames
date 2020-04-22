using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BoardGamesClient;
using BoardGamesClient.Models;
using BoardGamesClient.Responses;
using BoardGamesGrpc.Users;
using BoardGamesShared.Interfaces;
using Grpc.Core;
using GameOnlineGrpc = BoardGamesGrpc.GameOnlines;


namespace BoardGamesConsoleAppClient
{
    class Program
    {
        static void Main(string[] args)
        {
            /* To działa
            var playerList = new Google.Protobuf.Collections.RepeatedField<GameOnlineGrpc.Player>();
            playerList.Add(new GameOnlineGrpc.Player() { ID = 1, Name = "Test1", Color = 1 });
            playerList.Add(new GameOnlineGrpc.Player() { ID = 2, Name = "Test2", Color = 2 });

            GameOnlineGrpc.Game gameTest = new GameOnlineGrpc.Game()
            {
                Board = new GameOnlineGrpc.Board() { MaxHeight = 8, MaxWidth = 8},
                PlayerTurn = new GameOnlineGrpc.Player() { ID = 1, Name= "Test1", Color = 1}
        
            };
            gameTest.PlayerList.AddRange(playerList);


            var mtest = BoardGamesClient.Configurations.AutoMappers.Mapping.Mapper.Map<IGameData>(gameTest);
            var mtest2 = BoardGamesClient.Configurations.AutoMappers.Mapping.Mapper.Map<GameOnlineGrpc.Game>(mtest);
            */

            /*
            var matchuser = new Google.Protobuf.Collections.RepeatedField<GameOnlineGrpc.MatchUser>();
            matchuser.Add(new GameOnlineGrpc.MatchUser() { MatchId = 1, MatchResult = 0, MatchUserId = 2, User = null });
            matchuser.Add(new GameOnlineGrpc.MatchUser() { MatchId = 2, MatchResult = 0, MatchUserId = 3, User = null });
            matchuser.Add(new GameOnlineGrpc.MatchUser() { MatchId = 3, MatchResult = 0, MatchUserId = 5, User = null });

            GameOnlineGrpc.Match match = new GameOnlineGrpc.Match()
            {
                MatchId = 1,
                DateEnd = DateTime.Now.ToString(),
                DateStart = DateTime.Now.ToString(),
                GameType = 1
            };
            match.MatchUsers.AddRange(matchuser);

            var mtest = BoardGamesClient.Configurations.AutoMappers.Mapping.Mapper.Map<Match>(match);
            var mtest2 = BoardGamesClient.Configurations.AutoMappers.Mapping.Mapper.Map<GameOnlineGrpc.Match>(mtest);
            */
            /*
            //Działą!!! Wszystkie Dll musza mieć tą samą versję Grpc.Core, inaczje będzie komunikat że nie 
            //Teraz spróbować to przypiąć na wpfClient
            Program a = new Program();
            a.TestFullClient();
            //a.Test();
            //a.voidTestDouble();
            //a.LoginTest();
            */
            Console.ReadKey();

        }


        private void TestFullClient()
        {
            try
            {
                /*
                var client = new FullClientBulider().SetActionMessage((s) => Console.WriteLine(s)).Config();

                client.Login("TestEmail@", "pass");
                client.SearchOpponentAsync(GameTypes.Chess);
                */
                //client.UserClient.Login("TestEmail@", "pass");
                //client.GameClient.SearchOpponentAsync(GameTypes.Chess);
                
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        /*
        private void LoginTest()
        {
            try
            {

                this.client = new FullClient();
                //var a = this.client.UserService.Login("some@email.com", "Pass");
                var a = this.client.UserService.LoginTest(); //Ma referencje do grpc
                

                var stop = 0;
            }
            catch (Exception ex)
            {

            }


        }

        private async Task voidTestDouble()
        {
            this.client = new FullClient();

            Task a1 = this.client.Search();
            Task a2 = this.client.Search();

            await a1;
            await a2;

            await this.Wait();

        }

        public FullClient client;

        private async void Test()
        {
            try
            {
                this.client = new GameClient();
                this.client.TestAutoMapper();

                this.client.Search();
                Console.WriteLine("Ready");
                await this.Wait();
            }

            catch(Exception ex)
            {
                Console.WriteLine("Popsulo się");
            }
        }

        private async Task Wait()
        {
            while (1 == 1)
            {
                Console.ReadKey();
                Console.WriteLine("Click");

                //client.GameService.GamePlay
                if (client.GameService.GamePlay != null)
                {
                    client.PawnMove();
                    Console.WriteLine("PawnMove");
                }
            }
        }
        */
    }
}
