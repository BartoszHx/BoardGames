using BoardGameDatabase.Extensions;
using BoardGameDatabase.Interfaces;
using BoardGameDatabase.Interfaces.Validators;
using BoardGameDatabase.Models.Entites;
using System.Linq;

using BoardGameDatabase.Enums;

namespace BoardGameDatabase.Validations
{
    internal class UserServiceValidation : IUserServiceValidation
    {
	    private readonly IValidationModel<User> userValdiation;
	    private readonly IPasswordValidation passwordValidation;
        private readonly IBoardGameDbContext context;

        public UserServiceValidation(IValidationModel<User> userValdiation, IPasswordValidation passwordValidation, IBoardGameDbContext context)
        {
            this.userValdiation = userValdiation;
            this.passwordValidation = passwordValidation;
            this.context = context;
        }

	    public ValidationResult ChangePassword(string userPassword, string oldPassword, string newPassword, string repeatNewPassword)
	    {
            bool isNotChangedPassword = oldPassword == newPassword;
            if (isNotChangedPassword)
            {
                ValidationResult valid = new ValidationResult();
                valid.AddError(ValidationKey.PasswordChangeThisSame);
                return valid;
            }

            if (newPassword != repeatNewPassword)
            {
                ValidationResult valid = new ValidationResult();
                valid.AddError(ValidationKey.PasswordRepeatNotThisSame);
                return valid;
            }

            ValidationResult comparePassword = passwordValidation.ComparePassword(oldPassword, userPassword);
		    return !comparePassword.IsSucces ? comparePassword : passwordValidation.Validate(newPassword);
	    }

	    public ValidationResult Registration(User user, string password, string repeatNewPassword)
	    {
		    ValidationResult userValidResult = userValdiation.Validate(user);

            if (password != repeatNewPassword)
            {
                userValidResult.AddError(ValidationKey.PasswordRepeatNotThisSame);
            }

            if (!userValidResult.IsSucces && user == null)
            {
                return userValidResult;
            }

            bool isExistEmail = context.Users.Any(a => a.Email == user.Email.ToLower() && a.UserId != user.UserId);
            if (isExistEmail)
            {
                userValidResult.AddError(ValidationKey.EmailDuplicate);
            }

            return !userValidResult.IsSucces ? userValidResult : passwordValidation.Validate(password);
	    }

	    public ValidationResult Login(User user, string password)
	    {
		    bool canLogin = user != null && passwordValidation.ComparePassword(password, user.Password).IsSucces;
			ValidationResult valid = new ValidationResult();

            if (!canLogin)
		    {
				valid.AddError(ValidationKey.CantLogin);
		    }

		    return valid;
	    }
    }
}
