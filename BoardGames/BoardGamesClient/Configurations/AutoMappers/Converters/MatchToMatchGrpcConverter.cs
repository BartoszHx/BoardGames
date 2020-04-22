using AutoMapper;
using BoardGamesClient.Models;
using System;
using System.Collections.Generic;
using System.Text;
using GameOnlineGrpc = BoardGamesGrpc.GameOnlines;

namespace BoardGamesClient.Configurations.AutoMappers.Converters
{
    internal class MatchToMatchGrpcConverter : ITypeConverter<Match, GameOnlineGrpc.Match>
    {
        public GameOnlineGrpc.Match Convert(Match source, GameOnlineGrpc.Match destination, ResolutionContext context)
        {
            destination = destination ?? new GameOnlineGrpc.Match();
            destination.DateEnd = source.DateEnd.ToString();
            destination.DateStart = source.DateStart.ToString();
            destination.MatchId = source.MatchId;

            foreach (var matchUser in source.MatchUsers)
            {
                destination.MatchUsers.Add(context.Mapper.Map<GameOnlineGrpc.MatchUser>(matchUser));
            }

            return destination;
        }
    }
}
