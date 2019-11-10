using BoardGameDatabase.Enums;
using BoardGameDatabase.Models.Entites;
using System.Collections.Generic;

namespace BoardGameDatabase.Services.Response
{

    public class MatchServiceResponse : ServiceResponse
    {
        public Match Match { get; set; }

        public MatchServiceResponse() : base()
        {
            
        }

        public MatchServiceResponse(ServiceRespondStatus status, Dictionary<string, string> message, Match match = null) : base(status, message)
        {
            Match = match;
        }

        public MatchServiceResponse(Match match) : base()
        {
            Match = match;
        }
    }
}
