using BoardGamesOnline.Models;
using BoardGamesOnline.Services;
using BoardGamesOnline.Services.Users;

namespace BoardGamesOnline.Interfaces.Services
{
    public interface IUserService
    {
        //Co ma dać mi logowanie? Zapis do serwisu, nadanie ID sessiii, zwrócenie danych gracza
        UserRespond Login(string email, string password);
        ServiceRespond Registration(User user, string password, string repeatPassword);
        ServiceRespond ChangePassword(int userId, string oldPassword, string newPassword, string repeatPassword);
    }
}
