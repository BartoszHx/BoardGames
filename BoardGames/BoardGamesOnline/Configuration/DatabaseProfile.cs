using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using BoardGamesOnline.Models;
using BoardGamesOnline.Services;
using BoardGamesOnline.Services.Users;
using DbModel = BoardGameDatabase.Models.Entites;
using DbEnum = BoardGameDatabase.Enums;
using DbResponse = BoardGameDatabase.Services.Response;

namespace BoardGamesOnline.Configuration
{
    internal class DatabaseProfile : Profile
    {
        public DatabaseProfile()
        {
            //Enums
            this.CreateMapTwoWay<Enums.ServiceRespondStatus, DbEnum.ServiceRespondStatus>();
            
            //this.CreateMap<DbModel.MatchResult, Enums.MatchResults>().ConstructUsing(s => (Enums.MatchResults)s.MatchResultId);
            this.CreateMap<DbModel.GameType, BoardGamesShared.Enums.GameTypes>().ConvertUsing(s => (BoardGamesShared.Enums.GameTypes)s.GameTypeId);

            //Models

            this.CreateMap<Models.User, DbModel.User>()
                .ForMember(dest => dest.MatchUsers, opts => opts.Ignore());
            this.CreateMap<DbModel.User, Models.User>();

            this.CreateMap<MatchUser, DbModel.MatchUser>()
                .ForMember(dest => dest.MatchResult, opts => opts.Ignore())
                .ForMember(dest => dest.Match, opts => opts.Ignore())
                .ForMember(dest => dest.User, opts => opts.Ignore())
                .ForMember(dest => dest.UserId, opts => opts.MapFrom(src => src.User.UserId))
                .ForMember(dest => dest.MatchResultId, opts => opts.MapFrom(src => (int)src.MatchResult));
            this.CreateMap<DbModel.MatchUser, MatchUser>()
                .ForMember(dest => dest.MatchResult, opts => opts.MapFrom(src => (Enums.MatchResults)src.MatchResultId));
            
            CreateMap<Match, DbModel.Match>()
                .ForMember(dest => dest.GameType, opts => opts.Ignore())
                //.ForMember(dest => dest.MatchUsers, opts => opts.Ignore())
                .ForMember(dest => dest.GameTypeId, opts => opts.MapFrom(src => (int)src.GameType));
            
            CreateMap<DbModel.Match, Match>()
                .ForMember(dest => dest.GameType, opts => opts.MapFrom(src => (BoardGamesShared.Enums.GameTypes)src.GameTypeId));

            //Respons 

            this.CreateMapTwoWay<UserRespond, DbResponse.UserServiceResponse>();
            this.CreateMapTwoWay<ServiceRespond, DbResponse.ServiceResponse>();
            
        }
    }
}
