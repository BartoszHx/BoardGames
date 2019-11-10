using AutoMapper;
using BoardGamesGrpc.SharedModel;
using BoardGamesGrpc.Users;
using BoardGamesOnline.Interfaces.Services;
using Grpc.Core;
using System.Threading.Tasks;
using BoardGamesOnline.Services;
using BoardGamesOnline.Services.Users;
using BoardGamesServer.Configurations;
using Google.Protobuf.Collections;
using UserService = BoardGamesGrpc.Users.UserService;

namespace BoardGamesServer
{
    public class UserServer : UserService.UserServiceBase
    {
        private readonly IUserService service; //Wrzucić do singletona?

        public UserServer(IUserService userService)
        {
            this.service = userService;
        }

        public override Task<UserResponse> Login(LoginRequest request, ServerCallContext context)
        {
            UserRespond respons = this.service.Login(request.Email, request.Password);

            if (respons.Status == BoardGamesOnline.Enums.ServiceRespondStatus.Error)
            {
                return Task.FromResult(new UserResponse{ Respons = new ServerResponse { Status = ServiceResponseStatus.Error } } );
            }

            User user = Mapping.Mapper.Map<User>(respons.User);

            UserResponse userRespons = new UserResponse{ 
                User = user,
                Respons = new ServerResponse { Status = (ServiceResponseStatus)respons.Status }
            };

            //Jakaś metoda albo Mapper
            foreach (var keyValuePair in respons.Messages)
            {
                userRespons.Respons.Messages.Add(keyValuePair.Key, keyValuePair.Value);
            }
            
            return Task.FromResult(userRespons);
        }

        public override Task<ServiceResponse> Registration(RegistrationRequest request, ServerCallContext context)
        {
            BoardGamesOnline.Models.User user = new BoardGamesOnline.Models.User { Email = request.Email, Name = request.Name };

            ServiceRespond respons = this.service.Registration(user, request.Password, request.RepeatPassword);

            ServiceResponse serviceRespons = new ServiceResponse { Respons = new ServerResponse { Status = (ServiceResponseStatus)respons.Status} };

            foreach (var keyValuePair in respons.Messages)
            {
                serviceRespons.Respons.Messages.Add(keyValuePair.Key, keyValuePair.Value);
            }

            return Task.FromResult(serviceRespons);
        }
    }
}
