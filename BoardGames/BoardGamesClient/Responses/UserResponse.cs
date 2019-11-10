using System;
using System.Collections.Generic;
using System.Text;
using BoardGamesClient.Models;

namespace BoardGamesClient.Responses
{
    public class UserResponse : ServiceResponse
    {
        public User User { get; set; }
    }
}
