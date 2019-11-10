using BoardGameDatabase.Interfaces.Validators;
using BoardGameDatabase.Models.Entites;
using System.ComponentModel.DataAnnotations;
using System.Linq;

using BoardGameDatabase.Enums;

using DbContexts;
using BoardGameDatabase.Extensions;

namespace BoardGameDatabase.Validations
{
    internal class UserValidation : IValidationModel<User>
    {
	    private ValidationResult result;
	    private User user;

        public ValidationResult Validate(User obj)
	    {
		    result = new ValidationResult();
		    user = obj;

			IsNull();
		    if (!result.IsSucces)
		    {
			    return result;
		    }

			IsName();
			IsCorrectEmail();

		    return result;
	    }

        #region single validation

        private void IsNull()
	    {
		    if (user == null)
		    {
			    result.AddError(ValidationKey.IsNull);
		    }
        }

	    private void IsName()
	    {
		    if (string.IsNullOrWhiteSpace(user.Name))
		    {
			    result.AddError(ValidationKey.UserEmptyName);
		    }
        }

	    private void IsCorrectEmail()
	    {
		    bool isCorrectEmail = new EmailAddressAttribute().IsValid(user.Email);
		    if (!isCorrectEmail)
		    {
			    result.AddError(ValidationKey.EmailIncorrect);
		    }
        }

        #endregion
    }
}
