using BoardGameDatabase.Enums;
using BoardGameDatabase.Models.Entites;
using System.Collections.Generic;

namespace BoardGameDatabase.Services.Response
{
    public class UserServiceRespond : ServiceRespond
    {
        public User User { get; set; }

        internal UserServiceRespond() : base()
        {

        }

        internal UserServiceRespond(ServiceRespondStatus status, Dictionary<string, string> message, User user = null) : base(status, message)
        {
            User = user;
        }

        internal UserServiceRespond(User user) : base()
        {
            User = user;
        }
    }
}
