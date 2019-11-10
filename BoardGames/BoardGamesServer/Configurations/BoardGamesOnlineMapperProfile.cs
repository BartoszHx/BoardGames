using AutoMapper;
using BoardGamesGrpc.GameOnlines;
using BoardGamesGrpc.SharedModel;
using BoardGamesOnline.Interfaces;
using Google.Protobuf.Collections;
using System.Linq;
using BgModel = BoardGamesOnline.Models;
using BgShared = BoardGamesShared.Interfaces;

namespace BoardGamesServer.Configurations
{
    internal class BoardGamesOnlineMapperProfile : Profile
    {
        public BoardGamesOnlineMapperProfile()
        {
            this.CreateMapTwoWay<BgModel.User, User>();

            this.CreateMap<BgShared.IPawn, Pawn>();
            this.CreateMap<BgShared.IField, Field>();
            this.CreateMap<BgShared.IBoard, Board>();
            this.CreateMap<BgShared.IGameData, Game>();

            this.CreateMap<BgModel.MatchString, Match>()
                .ForMember(dest => dest.DateEnd, opt => opt.MapFrom(src => src.DateEnd == null ? string.Empty : src.DateEnd.Value.ToShortDateString()))
                .ForMember(dest => dest.DateStart, opt => opt.MapFrom(src => src.DateStart.ToShortDateString()))
                //To niechce się załapać
                //.ForMember(dest => dest.MatchUsers, opt => opt.MapFrom(src => new RepeatedField<User> { Mapper.Map<List<User>>(src.MatchUsers) }))
                .ForMember(dest => dest.MatchUsers, 
                    opt => opt.MapFrom(
                        src => new RepeatedField<User> { src.MatchUsers.Select(s => new User { Email = s.Email, Name = s.Name, UserId = s.UserId }) }));

            this.CreateMap<IGamePlay, GamePlay>();
        }
    }
}
