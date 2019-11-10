using BoardGameDatabase.Models.Entites;
using BoardGamesGrpc.GameOnlines;
using BoardGamesGrpc.SharedModel;
using BoardGamesServer;
using BoardGamesServerIntegrationTest.Contextx;
using DbContexts;
using Grpc.Core;
using NUnit.Framework;
using NUnit.Framework.Internal;
using System;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using User = BoardGameDatabase.Models.Entites.User;

namespace BoardGamesServerIntegrationTest.Tests
{
    [TestFixture]
    internal class GameClientTest
    {
        public BoardGamesGrpc.GameOnlines.GameOnlineService.GameOnlineServiceClient Client { get; private set; }
        public BoardGameDbContext Context { get; private set; }

        public GameClientTest()
        {
            BoardGamesServer.Program.MapperInit();
            InitServer();
            InitDatabaseEnums();
        }

        #region Init

        [SetUp]
        public void Init()
        {
            InitDataBase();
        }

        private void InitServer()
        {
            string host = "localhost";
            int portGame = 50052;

            GameOnlineServer server = new GameOnlineServer();

            // Build a server
            var serverGameOnline = new Server
                                       {
                                           Services = { BoardGamesGrpc.GameOnlines.GameOnlineService.BindService(new GameOnlineServer()) },
                                           Ports = { new ServerPort(host, portGame, ServerCredentials.Insecure) }
                                       };
            serverGameOnline.Start();


            //client
            var channel = new Channel($"{host}:{portGame}", ChannelCredentials.Insecure);
            Client = new BoardGamesGrpc.GameOnlines.GameOnlineService.GameOnlineServiceClient(channel);

        }
        private void InitDataBase()
        {
            Context = new BoardGameDbContextTest();
        }
        private void InitDatabaseEnums()
        {
            using (var context = new BoardGameDbContextTest())
            {
                context.GameTypes.AddOrUpdate(new GameType() { GameTypeId = 1, Name = "Chess" }); //Szachy
                context.GameTypes.AddOrUpdate(new GameType() { GameTypeId = 2, Name = "Checkers" });
                
                context.MatchResults.AddOrUpdate(new MatchResult() { MatchResultId = (int)BoardGameDatabase.Enums.MatchResults.InProgress, Name = "InProgress" });
                context.MatchResults.AddOrUpdate(new MatchResult() { MatchResultId = (int)BoardGameDatabase.Enums.MatchResults.Disconnected, Name = "Disconnected" });
                context.MatchResults.AddOrUpdate(new MatchResult() { MatchResultId = (int)BoardGameDatabase.Enums.MatchResults.Draw, Name = "Draw" });
                context.MatchResults.AddOrUpdate(new MatchResult() { MatchResultId = (int)BoardGameDatabase.Enums.MatchResults.Loss, Name = "Loss" });
                context.MatchResults.AddOrUpdate(new MatchResult() { MatchResultId = (int)BoardGameDatabase.Enums.MatchResults.Win, Name = "Win" });

                context.SaveChanges();
            }
        }

        [TearDown]
        public void Dispose()
        {
            Context.Dispose();
        }

        #endregion

        #region Tools

        private int getRandomUserId(int notThisId = 0)
        {
            int count = Context.Users.Count();
            if (count == 0)
            {
                var user = this.addRandomUser();
                return user.UserId;
            }

            var findUser = Context.Users.FirstOrDefault(f => f.UserId != notThisId);

            if (findUser != null)
            {
                return findUser.UserId;
            }

            var newUser = this.addRandomUser();
            return newUser.UserId;
        }

        private User addRandomUser()
        {
            string name = "test" + new Random(DateTime.Now.Millisecond).Next(0, 10000);
            var user = Context.Users.Add(new User { Email = name + "@a.a", Name = name, Password = "" });
            Context.SaveChanges();
            return user;
        }

        #endregion


        //Co testować?
        //Search async
        //Cancel
        //PlayMatch
        //Jakiś reconnect itp?
        #region Tests

        [Ignore("Zmodyfikować, test się nie kończy przez to że jest async")]
        public async Task SearchOpponentSucces()
        {
            //Test nie jest pełny
            int userIdFirst = getRandomUserId();
            int userIdSecend = getRandomUserId(userIdFirst);
            int gameType = 1; //Hmmm, skąd ma wiedzieć że to id jakiejś gry?

            //Problem
            //Co zrobić z GameData? Pomysł, ma przechowywać przebieg rozgrywki, dodatkowe dane itp. Na starcie w sumie jest puste... ale czy na pewno tak ma być? 

            //Act
            AsyncUnaryCall<SearchOpponentRespons> responsAsync1 = Client.SearchOpponentAsync(new SearchOpponentRequest { GameType = gameType, UserId = userIdFirst });
            AsyncUnaryCall<SearchOpponentRespons> responsAsync2 = Client.SearchOpponentAsync(new SearchOpponentRequest { GameType = gameType, UserId = userIdSecend });

            SearchOpponentRespons respons1 = await responsAsync1;
            SearchOpponentRespons respons2 = await responsAsync2;

            //Assert
            Assert.NotNull(respons1);
            Assert.NotNull(respons2);
            Assert.AreEqual(respons1.Respons.Status, ServiceResponseStatus.Ok);
            Assert.AreEqual(respons2.Respons.Status, ServiceResponseStatus.Ok);
            Assert.NotNull(respons1.Match);
            Assert.NotNull(respons2.Match);
            Assert.IsTrue(respons1.Match.MatchUsers.Any());
            Assert.IsTrue(respons2.Match.MatchUsers.Any());
            Assert.IsTrue(respons1.Match.MatchUsers.All(al => al.User != null));
            Assert.IsTrue(respons2.Match.MatchUsers.All(al => al.User != null));
            Assert.AreEqual(respons1.Match.MatchId, respons2.Match.MatchId);
            Assert.AreEqual(respons1.Match.MatchUsers.Select(s => s.MatchUserId).ToList(), respons2.Match.MatchUsers.Select(s => s.MatchUserId).ToList());
        }

        [Test]
        public async Task CancelSearch()
        {
            int userId = this.getRandomUserId();
            int gameType = 1;

            //ACT
            AsyncUnaryCall<SearchOpponentRespons> responsSearchAsync = Client.SearchOpponentAsync(new SearchOpponentRequest { GameType = gameType, UserId = userId });
            Thread.Sleep(100);
            CancelSearchOpponentRespons respons = Client.CancelSearchOpponent(new CancelSearchOpponentRequest{ UserId = userId });
            var responSearch = await responsSearchAsync;

            //Assert
            Assert.NotNull(respons);
            Assert.AreEqual(respons.Respons.Status, ServiceResponseStatus.Ok);

            Assert.NotNull(responSearch);
            Assert.AreEqual(responSearch.Respons.Status, ServiceResponseStatus.Cancel);
        }

        [Test]
        public void CancelSearchUserNoSearchMatch()
        {
            int userId = this.getRandomUserId();

            //ACT
            CancelSearchOpponentRespons respons = Client.CancelSearchOpponent(new CancelSearchOpponentRequest { UserId = userId });

            //Assert
            Assert.NotNull(respons);
            Assert.AreEqual(respons.Respons.Status, ServiceResponseStatus.Ok);
        }

        [Ignore("Zmodyfikować, test się nie kończy przez to że jest async")]
        ///Sprawdza czy da się połączyć i przesłać jakie kolwiek dane
        public async Task PlayMatch()
        {
            int userIdFirst = getRandomUserId();
            int userIdSecend = getRandomUserId(userIdFirst);
            int gameType = 1;
            GamePlay gp1 = null;
            GamePlay gp2 = null;
            AsyncDuplexStreamingCall<PlayMatchRequest, GamePlay> call = Client.PlayMatch();
            var dateStartNew = DateTime.Now.AddDays(10).ToString();
            var setDateEnd = DateTime.Now.AddDays(20).ToShortDateString();

            //Act
            AsyncUnaryCall<SearchOpponentRespons> responsAsync1 = Client.SearchOpponentAsync(new SearchOpponentRequest { GameType = gameType, UserId = userIdFirst });
            AsyncUnaryCall<SearchOpponentRespons> responsAsync2 = Client.SearchOpponentAsync(new SearchOpponentRequest { GameType = gameType, UserId = userIdSecend });
            SearchOpponentRespons respons1 = await responsAsync1;
            SearchOpponentRespons respons2 = await responsAsync2;

            gp1 = new GamePlay{ Match = respons1.Match };
            gp2 = new GamePlay{ Match = respons2.Match };

            gp1.Match.DateStart = dateStartNew;
            call.RequestStream.WriteAsync(new PlayMatchRequest { GamePlay = gp1, UserId = userIdFirst }); //Wywołuje ruch
            while (await call.ResponseStream.MoveNext(CancellationToken.None))
            {
                gp2 = call.ResponseStream.Current;
                break;
            }

            gp2.Match.DateEnd = setDateEnd;
            call.RequestStream.WriteAsync(new PlayMatchRequest { GamePlay = gp2, UserId = userIdSecend}); //Wywołuje ruch
            while (await call.ResponseStream.MoveNext(CancellationToken.None))
            {
                gp1 = call.ResponseStream.Current;
                break;
            }

            //Asert
            Assert.NotNull(gp1);
            Assert.NotNull(gp2);
            Assert.NotNull(gp1.Match);
            Assert.NotNull(gp2.Match);
            Assert.AreEqual(gp2.Match.DateStart, dateStartNew);
            Assert.AreEqual(gp1.Match.DateStart, dateStartNew);
            Assert.AreEqual(gp2.Match.DateEnd, setDateEnd);
            Assert.AreEqual(gp1.Match.DateEnd, setDateEnd);
        }

        #endregion

    }
}
