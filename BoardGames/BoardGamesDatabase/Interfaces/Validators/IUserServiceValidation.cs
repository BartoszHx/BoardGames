using BoardGameDatabase.Models.Entites;
using BoardGameDatabase.Validations;

namespace BoardGameDatabase.Interfaces.Validators
{
    internal interface IUserServiceValidation
    {
	    ValidationResult ChangePassword(string userPassword, string oldPassword, string newPassword, string repeatNewPassword);

	    ValidationResult Registration(User user, string password, string repeatPassword);

	    ValidationResult Login(User user, string password);
    }
}
