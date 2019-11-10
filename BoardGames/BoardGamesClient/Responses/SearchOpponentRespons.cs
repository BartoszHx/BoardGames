using System;
using System.Collections.Generic;
using System.Text;
using BoardGamesClient.Models;

namespace BoardGamesClient.Responses
{
    public class SearchOpponentRespons : ServiceResponse
    {
        //public GamePlay GamePlay { get; set; }
        public Match Match{ get; set; }
    }
}
