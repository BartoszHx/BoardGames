using AutoMapper;
using BoardGamesShared.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using GameOnlineGrpc = BoardGamesGrpc.GameOnlines;

namespace BoardGamesClient.Configurations.AutoMappers.Converters
{
    internal class IBoardToBoardGrpcConverter : ITypeConverter<IBoard, GameOnlineGrpc.Board>
    {
        public GameOnlineGrpc.Board Convert(IBoard source, GameOnlineGrpc.Board destination, ResolutionContext context)
        {
            destination = destination ?? new GameOnlineGrpc.Board();
            destination.MaxHeight = source.MaxHeight;
            destination.MaxWidth = source.MaxWidth;
            destination.MinHeight = source.MinHeight;
            destination.MinWidth = source.MinWidth;

            foreach (var field in source.FieldList)
            {
                destination.FieldList.Add(context.Mapper.Map<GameOnlineGrpc.Field>(field));
            }

            return destination;
        }
    }
}
