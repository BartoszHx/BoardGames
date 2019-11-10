using BoardGameDatabase.Interfaces;
using BoardGameDatabase.Interfaces.Services;
using BoardGameDatabase.Models.Entites;
using BoardGameDatabase.Services;
using BoardGameDatabase.Validations;
using BoardGameDatabaseTest.Helpers;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

using BoardGameDatabase;
using BoardGameDatabase.Enums;
using BoardGameDatabase.Interfaces.Validators;

namespace BoardGameDatabaseTest.Validations
{
    [TestFixture]
    public class UserServiceValidationTest
    {
        [Test]
        public void ChangePassword()
        {
            string password1 = "SOm3Pa$sw0rd";
            string password2 = "SOm1P$ssw0rd";
            string password3 = "mYPas$0!";
            string password4 = "NoSou1>Pas$";

            var mockContext = new Mock<IBoardGameDbContext>();

            IUserServiceValidation validation = new UserServiceValidation(new UserValidation(), new PasswordValidation(), mockContext.Object);

            //Act
            var resultOK1 = validation.ChangePassword(BoardGameDatabase.Operations.HashEncryption.Hash(password1), password1, "superNewPass111!", "superNewPass111!");
            var resultOK2 = validation.ChangePassword(BoardGameDatabase.Operations.HashEncryption.Hash(password2), password2, "12New*pas!", "12New*pas!");
            var resultBadNewPassword = validation.ChangePassword(BoardGameDatabase.Operations.HashEncryption.Hash(password3), password3, "superbadpassword", "superbadpassword");
            var resultBadOldPassword = validation.ChangePassword(BoardGameDatabase.Operations.HashEncryption.Hash(password1), "badPassword!2", "superNewPass22!2", "superNewPass22!2");
            var resultThisSame = validation.ChangePassword(BoardGameDatabase.Operations.HashEncryption.Hash(password1), password1, password1, password1);
            var resultRepeatPasswordWrong = validation.ChangePassword(BoardGameDatabase.Operations.HashEncryption.Hash(password1), password1, password2, password3);


            //Assert
            Assert.AreEqual(resultOK1.IsSucces, true);
            Assert.AreEqual(resultOK1.ErrorList.Count, 0);

            Assert.AreEqual(resultOK2.IsSucces, true);
            Assert.AreEqual(resultOK2.ErrorList.Count, 0);

            Assert.AreEqual(resultBadNewPassword.IsSucces, false);
            Assert.AreEqual(resultBadNewPassword.ErrorList.Count > 0, true);
            Assert.AreEqual(resultBadNewPassword.ErrorList.Any(a => a.Key == ValidationKey.PasswordNoUpperSymbol.ToString()), true);

            Assert.AreEqual(resultBadOldPassword.IsSucces, false);
            Assert.AreEqual(resultBadOldPassword.ErrorList.Count, 1);
            Assert.AreEqual(resultBadOldPassword.ErrorList.Any(a => a.Key == ValidationKey.PasswordNotCompare.ToString()), true);

            Assert.AreEqual(resultThisSame.IsSucces, false);
            Assert.AreEqual(resultThisSame.ErrorList.Count, 1);
            Assert.AreEqual(resultThisSame.ErrorList.Any(a => a.Key == ValidationKey.PasswordChangeThisSame.ToString()), true);

            Assert.AreEqual(resultRepeatPasswordWrong.IsSucces, false);
            Assert.AreEqual(resultRepeatPasswordWrong.ErrorList.Count, 1);
            Assert.AreEqual(resultRepeatPasswordWrong.ErrorList.Any(a => a.Key == ValidationKey.PasswordRepeatNotThisSame.ToString()), true);
        }

        [Test]
        public void Login()
        {
            //Arrange
            string email = "someemail@email.aaa";
            string password = "Som3Pa$sw0rd";
            string hashPassword = BoardGameDatabase.Operations.HashEncryption.Hash(password);

            var user1 = new User { Email = email, Name = "IhaveName", Password = hashPassword, UserId = 1 };

            var mockUserSet = new Mock<DbSet<User>>();

            var mockContext = new Mock<IBoardGameDbContext>();
            mockContext.Setup(m => m.Users).Returns(mockUserSet.Object);

            IUserServiceValidation validation = new UserServiceValidation(new UserValidation(), new PasswordValidation(), mockContext.Object);

            //Act
            var resultOk = validation.Login(user1, password);
            var resultNoUser = validation.Login(null, password);
            var resultBadPassword = validation.Login(user1, "pustka");
            var resultNoPassword = validation.Login(user1, null);

            //Assert
            Assert.AreEqual(resultOk.IsSucces, true);

            Assert.AreEqual(resultNoUser.IsSucces, false);
            Assert.AreEqual(resultNoUser.ErrorList.Any(a => a.Key == ValidationKey.CantLogin.ToString()), true);
            Assert.AreEqual(resultNoUser.ErrorList.Count, 1);

            Assert.AreEqual(resultBadPassword.IsSucces, false);
            Assert.AreEqual(resultBadPassword.ErrorList.Any(a => a.Key == ValidationKey.CantLogin.ToString()), true);
            Assert.AreEqual(resultBadPassword.ErrorList.Count, 1);

            Assert.AreEqual(resultNoPassword.IsSucces, false);
            Assert.AreEqual(resultNoPassword.ErrorList.Any(a => a.Key == ValidationKey.CantLogin.ToString()), true);
            Assert.AreEqual(resultNoPassword.ErrorList.Count, 1);
        }

        [Test]
        public void Registration()
        {
            //Arrange
            var userData = new List<User>
                               {
                                   new User { Email = "exist@a.aa", Name = "IhaveName1", Password = "12345", UserId = 1 }
                               }.AsQueryable();

            var mockUserSet = new Mock<DbSet<User>>();
            var mockContext = new Mock<IBoardGameDbContext>();
            mockUserSet.SetupData(userData);
            mockContext.Setup(s => s.Users).Returns(mockUserSet.Object);

            IUserServiceValidation validation = new UserServiceValidation(new UserValidation(), new PasswordValidation(), mockContext.Object);

            var user1 = new User { Email = "aaa@a.aa", Name = "Name1" };
            string password1 = "Has1o729!";

            var user2 = new User { Email = "bbb@a.aa", Name = "Name2" };
            string password2 = "AhLo20&..11";

            var user3 = new User { Email = "abcd@", Name = "Name3" };
            string password3 = "Has1o729";

            var user4 = new User { Email = "dddd@a.aa", Name = "Name4" };
            string password4 = "badpass";

            var user5 = new User { Email = "exist@a.aa", Name = "Name5" };
            string password5 = "Dasl1001";

            var user6 = new User { Email = "exist@a.aa"};
            string password6 = "go0Dpass!";

            var user7 = new User { Email = "amck" };
            string password7 = "go0Dpass!2";

            //Act
            var resultOk1 = validation.Registration(user1, password1, password1);
            var resultOk2 = validation.Registration(user2, password2, password2);
            var resultIncorectEmail = validation.Registration(user3, password3, password3);
            var resultBadPassword = validation.Registration(user4, password4, password4);
            var resultNoPassword = validation.Registration(user4, null, null);
            var resultDuplicateEmail = validation.Registration(user5, password5, password5);
            var resultNoUser = validation.Registration(null, password5, password5);
            var resultNullNameDuplicateEmail = validation.Registration(user6, password6, password6);
            var resultBadUser = validation.Registration(user7, password7, password7);
            var resultRepeatPasswordWrong = validation.Registration(user1, password1, password2);



            //Assert
            Assert.AreEqual(resultOk1.IsSucces, true);
            Assert.AreEqual(resultOk1.ErrorList.Count, 0);
            Assert.AreEqual(resultOk2.IsSucces, true);
            Assert.AreEqual(resultOk2.ErrorList.Count, 0);

            Assert.AreEqual(resultIncorectEmail.IsSucces, false);
            Assert.AreEqual(resultIncorectEmail.ErrorList.Count, 1);
            Assert.AreEqual(resultIncorectEmail.ErrorList.Any(a => a.Key == ValidationKey.EmailIncorrect.ToString()), true);

            Assert.AreEqual(resultBadPassword.IsSucces, false);
            Assert.AreEqual(resultBadPassword.ErrorList.Any(a => a.Key == ValidationKey.PasswordTooShort.ToString()), true);
            Assert.AreEqual(resultBadPassword.ErrorList.Any(a => a.Key == ValidationKey.PasswordNoDigit.ToString()), true);
            Assert.AreEqual(resultBadPassword.ErrorList.Any(a => a.Key == ValidationKey.PasswordNoUpperSymbol.ToString()), true);

            Assert.AreEqual(resultNoPassword.IsSucces, false);
            Assert.AreEqual(resultNoPassword.ErrorList.Count, 1);
            Assert.AreEqual(resultNoPassword.ErrorList.Any(a => a.Key == ValidationKey.NoPassword.ToString()), true);

            Assert.AreEqual(resultDuplicateEmail.IsSucces, false);
            Assert.AreEqual(resultDuplicateEmail.ErrorList.Count, 1);
            Assert.AreEqual(resultDuplicateEmail.ErrorList.Any(a => a.Key == ValidationKey.EmailDuplicate.ToString()), true);

            Assert.AreEqual(resultNoUser.IsSucces, false);
            Assert.AreEqual(resultNoUser.ErrorList.Count, 1);
            Assert.AreEqual(resultNoUser.ErrorList.Any(a => a.Key == ValidationKey.IsNull.ToString()), true);

            Assert.AreEqual(resultNullNameDuplicateEmail.IsSucces, false);
            Assert.AreEqual(resultNullNameDuplicateEmail.ErrorList.Count, 2);
            Assert.AreEqual(resultNullNameDuplicateEmail.ErrorList.Any(a => a.Key == ValidationKey.UserEmptyName.ToString()), true);
            Assert.AreEqual(resultNullNameDuplicateEmail.ErrorList.Any(a => a.Key == ValidationKey.EmailDuplicate.ToString()), true);

            Assert.AreEqual(resultBadUser.IsSucces, false);
            Assert.AreEqual(resultBadUser.ErrorList.Count, 2);
            Assert.AreEqual(resultBadUser.ErrorList.Any(a => a.Key == ValidationKey.UserEmptyName.ToString()), true);
            Assert.AreEqual(resultBadUser.ErrorList.Any(a => a.Key == ValidationKey.EmailIncorrect.ToString()), true);

            Assert.AreEqual(resultRepeatPasswordWrong.IsSucces, false);
            Assert.AreEqual(resultRepeatPasswordWrong.ErrorList.Count, 1);
            Assert.AreEqual(resultRepeatPasswordWrong.ErrorList.Any(a => a.Key == ValidationKey.PasswordRepeatNotThisSame.ToString()), true);
        }
    }
}
