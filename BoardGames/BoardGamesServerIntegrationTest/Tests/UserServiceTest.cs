using BoardGameDatabase.Operations;
using BoardGamesGrpc.SharedModel;
using BoardGamesGrpc.Users;
using BoardGamesOnline.Interfaces.Services;
using BoardGamesServer;
using DbContexts;
using Grpc.Core;
using NUnit.Framework;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using BoardGamesServerIntegrationTest.Contextx;
using User = BoardGameDatabase.Models.Entites.User;
using BoardGameDatabase.Buliders;

namespace BoardGamesServerIntegrationTest.Tests
{

    [TestFixture]
    internal class UserServiceTest
    {
        public UserService.UserServiceClient Client { get; private set; }
        public BoardGameDbContext Context { get; private set; }

        public UserServiceTest()
        {
            InitAutomapper();
            InitServer();
        }

        #region Init

        [SetUp]
        public void Init()
        {
            InitDataBase();            
        }

        private void InitAutomapper()
        {
            BoardGamesServer.Program.MapperInit();
        }

        private void InitServer()
        {
            string host = "localhost";
            int portUser = 50051;

            IUserService service = new BoardGamesOnline.Services.Users.UserService(new BoardGameUnitOfWorkBulider());
            UserServer server = new UserServer(service);

            // Build a server
            Server serverUser = new Server
                                    {
                                        Services = { UserService.BindService(server) },
                                        Ports = { new ServerPort(host, portUser, ServerCredentials.Insecure) }
                                    };
            serverUser.Start();

            //client
            var channel = new Channel($"{host}:{portUser}", ChannelCredentials.Insecure);
            Client = new UserService.UserServiceClient(channel);

        }

        private void InitDataBase()
        {
            Context = new BoardGameDbContextTest();
        }

        [TearDown]
        public void Dispose()
        {
            Context.Dispose();
        }

        #endregion

        #region Tools

        #endregion

        #region Test

        [Test]
        public void LoginSuccess()
        {
            string email = "a@a.abcd";
            string password = "pas1s";

            if (!Context.Users.Any(a => a.Email == email))
            {
                var passHash = HashEncryption.Hash(password);
                Context.Users.Add(new User { Email = email, Password = passHash, Name = "Test" });
                Context.SaveChanges();
            }

            LoginRequest request = (new LoginRequest { Email = email, Password = password });

            //Act
            UserResponse respons = Client.Login(request);

            //Assert
            Assert.NotNull(respons);
            Assert.NotNull(respons.User);
            Assert.AreEqual(respons.Respons.Status, ServiceResponseStatus.Ok);
            Assert.AreEqual(respons.User.Email, email);
        }

        [Test]
        public void LoginUnsuccessful()
        {
            UserResponse respons = Client.Login(new LoginRequest { Email = "a@a.a", Password = "Pass" });

            Assert.AreEqual(respons.Respons.Status, ServiceResponseStatus.Error);
            Assert.IsNull(respons.User);
            Assert.NotNull(respons.Respons.Messages);
        }

        [Test]
        public void RegistrationSuccess()
        {
            string email = "registration@a.abcd";
            string password = "pas1S:873";

            if (Context.Users.Any(a => a.Email == email))
            {
                var userDelete = Context.Users.First(f => f.Email == email);
                Context.Users.Remove(userDelete);
                Context.SaveChanges();
            }

            RegistrationRequest request = new RegistrationRequest { Email = email, Name = "Test1", Password = password, RepeatPassword = password };

            //Act
            ServiceResponse respons = Client.Registration(request);

            //Assert
            Assert.NotNull(respons);
            Assert.AreEqual(respons.Respons.Status,ServiceResponseStatus.Ok);
            Assert.NotNull(Context.Users.FirstOrDefault(f=> f.Email == email));
        }

        [Test]
        public void RegistrationUnsuccessful()
        {
            string email = "registration";
            string password = "pas1S:873";

            RegistrationRequest request = new RegistrationRequest { Email = email, Name = "Test1", Password = password, RepeatPassword = password };

            //Act
            ServiceResponse respons = Client.Registration(request);

            //Assert
            Assert.NotNull(respons);
            Assert.AreEqual(respons.Respons.Status, ServiceResponseStatus.Error);
            Assert.IsNull(Context.Users.FirstOrDefault(f => f.Email == email));
        }

        #endregion
    }
}
