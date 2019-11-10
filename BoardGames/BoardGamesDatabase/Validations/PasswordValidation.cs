using BoardGameDatabase.Enums;
using BoardGameDatabase.Interfaces.Validators;
using System.Linq;
using BoardGameDatabase.Operations;
using BoardGameDatabase.Extensions;

namespace BoardGameDatabase.Validations
{
    internal class PasswordValidation : IPasswordValidation
    {
	    private string _password;
	    private ValidationResult result;

	    public ValidationResult Validate(string obj)
	    {
		    _password = obj;
            result = new ValidationResult();

            IsNull();
		    if (!result.IsSucces)
		    {
			    return result;
		    }

		    IsTooShort(8);
		    IsTooLong(16);
		    IsHaveWhiteSpace();
		    IsDigitSymbol();
		    IsUpperSymbol();
		    IsSpecialSymbol();

		    return result;
	    }

	    public ValidationResult ComparePassword(string password, string hashPassword)
	    {
		    _password = password;
			result = new ValidationResult();

            this.IsNull();
            if (!result.IsSucces)
            {
                return result;
            }

            bool isCorrectPassword = HashEncryption.Verification(_password, hashPassword);
		    if (!isCorrectPassword)
		    {
				result.AddError(ValidationKey.PasswordNotCompare);
		    }

		    return result;
	    }

        #region Single validations

        private void IsNull()
	    {
		    if (string.IsNullOrWhiteSpace(_password))
		    {
			    result.AddError(ValidationKey.NoPassword);
		    }
	    }

	    private void IsTooShort(int lenght)
	    {
		    if (_password.Length < lenght)
		    {
			    result.AddError(ValidationKey.PasswordTooShort);
            }
        }

	    private void IsTooLong(int lenght)
	    {
		    if (_password.Length > lenght)
		    {
			    result.AddError(ValidationKey.PasswordTooLong);
		    }
	    }

	    private void IsHaveWhiteSpace()
	    {
		    if (_password.Any(char.IsWhiteSpace))
		    {
			    result.AddError(ValidationKey.PasswordHaveWhiteSpace);
		    }
	    }

	    private void IsDigitSymbol()
	    {
		    if (!_password.Any(char.IsDigit))
            {
	            result.AddError(ValidationKey.PasswordNoDigit);
		    }
	    }

	    private void IsUpperSymbol()
	    {
		    if (!_password.Any(char.IsUpper))
		    {
			    result.AddError(ValidationKey.PasswordNoUpperSymbol);
		    }
        }

	    private void IsSpecialSymbol()
	    {
		    if (this._password.All(a => char.IsLetterOrDigit(a) || char.IsWhiteSpace(a)))
		    {
			    result.AddError(ValidationKey.PasswordNoSpecialSymbol);
		    }
	    }

        #endregion
    }
}
