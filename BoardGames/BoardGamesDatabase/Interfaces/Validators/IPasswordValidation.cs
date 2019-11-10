using BoardGameDatabase.Validations;

namespace BoardGameDatabase.Interfaces.Validators
{
    internal interface IPasswordValidation : IValidationModel<string>
    {
	    ValidationResult ComparePassword(string password, string hashPassword);
    }
}
