using System;
using BoardGameDatabase.Enums;
using BoardGameDatabase.Interfaces;
using BoardGameDatabase.Interfaces.Services;
using BoardGameDatabase.Models;
using BoardGameDatabase.Models.Entites;
using BoardGameDatabaseTest.Helpers;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using BoardGameDatabase.Services;
using BoardGameDatabase.Validations;
using BoardGameDatabaseTest.Enums;
using Match = BoardGameDatabase.Models.Entites.Match;
using BoardGameDatabase.Buliders;

namespace BoardGameDatabaseTest.Services
{
    [TestFixture]
    public class MatchServiceTest
    {
        [Test]
        public void Create()
        {
            //Arrange
            var userData = new List<User>
                               {
                                   new User { Email = "email1@aaa.a", Name = "IhaveName1", Password = "1111", UserId = 1 },
                                   new User { Email = "email2@aaa.a", Name = "IhaveName2", Password = "1112", UserId = 2 },
                                   new User { Email = "email3@aaa.a", Name = "IhaveName3", Password = "1113", UserId = 3 },
                                   new User { Email = "email4@aaa.a", Name = "IhaveName4", Password = "1114", UserId = 4 }
                                   }.AsQueryable();


            var mockMatchSet = new Mock<DbSet<Match>>();
            var mockUserSet = new Mock<DbSet<User>>();
            mockUserSet.SetupData(userData);

            var mockContext = new Mock<IBoardGameDbContext>();
            mockContext.Setup(s => s.Matches).Returns(mockMatchSet.Object);
            mockContext.Setup(s => s.Users).Returns(mockUserSet.Object);

            IMatchService service = new MatchService(mockContext.Object,new MatchServiceValidationBulider() );

            //Act
            /*
            var result1 = service.Create(new CreateMatch { GameData = "{\"variable1\": 100,\"variable2\": 1}", GameType = (int)GameTypes.Chess, UserIdList = new List<int> { 1, 2 } });
            var result2 = service.Create(new CreateMatch { GameData = "{\"variable3\": 200,\"variable4\": 2}", GameType = (int)GameTypes.Chess, UserIdList = new List<int> { 3, 4 } });
            var result3 = service.Create(new CreateMatch { GameData = "{\"variable3\": 200,\"variable4\": 2", GameType = (int)GameTypes.Checkers, UserIdList = new List<int> { 2, 4 } });
            var result4 = service.Create(new CreateMatch { GameData = "{\"variable3\": 200,\"variable4\": 2}", GameType = (int)GameTypes.Checkers, UserIdList = new List<int> { 6, 7 } });
            */
            var result1 = service.Create(new CreateMatch { GameType = (int)GameTypes.Chess, UserIdList = new List<int> { 1, 2 } });
            var result2 = service.Create(new CreateMatch { GameType = (int)GameTypes.Chess, UserIdList = new List<int> { 3, 4 } });
            var result3 = service.Create(new CreateMatch { GameType = (int)GameTypes.Checkers, UserIdList = new List<int> { 2, 4 } });
            var result4 = service.Create(new CreateMatch { GameType = (int)GameTypes.Checkers, UserIdList = new List<int> { 6, 7 } });

            //Assert
            Assert.AreEqual(result1.Status, BoardGameDatabase.Enums.ServiceRespondStatus.Ok);
            Assert.NotNull(result1.Match, "result1.Match == null");
            Assert.AreEqual(result2.Status, BoardGameDatabase.Enums.ServiceRespondStatus.Ok);
            Assert.NotNull(result2.Match, "result2.Match == null");
            //Assert.AreEqual(result3.Status, BoardGameDatabase.Enums.ServiceRespondStatus.Error);
            //Assert.AreEqual(result3.Messages.Keys.Any(a => a == ValidationKey.MatchNoJsonFormatGameData.ToString()), true);
            Assert.AreEqual(result4.Status, BoardGameDatabase.Enums.ServiceRespondStatus.Error);
            Assert.AreEqual(result4.Messages.Keys.Any(a => a == ValidationKey.MatchNoUser.ToString()), true);
        }

        [Test]
        public void Update()
        {
            //Arrange
            var userData = new List<User>
                               {
                                   new User { Email = "email1@aaa.a", Name = "IhaveName1", Password = "1111", UserId = 1 },
                                   new User { Email = "email2@aaa.a", Name = "IhaveName2", Password = "1112", UserId = 2 },
                                   new User { Email = "email3@aaa.a", Name = "IhaveName3", Password = "1113", UserId = 3 },
                                   new User { Email = "email4@aaa.a", Name = "IhaveName4", Password = "1114", UserId = 4 }
                               }.AsQueryable();
            
            //svar matchUserData = new List<MatchUser>{ new MatchUser{MatchId = 1, }}

            var matchData = new List<Match>
                                {
                                    new Match { DateEnd = null, DateStart = DateTime.Now, GameData = "", GameTypeId = (int)GameTypes.Chess, MatchId = 1}

                                }.AsQueryable();

            var matchUserData = new List<MatchUser>
                                {
                                    new MatchUser { MatchId = 1, UserId = 1},
                                    new MatchUser { MatchId = 1, UserId = 2}
                                }.AsQueryable();


            var mockMatchSet = new Mock<DbSet<Match>>();
            var mockUserSet = new Mock<DbSet<User>>();
            var mockMatchUserSet = new Mock<DbSet<MatchUser>>();

            mockUserSet.SetupData(userData);
            mockMatchSet.SetupData(matchData);
            mockMatchUserSet.SetupData(matchUserData);

            var mockContext = new Mock<IBoardGameDbContext>();
            mockContext.Setup(s => s.Matches).Returns(mockMatchSet.Object);
            mockContext.Setup(s => s.Users).Returns(mockUserSet.Object);
            mockContext.Setup(s => s.MatchUsers).Returns(mockMatchUserSet.Object);

            IMatchService service = new MatchService(mockContext.Object, new MatchServiceValidationBulider());


            Match matchOk = new Match() { MatchId = 1, DateStart = DateTime.Now, GameTypeId = (int)GameTypes.Chess, GameData = "{\"variable3\": 200,\"variable4\": 2}" };
            Match matchNoMatchId = new Match() { DateStart = DateTime.Now, GameTypeId = (int)GameTypes.Chess, GameData = "{\"variable3\": 200,\"variable4\": 2}" };
            Match matchIncorectData = new Match() { MatchId = 1, GameTypeId = (int)GameTypes.Chess, GameData = "{\"variable3\": 200,\"variable4\":" };


            //Act
            var respondOk = service.Update(matchOk);
            var respondNoMatchId = service.Update(matchNoMatchId);
            var respondIncorectData = service.Update(matchIncorectData);


            //Assert
            Assert.AreEqual(respondOk.Status, ServiceRespondStatus.Ok);
            Assert.AreEqual(respondOk.Messages.Count, 0);

            Assert.AreEqual(respondNoMatchId.Status, ServiceRespondStatus.Error);
            Assert.AreEqual(respondNoMatchId.Messages.Count, 1);

            Assert.AreEqual(respondIncorectData.Status, ServiceRespondStatus.Error);
        }
    }
}
