using BoardGamesGrpc.Users;
using Grpc.Core;
using System;
using System.Threading;
using System.Threading.Tasks;
using BoardGamesGrpc.GameOnlines;

namespace BoardGamesConsoleApp
{
    class Program
    {
        const string Host = "localhost";
        const int PortUser = 50051;
        const int PortGameOnline = 50052;


        private int UserID = 0;

        static void Main(string[] args)
        {
            //NormalTest();

            //new Program().PlayMatchTest();
            //new Program().BigTest();
            new Program().LoginTest();




            //new Program().SearchTest();
            //new Program().Cancel();

            Console.ReadKey();

        }

        private GamePlay GP;
        private int userID;

        private AsyncDuplexStreamingCall<PlayMatchRequest, GamePlay> _call;


        private async void BigTest()
        {
            this.PlayMatchTest();
            await this.Wait();

        }

        private async Task Wait()
        {
            while (1 == 1)
            {
                Console.ReadKey();

                if (this._call != null)
                {
                    this.GP.Match.MatchId += 1;
                    this._call.RequestStream.WriteAsync(new PlayMatchRequest { GamePlay = GP, UserId = userID });
                }
            }
        }

        private async Task PlayMatchTest()
        {
            var channel = new Channel($"{Host}:{PortGameOnline}", ChannelCredentials.Insecure);

            var client = new GameOnlineService.GameOnlineServiceClient(channel);

            userID = new Random().Next(1, 100);

            var search = client.SearchOpponentAsync(new SearchOpponentRequest { GameType = 1, UserId = userID });
            var match = await search;
            this.GP = new GamePlay();
            this.GP.Match = match.Match;

            using (_call = client.PlayMatch())
            {
                while (await this._call.ResponseStream.MoveNext(CancellationToken.None))
                {
                    GP = this._call.ResponseStream.Current;
                    Console.WriteLine(GP.Match.MatchId);
                }
            }

        }

        private async void SearchTest()
        {
            
            var channel = new Channel($"{Host}:{PortGameOnline}", ChannelCredentials.Insecure);

            var client = new GameOnlineService.GameOnlineServiceClient(channel);

            var user1Id = 1;
            var user2Id = 2;

            var a = client.SearchOpponentAsync(new SearchOpponentRequest { GameType = 1, UserId = user1Id });
            Thread.Sleep(2000);
            var b = client.SearchOpponentAsync(new SearchOpponentRequest { GameType = 1, UserId = user2Id });

            var c = await a;
            var d = await b;
            var stop = 1;
            stop += 2;

            Console.WriteLine(d.Match.MatchId);
        }



        static void NormalTest()
        {
            var channel = new Channel($"{Host}:{PortUser}", ChannelCredentials.Insecure);

            var client = new UserService.UserServiceClient(channel);


            var requestRegistration = new RegistrationRequest { Email = "test@aa.pl", Name = "SomeName", Password = "Go0]Pass", RepeatPassword = "Go0]Pass" };
            var responsRegistration = client.Registration(requestRegistration);


            var requestLogin = new LoginRequest { Email = "TestEmail@a.aa", Password = "14Jd3l1;" };
            var responsLogin = client.Login(requestLogin);

            var stop1 = 0;
        }


        private async void Cancel()
        {
            var channel = new Channel($"{Host}:{PortGameOnline}", ChannelCredentials.Insecure);

            var client = new GameOnlineService.GameOnlineServiceClient(channel);

            var user1Id = new Random().Next(1, 100);

            var a = client.SearchOpponentAsync(new SearchOpponentRequest { GameType = 1, UserId = user1Id });

            Thread.Sleep(2000);
           // client.CancelSearchOpponentAsync(new SearchOpponentRequest { GameType = 1, UserId = user1Id });


            var c = await a;

            var stop = 1;
            stop += 2;

            //Console.WriteLine(d.MatchId);
        }

        private void LoginTest()
        {
            var channel = new Channel($"{Host}:{PortUser}", ChannelCredentials.Insecure);

            var client = new UserService.UserServiceClient(channel);

            var abc = client.Login(new LoginRequest{Email = "some@email", Password = "Pass"});

            var stop = 1;
        }
    }
}
