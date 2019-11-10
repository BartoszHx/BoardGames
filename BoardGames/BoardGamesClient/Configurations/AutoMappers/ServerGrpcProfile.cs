using AutoMapper;
using BoardGamesGrpc.Users;
using UserGrpc = BoardGamesGrpc.Users;
using GameOnlineGrpc = BoardGamesGrpc.GameOnlines;
using BoardGamesClient.Models;
using BoardGamesShared.Interfaces;
using UserSharedModelGrpc = BoardGamesGrpc.SharedModel;

namespace BoardGamesClient.Configurations
{
    internal class ServerGrpcProfile : Profile
    {
        public ServerGrpcProfile()
        {

            this.CreateMapTwoWay<UserSharedModelGrpc.User, User>();

            this.CreateMapTwoWay<GameOnlineGrpc.Pawn, Pawn>();
            this.CreateMapTwoWay<GameOnlineGrpc.Field, Field>();
            this.CreateMapTwoWay<GameOnlineGrpc.Board, Board>();
            this.CreateMapTwoWay<GameOnlineGrpc.Match, Match>();
            this.CreateMapTwoWay<GameOnlineGrpc.GamePlay, GamePlay>();
            this.CreateMapTwoWay<GameOnlineGrpc.Game, Game>();

            //Interfacy
            this.CreateMap<GameOnlineGrpc.Pawn, IPawn>().ConstructUsing(parentDto => new Pawn());
            this.CreateMap<GameOnlineGrpc.Field, IField>().ConstructUsing(parentDto => new Field());
            this.CreateMap<GameOnlineGrpc.Board, IBoard>().ConstructUsing(parentDto => new Board());
            this.CreateMap<GameOnlineGrpc.Player, IPlayer>().ConstructUsing(parentDto => new Player());
            this.CreateMap<GameOnlineGrpc.Game, IGameData>().ConstructUsing(parentDto => new Game());

            this.CreateMap<IPawn, GameOnlineGrpc.Pawn>();
            this.CreateMap<IField, GameOnlineGrpc.Field>();
            this.CreateMap<IBoard, GameOnlineGrpc.Board>();
            this.CreateMap<IPlayer, GameOnlineGrpc.Player>();
            this.CreateMap<IGameData, GameOnlineGrpc.Game>();




            this.CreateMap<UserGrpc.UserResponse, UserResponse>();
            this.CreateMap<UserGrpc.ServiceResponse, ServiceResponse>();
            this.CreateMap<Registration, UserGrpc.RegistrationRequest>();

            /*

            this.CreateMapTwoWay<BgModel.User, User>();

            this.CreateMap<BgShared.IPawn, Pawn>();
            this.CreateMap<BgShared.IField, Field>();
            this.CreateMap<BgShared.IBoard, Board>();
            this.CreateMap<BgShared.IGameData, Game>();

            this.CreateMap<BgModel.Match, Match>()
                .ForMember(dest => dest.DateEnd, opt => opt.MapFrom(src => src.DateEnd == null ? string.Empty : src.DateEnd.Value.ToShortDateString()))
                .ForMember(dest => dest.DateStart, opt => opt.MapFrom(src => src.DateStart.ToShortDateString()))
                //To niechce się załapać
                //.ForMember(dest => dest.MatchUsers, opt => opt.MapFrom(src => new RepeatedField<User> { Mapper.Map<List<User>>(src.MatchUsers) }))
                .ForMember(dest => dest.MatchUsers, 
                    opt => opt.MapFrom(
                        src => new RepeatedField<User> { src.MatchUsers.Select(s => new User { Email = s.Email, Name = s.Name, UserId = s.UserId }) }));

            this.CreateMap<IGamePlay, GamePlay>();
            */
        }
    }
}
