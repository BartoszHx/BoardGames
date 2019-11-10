using System;
using System.Collections.Generic;
using System.Text;
using BoardGamesClient.Configurations.AutoMappers;
using BoardGamesClient.Interfaces;
using BoardGamesClient.Models;
using BoardGamesClient.Servers;
using BoardGamesGrpc.Users;
using Grpc.Core;
using UserResponse = BoardGamesClient.Responses.UserResponse;

namespace BoardGamesClient.Servers
{
    internal class UserServer
    {
        private ServerConnector server;

        internal UserServer(ServerConnector serverConnector)
        {
            this.server = serverConnector;
        }

        public Responses.UserResponse Login(string email, string password)
        {
            BoardGamesGrpc.Users.UserResponse response = this.server.UserClient.Login(new LoginRequest { Email = email, Password = password });
            return Mapping.Mapper.Map<UserResponse>(response);
        }

        public Responses.UserResponse Registration(Registration registration)
        {
            BoardGamesGrpc.Users.ServiceResponse response = this.server.UserClient.Registration(Mapping.Mapper.Map<RegistrationRequest>(registration));
            return Mapping.Mapper.Map<UserResponse>(response);
        }
    }
}
