using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BoardGamesClient;
using BoardGamesClient.Responses;
using BoardGamesGrpc.Users;
using BoardGamesShared.Enums;
using Grpc.Core;

namespace BoardGamesConsoleAppClient
{
    class Program
    {
        static void Main(string[] args)
        {
            //Działą!!! Wszystkie Dll musza mieć tą samą versję Grpc.Core, inaczje będzie komunikat że nie 
            //Teraz spróbować to przypiąć na wpfClient
            Program a = new Program();
            a.TestFullClient();
            //a.Test();
            //a.voidTestDouble();
            //a.LoginTest();
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
