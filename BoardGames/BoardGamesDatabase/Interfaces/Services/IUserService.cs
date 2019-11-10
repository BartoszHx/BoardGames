using BoardGameDatabase.Models.Entites;
using BoardGameDatabase.Services.Response;
using System;

namespace BoardGameDatabase.Interfaces.Services
{
    public interface IUserService
	{
		UserServiceResponse Login(string email, string password);
		ServiceResponse Registration(User user, string password, string repeatPassword);
        ServiceResponse ChangePassword(int userId, string oldPassword, string newPassword, string repeatPassword);
	}
}
