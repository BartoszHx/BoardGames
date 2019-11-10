using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BoardGameDatabase.Enums;
using BoardGameDatabase.Validations;

using NUnit.Framework;


namespace BoardGameDatabaseTest.Validations
{
    [TestFixture]
    public class PasswordValidationTest
    {
        [Test]
        public void CorrectPassword()
        {
            BoardGameDatabase.Interfaces.Validators.IPasswordValidation validPassword = new PasswordValidation();

            string password8symbols = "Bksai1@0";
            string passwordOk = "hasLo876!";
            string password16symbols = "SupeRTa1jn>>Pa$$";

            var result1 = validPassword.Validate(password8symbols);
            var result2 = validPassword.Validate(passwordOk);
            var result3 = validPassword.Validate(password16symbols);

            Assert.AreEqual(result1.IsSucces, true);
            Assert.AreEqual(result2.IsSucces, true);
            Assert.AreEqual(result3.IsSucces, true);

        }

        [Test]
        public void IncorrectPassword()
        {
            //Arrange
            BoardGameDatabase.Interfaces.Validators.IPasswordValidation validPassword = new PasswordValidation();

            string passwordNull = "";
            string passwordTooShort = "Sho1!";
            string passwordTooLong = "123456789O!23456709";
            string passwordWhiteSpace = "Some Pass1!";
            string passwordNoDigital = "bezrZadnejCyfr!";
            string passwordNoUpperSymbol = "samemałeli!1";
            string passwordNoSpecialSymbol = "SomePass1";

            string passwordWhiteSpaceNoSpecial = "Some Pass 1";
            string passwordVeryBad = "s ho";

            //Act
            var resultNull = validPassword.Validate(passwordNull);
            var resultTooShort = validPassword.Validate(passwordTooShort);
            var resultTooLong = validPassword.Validate(passwordTooLong);
            var resultWhiteSpace = validPassword.Validate(passwordWhiteSpace);
            var resultNoDigital = validPassword.Validate(passwordNoDigital);
            var resultNoUpperSymbol = validPassword.Validate(passwordNoUpperSymbol);
            var resultNoSpecialSymbol = validPassword.Validate(passwordNoSpecialSymbol);

            var resultWhiteSpaceNoSpecial = validPassword.Validate(passwordWhiteSpaceNoSpecial);
            var resultVeryBad = validPassword.Validate(passwordVeryBad);


            //Assert
            Assert.AreEqual(resultNull.IsSucces, false);
            Assert.AreEqual(resultNull.ErrorList.Keys.All(a=> a == ValidationKey.NoPassword.ToString()), true);
            Assert.AreEqual(resultNull.ErrorList.Keys.Count, 1);

            Assert.AreEqual(resultTooShort.IsSucces, false);
            Assert.AreEqual(resultTooShort.ErrorList.Keys.All(a => a == ValidationKey.PasswordTooShort.ToString()), true);
            Assert.AreEqual(resultTooShort.ErrorList.Keys.Count, 1);

            Assert.AreEqual(resultTooLong.IsSucces, false);
            Assert.AreEqual(resultTooLong.ErrorList.Keys.All(a => a == ValidationKey.PasswordTooLong.ToString()), true);
            Assert.AreEqual(resultTooLong.ErrorList.Keys.Count, 1);

            Assert.AreEqual(resultWhiteSpace.IsSucces, false);
            Assert.AreEqual(resultWhiteSpace.ErrorList.Keys.All(a => a == ValidationKey.PasswordHaveWhiteSpace.ToString()), true);
            Assert.AreEqual(resultWhiteSpace.ErrorList.Keys.Count, 1);

            Assert.AreEqual(resultNoDigital.IsSucces, false);
            Assert.AreEqual(resultNoDigital.ErrorList.Keys.All(a => a == ValidationKey.PasswordNoDigit.ToString()), true);
            Assert.AreEqual(resultNoDigital.ErrorList.Keys.Count, 1);

            Assert.AreEqual(resultNoUpperSymbol.IsSucces, false);
            Assert.AreEqual(resultNoUpperSymbol.ErrorList.Keys.All(a => a == ValidationKey.PasswordNoUpperSymbol.ToString()), true);
            Assert.AreEqual(resultNoUpperSymbol.ErrorList.Keys.Count, 1);

            Assert.AreEqual(resultNoSpecialSymbol.IsSucces, false);
            Assert.AreEqual(resultNoSpecialSymbol.ErrorList.Keys.All(a => a == ValidationKey.PasswordNoSpecialSymbol.ToString()), true);
            Assert.AreEqual(resultNoSpecialSymbol.ErrorList.Keys.Count, 1);

            Assert.AreEqual(resultNoSpecialSymbol.IsSucces, false);
            Assert.AreEqual(resultNoSpecialSymbol.ErrorList.Keys.All(a => a == ValidationKey.PasswordNoSpecialSymbol.ToString()), true);
            Assert.AreEqual(resultNoSpecialSymbol.ErrorList.Keys.Count, 1);

            Assert.AreEqual(resultWhiteSpaceNoSpecial.IsSucces, false);
            Assert.AreEqual(resultWhiteSpaceNoSpecial.ErrorList.Keys.Any(a => a == ValidationKey.PasswordNoSpecialSymbol.ToString()), true);
            Assert.AreEqual(resultWhiteSpaceNoSpecial.ErrorList.Keys.Any(a => a == ValidationKey.PasswordHaveWhiteSpace.ToString()), true);
            Assert.AreEqual(resultWhiteSpaceNoSpecial.ErrorList.Keys.Count, 2);

            Assert.AreEqual(resultVeryBad.IsSucces, false);
            Assert.AreEqual(resultVeryBad.ErrorList.Keys.Any(a => a == ValidationKey.PasswordTooShort.ToString()), true);
            Assert.AreEqual(resultVeryBad.ErrorList.Keys.Any(a => a == ValidationKey.PasswordHaveWhiteSpace.ToString()), true);
            Assert.AreEqual(resultVeryBad.ErrorList.Keys.Any(a => a == ValidationKey.PasswordNoDigit.ToString()), true);
            Assert.AreEqual(resultVeryBad.ErrorList.Keys.Any(a => a == ValidationKey.PasswordNoSpecialSymbol.ToString()), true);
            Assert.AreEqual(resultVeryBad.ErrorList.Keys.Any(a => a == ValidationKey.PasswordNoUpperSymbol.ToString()), true);
            Assert.AreEqual(resultVeryBad.ErrorList.Keys.Count, 5);

        }

        [Test]
        public void ComparePassword()
        {
            //Arrange
            BoardGameDatabase.Interfaces.Validators.IPasswordValidation validation = new PasswordValidation();

            string password1 = "someG0)dPa$";
            string password2 = "Ano*9Pa@@s";

            //Act
            var resultOk1 = validation.ComparePassword(password1, BoardGameDatabase.Operations.HashEncryption.Hash(password1));
            var resultOk2 = validation.ComparePassword(password2, BoardGameDatabase.Operations.HashEncryption.Hash(password2));
            var resultNotCompare = validation.ComparePassword(password1, BoardGameDatabase.Operations.HashEncryption.Hash(password2));
            var resultNullPassword = validation.ComparePassword(null, BoardGameDatabase.Operations.HashEncryption.Hash(password1));
            var resultWitheSpacePassword = validation.ComparePassword(" ", BoardGameDatabase.Operations.HashEncryption.Hash(password1));


            //Assert
            Assert.AreEqual(resultOk1.IsSucces, true);
            Assert.AreEqual(resultOk1.ErrorList.Count, 0);

            Assert.AreEqual(resultOk2.IsSucces, true);
            Assert.AreEqual(resultOk2.ErrorList.Count, 0);

            Assert.AreEqual(resultNotCompare.IsSucces, false);
            Assert.AreEqual(resultNotCompare.ErrorList.Any(a => a.Key == ValidationKey.PasswordNotCompare.ToString()), true);
            Assert.AreEqual(resultNotCompare.ErrorList.Count, 1);

            Assert.AreEqual(resultNullPassword.IsSucces, false);
            Assert.AreEqual(resultNullPassword.ErrorList.Any(a => a.Key == ValidationKey.NoPassword.ToString()), true);
            Assert.AreEqual(resultNullPassword.ErrorList.Count, 1);

            Assert.AreEqual(resultWitheSpacePassword.IsSucces, false);
            Assert.AreEqual(resultWitheSpacePassword.ErrorList.Any(a => a.Key == ValidationKey.NoPassword.ToString()), true);
            Assert.AreEqual(resultWitheSpacePassword.ErrorList.Count, 1);
        }
    }
}
