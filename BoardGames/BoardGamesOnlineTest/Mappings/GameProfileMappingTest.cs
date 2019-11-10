using AutoMapper;
using BoardGamesOnline.Configuration;
using NUnit.Framework;

namespace BoardGamesOnlineTest.Mappings
{
    [TestFixture]
    public class GameProfileMappingTest
    {
        [Test]
        public void ConfigurationValid()
        {
            var config = new MapperConfiguration(cfg =>
                {
                    cfg.ShouldMapProperty = p => p.GetMethod.IsPublic;
                    cfg.AddProfile<GameProfile>();
                });
            var mapper = config.CreateMapper();

            config.AssertConfigurationIsValid();


        }
    }
}
