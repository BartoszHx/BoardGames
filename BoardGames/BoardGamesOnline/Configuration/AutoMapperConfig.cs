using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace BoardGamesOnline.Configuration
{
    internal static class AutoMapperConfig
    {
        public static IMapper MapperConfiguration()
        {
            var config = new MapperConfiguration(cfg =>
                {
                    cfg.ShouldMapProperty = p => p.GetMethod.IsPublic;
                    cfg.AddProfile<DatabaseProfile>();
                    cfg.AddProfile<GameProfile>();
                });
            var mapper = config.CreateMapper();

            //config.AssertConfigurationIsValid();


            return mapper;
        }

        public static void CreateMapTwoWay<T1, T2>(this Profile profile)
        {
            profile.CreateMap<T1, T2>();
            profile.CreateMap<T2, T1>();
        }
    }
}
