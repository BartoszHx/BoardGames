using BoardGameDatabase.Enums;
using BoardGameDatabase.Models.Entites;
using System.Collections.Generic;

namespace BoardGameDatabase.Services.Response
{

    public class MatchServiceRespond : ServiceRespond
    {
        public Match Match { get; set; }

        public MatchServiceRespond() : base()
        {
            
        }

        public MatchServiceRespond(ServiceRespondStatus status, Dictionary<string, string> message, Match match = null) : base(status, message)
        {
            Match = match;
        }

        public MatchServiceRespond(Match match) : base()
        {
            Match = match;
        }
    }
}
