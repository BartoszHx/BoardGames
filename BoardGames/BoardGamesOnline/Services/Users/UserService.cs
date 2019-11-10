using BoardGameDatabase.Interfaces;
using BoardGamesOnline.Interfaces.Services;
using BoardGamesOnline.Models;
using BoardGamesOnline.Operations;
using DbModel = BoardGameDatabase.Models.Entites;


namespace BoardGamesOnline.Services.Users
{
    public class UserService : IUserService //internal, komunikacja przez interfacy
    {
        private IBoardGameUnitOfWorkBulider bulider;

        public UserService(IBoardGameUnitOfWorkBulider bulider)
        {
            this.bulider = bulider;
        }

        public UserRespond Login(string email, string password)
        {
            using (IBoardGameUnitOfWork service = bulider.Bulid())
            {
                return Mapping.Mapper.Map<UserRespond>(service.UserService.Login(email, password));
            }
        }

        public ServiceRespond Registration(User user, string password, string repeatPassword)
        {
            using (IBoardGameUnitOfWork service = bulider.Bulid())
            {
                DbModel.User userDb = Mapping.Mapper.Map<DbModel.User>(user);
                return Mapping.Mapper.Map<ServiceRespond>(service.UserService.Registration(userDb, password, repeatPassword));
            }
        }

        public ServiceRespond ChangePassword(int userId, string oldPassword, string newPassword, string repeatNewPassword)
        {
            using (IBoardGameUnitOfWork service = bulider.Bulid())
            {
                return Mapping.Mapper.Map<ServiceRespond>(service.UserService.ChangePassword(userId, oldPassword, newPassword, repeatNewPassword));
            }
        }
    }
}
