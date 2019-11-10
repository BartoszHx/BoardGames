using BoardGameDatabase.Enums;
using System.Collections.Generic;

namespace BoardGameDatabase.Services.Response
{
    public class ServiceRespond
    {
        public ServiceRespondStatus Status { get; set; }

        public Dictionary<string, string> Messages { get; set; }

        internal ServiceRespond()
        {
            Status = ServiceRespondStatus.Ok;
            Messages = new Dictionary<string, string>();
        }

        internal ServiceRespond(ServiceRespondStatus status, Dictionary<string, string> messages)
        {
            Status = status;
            Messages = messages;
        }

        internal ServiceRespond(ServiceRespondStatus status, string key, string value)
        {
            Status = status;
            Messages = new Dictionary<string, string> { { key, value } };
        }
    }
}
