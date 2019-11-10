using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BoardGamesClient.Buliders;
using BoardGamesClient.Interfaces;
using BoardGamesClient.Models;
using BoardGamesClient.Responses;
using BoardGamesClient.Servers;

namespace BoardGamesClient.Clients
{
    internal class UserClient : IUserClient
    {
        private readonly UserServer userServer;

        private Action<Dictionary<string, string>> message; 

        public UserClient(UserClientBulider bulider)
        {
            this.userServer = new UserServer(bulider.ServerConnector);
            this.message = bulider.Message;
        }

        public User Login(string email, string password)
        {
            var respons = this.userServer.Login(email, password);
            if (respons.Status != ServiceResponseStatus.Ok)
            {
                this.message(respons.Messages);
                return null;
            }

            return respons.User;
        }

        public void LogOff(User user)
        {
            //Co tutaj?
        }

        public User Registration(Registration registration)
        {
            var respons = this.userServer.Registration(registration);
            if (respons.Status != ServiceResponseStatus.Ok)
            {
                this.message(respons.Messages);
                return null;
            }

            return respons.User;
        }
    }
}
