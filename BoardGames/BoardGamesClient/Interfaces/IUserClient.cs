using System;
using System.Collections.Generic;
using System.Text;
using BoardGamesClient.Models;

namespace BoardGamesClient.Interfaces
{
    public interface IUserClient
    {
        User Login(string email, string password);

        void LogOff(User user);

        User Registration(Registration registration);
    }
}
