using BoardGameDatabase.Enums;
using System.Collections.Generic;

namespace BoardGameDatabase.Services.Response
{
    public class ServiceResponse
    {
        public ServiceRespondStatus Status { get; set; }

        public Dictionary<string, string> Messages { get; set; }

        internal ServiceResponse()
        {
            Status = ServiceRespondStatus.Ok;
            Messages = new Dictionary<string, string>();
        }

        internal ServiceResponse(ServiceRespondStatus status, Dictionary<string, string> messages)
        {
            Status = status;
            Messages = messages;
        }

        internal ServiceResponse(ServiceRespondStatus status, string key, string value)
        {
            Status = status;
            Messages = new Dictionary<string, string> { { key, value } };
        }
    }
}
