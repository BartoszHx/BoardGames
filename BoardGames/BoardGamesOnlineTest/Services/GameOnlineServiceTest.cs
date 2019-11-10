using BoardGameDatabase.Interfaces;
using BoardGamesOnline.Models;
using BoardGamesOnlineTest.Helpers;
using BoardGamesShared.Enums;
using Moq;
using NUnit.Framework;
using NUnit.Framework.Internal;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Match = BoardGamesOnline.Models.Match;
using DbModels = BoardGameDatabase.Models.Entites;

namespace BoardGamesOnlineTest.Services
{
    [TestFixture]
    public class GameOnlineServiceTest
    {
        [Ignore("Zmodyfikować, test się nie kończy przez to że jest async")]
        public async Task SearchOpponentAsync()
        {
            var userData = new List<DbModels.User>
                               {
                                   new DbModels.User { Email = "email1@aaa.a", Name = "IhaveName1", Password = "1111", UserId = 1 },
                                   new DbModels.User { Email = "email2@aaa.a", Name = "IhaveName2", Password = "2222", UserId = 2 }
                               }.AsQueryable();

            var mockUserSet = new Mock<DbSet<DbModels.User>>();
            var mockMatchSet = new Mock<DbSet<DbModels.Match>>();

            mockUserSet.SetupData(userData);

            Mock<IBoardGameDbContext> mockContext = new Mock<IBoardGameDbContext>();
            mockContext.Setup(s => s.Matches).Returns(mockMatchSet.Object);
            mockContext.Setup(s => s.Users).Returns(mockUserSet.Object);


            BoardGamesOnline.Services.GameOnline.GameOnlineService mockService = new BoardGamesOnline.Services.GameOnline.GameOnlineService(new Mocks.BgDatabaseUowBuliderMock(mockContext.Object)); 


            Task<Match> opponentTask1 = mockService.SearchOpponentAsync(new SearchOpponent { UserId = 1, GameType = GameTypes.Chess });
            Task<Match> opponentTask2 = mockService.SearchOpponentAsync(new SearchOpponent { UserId = 2, GameType = GameTypes.Chess });
            mockService.ConnectPlayersToMatchAsync();

            Match opponent1 = await opponentTask1;
            Match opponent2 = await opponentTask2;


            Assert.NotNull(opponent1);
            Assert.NotNull(opponent2);

            //Assert.AreEqual(opponent1.Match.MatchId, 1);
            //Assert.AreEqual(opponent2.Match.MatchId, 1);

            
            Assert.AreEqual(opponent1.MatchUsers.Any(a=> a.User.UserId == 1), true);
            Assert.AreEqual(opponent1.MatchUsers.Any(a=> a.User.UserId == 2), true);
            Assert.AreEqual(opponent2.MatchUsers.Any(a => a.User.UserId == 1), true);
            Assert.AreEqual(opponent2.MatchUsers.Any(a => a.User.UserId == 2), true);
        }

        [Test]
        public async Task CancelSearchOpponentAsync()
        {
            var mockContext = new Mock<IBoardGameDbContext>();
            var mockService = new BoardGamesOnline.Services.GameOnline.GameOnlineService(new Mocks.BgDatabaseUowBuliderMock(mockContext.Object));
            var searchUser = new SearchOpponent { UserId = 1, GameType = GameTypes.Chess };

            //Act
            Task<Match> opponentTask1 = mockService.SearchOpponentAsync(searchUser);
            Thread.Sleep(3000);
            mockService.CancelSearchOpponentAsync(searchUser.UserId);

            Match opponent1 = await opponentTask1;


            Assert.IsNull(opponent1);
        }
    }
}
