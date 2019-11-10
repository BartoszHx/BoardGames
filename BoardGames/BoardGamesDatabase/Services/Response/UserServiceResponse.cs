using BoardGameDatabase.Enums;
using BoardGameDatabase.Models.Entites;
using System.Collections.Generic;

namespace BoardGameDatabase.Services.Response
{
    public class UserServiceResponse : ServiceResponse
    {
        public User User { get; set; }

        internal UserServiceResponse() : base()
        {

        }

        internal UserServiceResponse(ServiceRespondStatus status, Dictionary<string, string> message, User user = null) : base(status, message)
        {
            User = user;
        }

        internal UserServiceResponse(User user) : base()
        {
            User = user;
        }
    }
}
