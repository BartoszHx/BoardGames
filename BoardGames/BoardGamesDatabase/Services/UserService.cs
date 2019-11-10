using BoardGameDatabase.Enums;
using BoardGameDatabase.Interfaces.Services;
using BoardGameDatabase.Interfaces.Validators;
using BoardGameDatabase.Models.Entites;
using BoardGameDatabase.Operations;
using BoardGameDatabase.Services.Response;
using DbContexts;
using System.Linq;

using BoardGameDatabase.Interfaces;
using BoardGameDatabase.Validations;

namespace BoardGameDatabase.Services
{
    internal class UserService : IUserService
    {
	    private readonly IBoardGameDbContext context;
	    private readonly IUserServiceValidation userServiceValidation;

	    public UserService(IBoardGameDbContext context, IUserServiceValidation userServiceValidation)
        {
            this.context = context;
            this.userServiceValidation = userServiceValidation;
        }

	    public UserServiceResponse Login(string email, string password)
	    {
		    User user = GetUserByEmail(email);
		    ValidationResult valid = userServiceValidation.Login(user, password);

		    return valid.IsSucces ? new UserServiceResponse(user) : new UserServiceResponse(ServiceRespondStatus.Error, valid.ErrorList);
	    }

	    public ServiceResponse Registration(User user, string password, string repeatPassword)
	    {
		    user.Email = user.Email.ToLower();

		    ValidationResult valid = userServiceValidation.Registration(user, password, repeatPassword);
		    if (!valid.IsSucces)
		    {
			    return new ServiceResponse(ServiceRespondStatus.Error, valid.ErrorList);
		    }

            user.Password = HashEncryption.Hash(password);

		    context.Users.Add(user);
		    context.SaveChanges();

		    return new ServiceResponse();
	    }

	    public ServiceResponse ChangePassword(int userId, string oldPassword, string newPassword, string repeatNewPassword)
	    {
		    User user = GetUserById(userId);
            if (user == null)
            {
                return new ServiceResponse(ServiceRespondStatus.Error, ValidationKey.NullUser.ToString(), MessageOperation.GetValidationMessage(ValidationKey.NullUser));
            }

		    ValidationResult passwordValidate = userServiceValidation.ChangePassword(user.Password, oldPassword, newPassword, repeatNewPassword);
		    if (!passwordValidate.IsSucces)
		    {
			    return new ServiceResponse(ServiceRespondStatus.Error, passwordValidate.ErrorList);
		    }

            user.Password = HashEncryption.Hash(newPassword);
		    context.SaveChanges();

		    return new ServiceResponse();
	    }

        #region Private method
        private User GetUserById(int userId)
	    {
		    return context.Users.FirstOrDefault(f => f.UserId == userId);
	    }

	    private User GetUserByEmail(string email)
        {
		    return context.Users.FirstOrDefault(f => f.Email == email.ToLower());
	    }
        #endregion
    }
}
