using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BoardGamesOnline.Enums;
using BoardGamesOnline.Models;
using BoardGamesShared.Enums;

namespace BoardGamesOnline.Interfaces.Services
{
    public interface IGameOnlineService
    {
        List<IGamePlay> PlayedMatches { get; set; }

        Task<Match> SearchOpponentAsync(SearchOpponent searchOpponent); //Co zwrócić? IGamePlay? Zrobić tak aby zwrócić iGamepLay i następną usługą uzyskać streming

        void CancelSearchOpponentAsync(int userId);

        Task ConnectPlayersToMatchAsync(); 
        //void ConnectPlayersToMatch(BoardGamesShared.Enums.GameTypes gameType); //hmmm, ma łączyć przeciwników między sobą

    }
}
