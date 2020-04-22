using AutoMapper;
using BoardGamesGrpc.Users;
using UserGrpc = BoardGamesGrpc.Users;
using GameOnlineGrpc = BoardGamesGrpc.GameOnlines;
using BoardGamesClient.Models;
using BoardGamesShared.Interfaces;
using UserSharedModelGrpc = BoardGamesGrpc.SharedModel;
using System.Collections.Generic;
using Google.Protobuf.Collections;
using BoardGamesClient.Configurations.AutoMappers.Converters;
using BoardGamesShared.Models;

namespace BoardGamesClient.Configurations
{
    internal class ServerGrpcProfile : Profile
    {
        public ServerGrpcProfile()
        {

            this.CreateMapTwoWay<UserSharedModelGrpc.User, User>();
            this.CreateMap<GameOnlineGrpc.Match, Match>();
            this.CreateMap<Match, GameOnlineGrpc.Match>().ConvertUsing(typeof(MatchToMatchGrpcConverter));
            this.CreateMapTwoWay<GameOnlineGrpc.GamePlay, GamePlay>();
            this.CreateMapTwoWay<GameOnlineGrpc.MatchUser, MatchUser>();

            //Interfacy
            this.CreateMap<GameOnlineGrpc.Pawn, IPawn>().ConstructUsing(parentDto => new Pawn());
            this.CreateMap<GameOnlineGrpc.Field, IField>().ConstructUsing(parentDto => new Field());
            this.CreateMap<GameOnlineGrpc.Board, IBoard>().ConstructUsing(parentDto => new Board());
            this.CreateMap<GameOnlineGrpc.Player, IPlayer>().ConstructUsing(parentDto => new Player());
            this.CreateMap<GameOnlineGrpc.PawnHistory, IPawnHistory>().ConstructUsing(parentDto => new PawnHistory());
            this.CreateMap<GameOnlineGrpc.GameData, IGameData>().ConstructUsing(parentDto => new GameData());


            this.CreateMap<IPawn, GameOnlineGrpc.Pawn>();
            this.CreateMap<IField, GameOnlineGrpc.Field>();
            this.CreateMap<IBoard, GameOnlineGrpc.Board>().ConvertUsing(typeof(IBoardToBoardGrpcConverter));
            this.CreateMap<IPlayer, GameOnlineGrpc.Player>();
            this.CreateMap<IPawnHistory, GameOnlineGrpc.PawnHistory>();
            this.CreateMap<IGameData, GameOnlineGrpc.GameData>().ConvertUsing(typeof(IGameDataToGameGrpcConverter));

            /*
            this.CreateMap(typeof(IEnumerable<>), typeof(RepeatedField<>)).ConvertUsing(typeof(EnumerableToRepeatedFieldTypeConverter<,>));
            this.CreateMap(typeof(RepeatedField<>), typeof(List<>)).ConvertUsing(typeof(RepeatedFieldToListTypeConverter<,>));
            */

            this.CreateMap<UserGrpc.UserResponse, UserResponse>();
            this.CreateMap<UserGrpc.ServiceResponse, ServiceResponse>();
            this.CreateMap<Registration, UserGrpc.RegistrationRequest>();

        }
    }
}
