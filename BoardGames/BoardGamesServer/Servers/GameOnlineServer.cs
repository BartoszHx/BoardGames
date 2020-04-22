using AutoMapper;
using BoardGamesGrpc.GameOnlines;
using BoardGamesGrpc.SharedModel;
using BoardGamesOnline.Interfaces.Services;
using BoardGamesOnline.Models;
using BoardGamesShared.Enums;
using Grpc.Core;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Google.Protobuf.Collections;
using Match = BoardGamesGrpc.GameOnlines.Match;
using User = BoardGamesOnline.Models.User;
using BoardGamesServer.Configurations;
using BoardGamesServer.Models;
using System.Linq;

namespace BoardGamesServer
{
    public class GameOnlineServer : GameOnlineService.GameOnlineServiceBase
    {
        private IGameOnlineService service;

        private static readonly HashSet<PlayMatchResponseStream> responseStreamList = new HashSet<PlayMatchResponseStream>();

        public GameOnlineServer()
        {
            this.service = StaticKernel.Get<IGameOnlineService>();
            this.ChoosePlayersToPlayAsync(); // Nie podoba mi się to tutaj 
        }

        private async Task ChoosePlayersToPlayAsync()
        {
            await this.service.ConnectPlayersToMatchAsync();
        }

        public override async Task PlayMatch(IAsyncStreamReader<PlayMatchRequest> requestStream, IServerStreamWriter<GamePlay> responseStream, ServerCallContext context)
        {
            try
            {
                string guidID = context.RequestHeaders.First(f => f.Key == "guidid").Value;

                PlayMatchResponseStream playMatchResponseStream = new PlayMatchResponseStream { GuidID = guidID };
                playMatchResponseStream.ResponseStream = responseStream;
                responseStreamList.Add(playMatchResponseStream);

                while (await requestStream.MoveNext(CancellationToken.None))
                {
                    var gamePlayFromClient = requestStream.Current;

                    foreach (var stream in responseStreamList.Where(w=> w.GuidID == guidID).Select(s=> s.ResponseStream))
                    {
                        await stream.WriteAsync(gamePlayFromClient.GamePlay);
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }
        
        public override async Task<SearchOpponentRespons> SearchOpponent(SearchOpponentRequest request, ServerCallContext context)
        {
            try
            {
                BoardGamesOnline.Models.Match match = await this.service.SearchOpponentAsync(new SearchOpponent { GameType = (GameTypes)request.GameType, UserId = request.UserId });

                if (match == null)
                {
                    return new SearchOpponentRespons { Respons = new ServerResponse { Status = ServiceResponseStatus.Cancel } };
                }

                Match mapMatch = Mapping.Mapper.Map<Match>(match);

                return new SearchOpponentRespons { Match = mapMatch, Respons = new ServerResponse { Status = ServiceResponseStatus.Ok } };
            }
            catch (Exception e)
            {
                MapField<string, string> mf = new MapField<string, string>();
                mf.Add("Exception", e.Message);
                return new SearchOpponentRespons { Respons = new ServerResponse { Status = ServiceResponseStatus.Error, Messages = { mf } } };
            }
        }
        
        public override Task<CancelSearchOpponentRespons> CancelSearchOpponent(CancelSearchOpponentRequest request, ServerCallContext context)
        {
            this.service.CancelSearchOpponentAsync(request.UserId);
            return Task.FromResult(new CancelSearchOpponentRespons { Respons = new ServerResponse { Status = ServiceResponseStatus.Ok }});
        }
    }
}
