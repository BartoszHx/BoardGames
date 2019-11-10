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

namespace BoardGamesServer
{
    public class GameOnlineServer : GameOnlineService.GameOnlineServiceBase
    {
        private IGameOnlineService service;

        private static HashSet<IServerStreamWriter<GamePlay>> responseStreamList = new HashSet<IServerStreamWriter<GamePlay>>();

        public GameOnlineServer()
        {
            this.service = StaticKernel.Get<IGameOnlineService>();
            responseStreamList = new HashSet<IServerStreamWriter<GamePlay>>();
            this.ChoosePlayersToPlayAsync(); // Nie podoba mi się to tutaj 
        }

        private async Task ChoosePlayersToPlayAsync()
        {
            await this.service.ConnectPlayersToMatchAsync();
        }

        public override async Task PlayMatch(IAsyncStreamReader<PlayMatchRequest> requestStream, IServerStreamWriter<GamePlay> responseStream, ServerCallContext context)
        {
            responseStreamList.Add(responseStream);

            while (await requestStream.MoveNext(CancellationToken.None))
            {
                var gamePlayFromClient = requestStream.Current;
                
                foreach (var stream in responseStreamList)
                {
                    await stream.WriteAsync(gamePlayFromClient.GamePlay);
                }
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
