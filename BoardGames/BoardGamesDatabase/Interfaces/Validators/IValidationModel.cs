using BoardGameDatabase.Validations;

namespace BoardGameDatabase.Interfaces.Validators
{
    internal interface IValidationModel<T>
    {
	    ValidationResult Validate(T obj);
    }
}
