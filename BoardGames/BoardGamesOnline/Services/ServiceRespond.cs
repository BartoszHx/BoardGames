using BoardGamesOnline.Enums;
using System.Collections.Generic;

namespace BoardGamesOnline.Services
{
    public class ServiceRespond
    {
        public ServiceRespondStatus Status { get; set; }

        public Dictionary<string, string> Messages { get; set; }
    }
}
