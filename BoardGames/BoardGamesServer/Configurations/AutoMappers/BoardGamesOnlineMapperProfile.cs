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
            this.CreateMap<BgShared.IPawn, Pawn>();
            this.CreateMap<BgShared.IField, Field>();
            this.CreateMap<BgShared.IBoard, Board>();
            this.CreateMap<BgShared.IPlayer, Player>();

            this.CreateMap<BgShared.IGameData, Game>();

            this.CreateMapTwoWay<BgModel.User, User>();
            this.CreateMapTwoWay<BgModel.MatchUser, MatchUser>();

            this.CreateMap<BgModel.Match, Match>()
                .ForMember(dest => dest.DateEnd, opt => opt.MapFrom(src => src.DateEnd == null ? string.Empty : src.DateEnd.Value.ToShortDateString()))
                .ForMember(dest => dest.DateStart, opt => opt.MapFrom(src => src.DateStart.ToShortDateString()))
                .ForMember(dest => dest.MatchUsers, opt => opt.UseDestinationValue());

            this.CreateMap<IGamePlay, GamePlay>();
        }
    }
}
