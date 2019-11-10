using BoardGameDatabase;
using BoardGameDatabase.Enums;
using BoardGameDatabase.Interfaces;
using BoardGameDatabase.Interfaces.Validators;
using BoardGameDatabase.Models;
using BoardGameDatabase.Models.Entites;
using BoardGameDatabase.Validations;
using BoardGameDatabaseTest.Helpers;
using Moq;
using NUnit.Framework;
using NUnit.Framework.Internal;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using BoardGameDatabaseTest.Enums;
using Match = BoardGameDatabase.Models.Entites.Match;

namespace BoardGameDatabaseTest.Validations
{
    [TestFixture]
    internal class MatchServiceValidationTest
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

            var mockUserSet = new Mock<DbSet<User>>();
            mockUserSet.SetupData(userData);

            var mockContext = new Mock<IBoardGameDbContext>();
            mockContext.Setup(s => s.Users).Returns(mockUserSet.Object);

            IMatchServiceValidation serviceValidation = new MatchServiceValidation(new MatchValidation(), mockContext.Object);

            CreateMatch createMatchOk = new CreateMatch { GameType = (int)GameTypes.Chess, UserIdList = new List<int> { 1, 2 } };
            CreateMatch createMatchOkMultipleUser = new CreateMatch { GameType = (int)GameTypes.Chess, UserIdList = new List<int> { 1, 2, 3, 4 } };
            CreateMatch createMatchNoGameData = new CreateMatch { GameType = (int)GameTypes.Chess, UserIdList = new List<int> { 1, 2 } };
            CreateMatch createMatchNoUserList = new CreateMatch { GameType = (int)GameTypes.Chess, UserIdList = null };
            CreateMatch createMatchNoUserInDB = new CreateMatch { GameType = (int)GameTypes.Chess, UserIdList = new List<int> { 1, 6 } };


            //Act
            var resultOk = serviceValidation.Create(createMatchOk);
            var resultOkMultipleUser = serviceValidation.Create(createMatchOk);
            var resultNoGameData = serviceValidation.Create(createMatchNoGameData);
            var resultNoUserList = serviceValidation.Create(createMatchNoUserList);
            var resultMatchNoUserInDB = serviceValidation.Create(createMatchNoUserInDB);


            //Assert
            Assert.AreEqual(resultOk.IsSucces, true);
            Assert.AreEqual(resultOk.ErrorList.Count, 0);

            Assert.AreEqual(resultOkMultipleUser.IsSucces, true);
            Assert.AreEqual(resultOkMultipleUser.ErrorList.Count, 0);

            /*
            Assert.AreEqual(resultNoGameData.IsSucces, false);
            Assert.AreEqual(resultNoGameData.ErrorList.Any(a=> a.Key == ValidationKey.MatchNoJsonFormatGameData.ToString()), true);
            Assert.AreEqual(resultNoGameData.ErrorList.Count, 1);
            */
            Assert.AreEqual(resultNoUserList.IsSucces, false);
            Assert.AreEqual(resultNoUserList.ErrorList.Any(a => a.Key == ValidationKey.MatchNoUsers.ToString()), true);
            Assert.AreEqual(resultNoUserList.ErrorList.Count, 1);

            Assert.AreEqual(resultMatchNoUserInDB.IsSucces, false);
            Assert.AreEqual(resultMatchNoUserInDB.ErrorList.Any(a => a.Key == ValidationKey.MatchNoUser.ToString() && a.Value.Contains("6")), true);
            Assert.AreEqual(resultMatchNoUserInDB.ErrorList.Count, 1);
        }

        [Test]
        public void Update()
        {
            //Arrange
            var mockContext = new Mock<IBoardGameDbContext>();
            IMatchServiceValidation serviceValidation = new MatchServiceValidation(new MatchValidation(), mockContext.Object);

            Match matchOk = new Match() { MatchId = 1, DateStart = DateTime.Now, GameTypeId = (int)GameTypes.Chess, GameData = "{\"variable3\": 200,\"variable4\": 2}" };
            Match matchOkDataEnd = new Match() { MatchId = 1, DateStart = DateTime.Now, DateEnd = DateTime.Now.AddDays(1), GameTypeId = (int)GameTypes.Chess, GameData = "{\"variable3\": 200,\"variable4\": 2}" };

            Match matchNoMatchId = new Match() { DateStart = DateTime.Now, GameTypeId = (int)GameTypes.Chess, GameData = "{\"variable3\": 200,\"variable4\": 2}" };
            Match matchNull = null;
            Match matchNoGameData = new Match() { MatchId = 1, DateStart = DateTime.Now, GameTypeId = (int)GameTypes.Chess };
            Match matchIncorrectGameData = new Match() { MatchId = 1, DateStart = DateTime.Now, GameTypeId = (int)GameTypes.Chess, GameData = "{\"variable3\": 200,\"variable4" };
            Match matchNoDateStart = new Match() { MatchId = 1, GameTypeId = (int)GameTypes.Chess, GameData = "{\"variable3\": 200,\"variable4\": 2}" };
            Match matchBadEndData = new Match() { MatchId = 1, DateStart = DateTime.Now, DateEnd = DateTime.Now.AddDays(-1), GameTypeId = (int)GameTypes.Chess, GameData = "{\"variable3\": 200,\"variable4\": 2}" };
            Match matchIncorrect = new Match() { MatchId = 1, DateEnd = DateTime.Now.AddDays(-1), GameTypeId = (int)GameTypes.Chess, GameData = "{\"variable3\": 200,\"variable4\"" };


            //Act
            var resultOk = serviceValidation.Update(matchOk);
            var resultOkDataEnd = serviceValidation.Update(matchOkDataEnd);
            var resultNoMatchId = serviceValidation.Update(matchNoMatchId);
            var resultNull = serviceValidation.Update(matchNull);
            var resultNoGameData = serviceValidation.Update(matchNoGameData);
            var resultIncorrectGameData = serviceValidation.Update(matchIncorrectGameData);
            var resultNoDateStart = serviceValidation.Update(matchNoDateStart);
            var resultBadEndData = serviceValidation.Update(matchBadEndData);
            var resultIncorrect = serviceValidation.Update(matchIncorrect);


            //Assert
            Assert.AreEqual(resultOk.IsSucces, true);
            Assert.AreEqual(resultOk.ErrorList.Count, 0);

            Assert.AreEqual(resultOkDataEnd.IsSucces, true);
            Assert.AreEqual(resultOkDataEnd.ErrorList.Count, 0);

            Assert.AreEqual(resultNoMatchId.IsSucces, false);
            Assert.AreEqual(resultNoMatchId.ErrorList.Any(a=> a.Key == ValidationKey.MatchNoId.ToString()), true);
            Assert.AreEqual(resultNoMatchId.ErrorList.Count, 1);

            Assert.AreEqual(resultNull.IsSucces, false);
            Assert.AreEqual(resultNull.ErrorList.Any(a => a.Key == ValidationKey.IsNull.ToString()), true);
            Assert.AreEqual(resultNull.ErrorList.Count, 1);

            /*
            Assert.AreEqual(resultNoGameData.IsSucces, false);
            Assert.AreEqual(resultNoGameData.ErrorList.Any(a => a.Key == ValidationKey.MatchNoJsonFormatGameData.ToString()), true);
            Assert.AreEqual(resultNoGameData.ErrorList.Count, 1);
            */

            Assert.AreEqual(resultIncorrectGameData.IsSucces, false);
            Assert.AreEqual(resultIncorrectGameData.ErrorList.Any(a => a.Key == ValidationKey.MatchNoJsonFormatGameData.ToString()), true);
            Assert.AreEqual(resultIncorrectGameData.ErrorList.Count, 1);

            Assert.AreEqual(resultNoDateStart.IsSucces, false);
            Assert.AreEqual(resultNoDateStart.ErrorList.Any(a => a.Key == ValidationKey.MatchNullDateStart.ToString()), true);
            Assert.AreEqual(resultNoDateStart.ErrorList.Count, 1);

            Assert.AreEqual(resultBadEndData.IsSucces, false);
            Assert.AreEqual(resultBadEndData.ErrorList.Any(a => a.Key == ValidationKey.MatchIncorectDateEnd.ToString()), true);
            Assert.AreEqual(resultBadEndData.ErrorList.Count, 1);

            Assert.AreEqual(resultIncorrect.IsSucces, false);
            Assert.AreEqual(resultIncorrect.ErrorList.Any(a => a.Key == ValidationKey.MatchNoJsonFormatGameData.ToString()), true);
            Assert.AreEqual(resultIncorrect.ErrorList.Any(a => a.Key == ValidationKey.MatchNullDateStart.ToString()), true);
            Assert.AreEqual(resultIncorrect.ErrorList.Count, 2);
        }
    }
}
