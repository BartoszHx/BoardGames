using BoardGameDatabase.Interfaces.Services;
using BoardGameDatabase.Models.Entites;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

using BoardGameDatabase;
using BoardGameDatabase.Enums;
using BoardGameDatabase.Interfaces;
using BoardGameDatabase.Services;
using BoardGameDatabase.Validations;

using BoardGameDatabaseTest.Helpers;

namespace BoardGameDatabaseTest.Services
{
    [TestFixture]
    public class UserServiceTest
    {
        [Test]
        public void Registartion()
        {
            //Arrange
            var userData = new List<User>
                               {
                                   new User { Email = "duplicatemail@a.aa", Name = "IhaveName1", Password = "12345", UserId = 1 }
                               }.AsQueryable();

            var mockUserSet = new Mock<DbSet<User>>();
            var mockContext = new Mock<IBoardGameDbContext>();
            mockUserSet.SetupData(userData);
            mockContext.Setup(s => s.Users).Returns(mockUserSet.Object);

            IUserService service = new UserService(mockContext.Object, new UserServiceValidation(new UserValidation(), new PasswordValidation(), mockContext.Object));

            //IUserService service = BoardGameDatabase.StaticKernel.Get<IUserService>("context", mockContext.Object);

            var user1 = new User();
            user1.Email = "aaa@a.aa";
            user1.Name = "Name1";
            string password1 = "Has1o729!";

            var user2 = new User();
            user2.Email = "bbb@a.aa";
            user2.Name = "Name2";
            string password2 = "AhLo20&..11";

            var user3 = new User();
            user3.Email = "aaa";
            user3.Name = "Name3";
            string password3 = "Has1o729";

            var user4 = new User();
            user4.Email = "dddd@a.aa";
            user4.Name = "Name4";
            string password4 = "badpass";

            var user5 = new User();
            user5.Email = "duplicatemail@a.aa";
            user5.Name = "Name5";
            string password5 = "Dasl1001";

            //Act
            var result1 = service.Registration(user1, password1, password1);
            var result2 = service.Registration(user2, password2, password2);
            var result3 = service.Registration(user3, password3, password3);
            var result4 = service.Registration(user4, password4, password4);
            var result5 = service.Registration(user5, password5, password5);
            var result6 = service.Registration(user5, password5, "repeatPasswordWrong");


            //Assert
            Assert.AreEqual(result1.Status, ServiceRespondStatus.Ok);
            Assert.AreEqual(result2.Status, ServiceRespondStatus.Ok);
            Assert.AreEqual(result3.Status, ServiceRespondStatus.Error);
            Assert.AreEqual(result3.Messages.Any(a=> a.Key == ValidationKey.EmailIncorrect.ToString()), true);
            Assert.AreEqual(result4.Status, ServiceRespondStatus.Error);
            Assert.AreEqual(result4.Messages.Any(a => a.Key == ValidationKey.PasswordTooShort.ToString()), true);
            Assert.AreEqual(result4.Messages.Any(a => a.Key == ValidationKey.PasswordNoDigit.ToString()), true);
            Assert.AreEqual(result4.Messages.Any(a => a.Key == ValidationKey.PasswordNoUpperSymbol.ToString()), true);
            Assert.AreEqual(result5.Status, ServiceRespondStatus.Error);
            Assert.AreEqual(result5.Messages.Any(a => a.Key == ValidationKey.EmailDuplicate.ToString()), true);
            Assert.AreEqual(result6.Status, ServiceRespondStatus.Error);
            Assert.AreEqual(result6.Messages.Any(a => a.Key == ValidationKey.PasswordRepeatNotThisSame.ToString()), true);

        }

        [Test]
        public void Login()
        {
            //Arrange
            string email = "someemail@email.aaa";
            string password = "Som3Pa$sw0rd";
            string hashPassword = BoardGameDatabase.Operations.HashEncryption.Hash(password);

            var userData = new List<User>
                               {
                                   new User { Email = email, Name = "IhaveName", Password = hashPassword, UserId = 1 }
                               }.AsQueryable();

            var mockUserSet = new Mock<DbSet<User>>();
            mockUserSet.SetupData(userData);

            var mockContext = new Mock<IBoardGameDbContext>();
            mockContext.Setup(m => m.Users).Returns(mockUserSet.Object);

            IUserService service = new UserService(mockContext.Object, new UserServiceValidation(new UserValidation(), new PasswordValidation(), mockContext.Object));

            //Act
            var result1 = service.Login(email, password);
            var result2 = service.Login("noEmail@aaa.pl", password);
            var result3 = service.Login(email, "pustka");
            

            //Assert
            Assert.AreEqual(result1.Status, BoardGameDatabase.Enums.ServiceRespondStatus.Ok);
            Assert.IsNotNull(result1.User);
            Assert.AreEqual(result2.Status, BoardGameDatabase.Enums.ServiceRespondStatus.Error);
            Assert.AreEqual(result2.Messages.Any(a=> a.Key == ValidationKey.CantLogin.ToString()), true);
            Assert.IsNull(result2.User);
            Assert.AreEqual(result3.Status, BoardGameDatabase.Enums.ServiceRespondStatus.Error);
            Assert.AreEqual(result3.Messages.Any(a => a.Key == ValidationKey.CantLogin.ToString()), true);
            Assert.IsNull(result3.User);
        }

        [Test]
        public void ChangePassword()
        {
            //Arrange
            string password1 = "Som3Pa$sw0rd";
            string password2 = "Som1Passw0rd";

            var userData = new List<User>
                               {
                                   new User { Email = "someemail@aaa.pl", Name = "IhaveName", Password = BoardGameDatabase.Operations.HashEncryption.Hash(password1), UserId = 1 },
                                   new User { Email = "someemail2@aaa.pl", Name = "IhaveName2", Password = BoardGameDatabase.Operations.HashEncryption.Hash(password2), UserId = 2 },
                                   new User { Email = "someemail3@aaa.pl", Name = "IhaveName3", Password = BoardGameDatabase.Operations.HashEncryption.Hash(password2), UserId = 3 },
                                   new User { Email = "someemail3@aaa.pl", Name = "IhaveName4", Password = BoardGameDatabase.Operations.HashEncryption.Hash(password1), UserId = 4 }
            }.AsQueryable();

            var mockUserSet = new Mock<DbSet<User>>();
            mockUserSet.SetupData(userData);


            var mockContext = new Mock<IBoardGameDbContext>();
            mockContext.Setup(s => s.Users).Returns(mockUserSet.Object);

            IUserService service = new UserService(mockContext.Object, new UserServiceValidation(new UserValidation(), new PasswordValidation(), mockContext.Object));

            //Act
            var resultOk = service.ChangePassword(1, password1, "superNewPass111!", "superNewPass111!");
            var resultBadNewPassword = service.ChangePassword(2, password2, "superbadpassword", "superbadpassword");
            var resultBadOldPassword = service.ChangePassword(3, "IncorectOldPass", "superNewPass222", "superNewPass222");
            var resultThisSamePassword = service.ChangePassword(4, password1, password1, password1);
            var resultNoUser = service.ChangePassword(100, password1, password2, password2);
            var resultRepeatPasswordWrong = service.ChangePassword(1, password1, password2, "NotRepearPassword");


            //Assert
            Assert.AreEqual(resultOk.Status, BoardGameDatabase.Enums.ServiceRespondStatus.Ok);
            Assert.AreEqual(resultBadNewPassword.Status, BoardGameDatabase.Enums.ServiceRespondStatus.Error);
            Assert.AreEqual(resultBadOldPassword.Status, BoardGameDatabase.Enums.ServiceRespondStatus.Error);
            Assert.AreEqual(resultBadOldPassword.Messages.Any(a => a.Key == ValidationKey.PasswordNotCompare.ToString()), true);
            Assert.AreEqual(resultThisSamePassword.Status, BoardGameDatabase.Enums.ServiceRespondStatus.Error);
            Assert.AreEqual(resultThisSamePassword.Messages.Any(a => a.Key == ValidationKey.PasswordChangeThisSame.ToString()), true);
            Assert.AreEqual(resultNoUser.Status, ServiceRespondStatus.Error);
            Assert.AreEqual(resultNoUser.Messages.Any(a => a.Key == ValidationKey.NullUser.ToString()), true);
            Assert.AreEqual(resultRepeatPasswordWrong.Status, ServiceRespondStatus.Error);
            Assert.AreEqual(resultRepeatPasswordWrong.Messages.Any(a => a.Key == ValidationKey.PasswordRepeatNotThisSame.ToString()), true);

        }
    }
}
