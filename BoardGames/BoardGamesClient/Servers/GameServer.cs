using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BoardGamesClient.Configurations.AutoMappers;
using BoardGamesClient.Interfaces;
using BoardGamesClient.Models;
using BoardGamesClient.Responses;
using BoardGamesClient.Servers;
using BoardGamesShared.Enums;
using Grpc.Core;
using GameOnlineGrpc = BoardGamesGrpc.GameOnlines;

namespace BoardGamesClient.Servers
{
    internal class GameServer
    {
        private ServerConnector server;
        private AsyncDuplexStreamingCall<GameOnlineGrpc.PlayMatchRequest, GameOnlineGrpc.GamePlay> _matchPlayCall;

        internal GameServer(ServerConnector serverConnector)
        {
            this.server = serverConnector;
        }

        public async Task<SearchOpponentRespons> SearchOpponentAsync(GameTypes gametype, int userID) //dokładniej request
        {
            GameOnlineGrpc.SearchOpponentRespons match = await this.server.GameClient.SearchOpponentAsync(new GameOnlineGrpc.SearchOpponentRequest { GameType = (int)gametype, UserId = userID });

            Responses.SearchOpponentRespons respons = new SearchOpponentRespons
            {
                Messages = new Dictionary<string, string>(),
                Status = (ServiceResponseStatus)match.Respons.Status,
                Match = Mapping.Mapper.Map<Match>(match.Match)
            };

            foreach (var responsMessage in match.Respons.Messages)
            {
                respons.Messages.Add(responsMessage.Key, responsMessage.Value);
            }

            return respons;
        }

        public void CancelSearchOpponent(int userID)
        {
            this.server.GameClient.CancelSearchOpponent(new BoardGamesGrpc.GameOnlines.CancelSearchOpponentRequest { UserId = userID });
        }

        public async Task PlayMatchConnect(PlayMatch playMatch)
        {
            using (this._matchPlayCall = this.server.GameClient.PlayMatch())
            {
                while (await this._matchPlayCall.ResponseStream.MoveNext(CancellationToken.None))
                {
                    playMatch.GamePlay = Mapping.Mapper.Map<GamePlay>(this._matchPlayCall.ResponseStream.Current);
                }
            }
        }

        public void PlayMatchSend(PlayMatch playMatch)
        {
            BoardGamesGrpc.GameOnlines.GamePlay gamePlay = Mapping.Mapper.Map<BoardGamesGrpc.GameOnlines.GamePlay>(playMatch.GamePlay);
            this._matchPlayCall.RequestStream.WriteAsync(new BoardGamesGrpc.GameOnlines.PlayMatchRequest { GamePlay = gamePlay, UserId = playMatch.UserId });
        }
    }
}
