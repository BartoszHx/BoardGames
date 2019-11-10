using System;
using System.Collections.Generic;
using System.Text;

namespace BoardGamesClient.Responses
{
    public class ServiceResponse
    {
        public Dictionary<string, string> Messages { get; set; }
        public ServiceResponseStatus Status { get; set; }
    }
}
