using BoardGameDatabase.Interfaces;
using BoardGamesOnline.Interfaces.Services;
using BoardGamesOnline.Models;
using BoardGamesOnline.Operations;
using DbModel = BoardGameDatabase.Models.Entites;


namespace BoardGamesOnline.Services.Users
{
    public class UserService : IUserService //internal, komunikacja przez interfacy
    {
        private IBoardGameServiceBulider serviceBulider;

        public UserService(IBoardGameServiceBulider bulider)
        {
            this.serviceBulider = bulider;
        }

        public UserRespond Login(string email, string password)
        {
            using (IBoardGameServices service = serviceBulider.Bulid())
            {
                return Mapping.Mapper.Map<UserRespond>(service.UserService.Login(email, password));
            }
        }

        public ServiceRespond Registration(User user, string password, string repeatPassword)
        {
            using (IBoardGameServices service = serviceBulider.Bulid())
            {
                DbModel.User userDb = Mapping.Mapper.Map<DbModel.User>(user);
                return Mapping.Mapper.Map<ServiceRespond>(service.UserService.Registration(userDb, password, repeatPassword));
            }
        }

        public ServiceRespond ChangePassword(int userId, string oldPassword, string newPassword, string repeatNewPassword)
        {
            using (IBoardGameServices service = serviceBulider.Bulid())
            {
                return Mapping.Mapper.Map<ServiceRespond>(service.UserService.ChangePassword(userId, oldPassword, newPassword, repeatNewPassword));
            }
        }
    }
}
