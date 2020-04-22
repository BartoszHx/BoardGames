using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace BoardGamesClient.Configurations.AutoMappers
{
    public static class Mapping //zmienić na internal
    {
        private static readonly Lazy<IMapper> Lazy = new Lazy<IMapper>(Configurations.AutoMapperConfig.MapperConfiguration);

        public static IMapper Mapper => Lazy.Value;
    }
}
