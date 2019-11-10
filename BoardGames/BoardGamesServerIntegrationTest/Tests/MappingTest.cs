using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BoardGamesServer.Configurations;
using NUnit.Framework;

namespace BoardGamesServerIntegrationTest.Tests
{
    [TestFixture]
    public class MappingTest
    {
        [Test]
        public void BoardGamesOnlineMapperProfileTest()
        {
            var config = new MapperConfiguration(cfg =>
                {
                    cfg.ShouldMapProperty = p => p.GetMethod.IsPublic;
                    cfg.AddProfile<BoardGamesOnlineMapperProfile>();
                });
            var mapper = config.CreateMapper();

            config.AssertConfigurationIsValid();
        }

        
    }
}
