using AutoMapper;
using BoardGamesOnline.Configuration;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace BoardGamesOnlineTest.Mappings
{
    [TestFixture]
    public class DatabaseProfileMappingTest
    {
        //http://docs.automapper.org/en/stable/Getting-started.html
        [Test]
        public void ConfigurationValid()
        {
            var config = new MapperConfiguration(cfg =>
                {
                    cfg.ShouldMapProperty = p => p.GetMethod.IsPublic;
                    cfg.AddProfile<DatabaseProfile>();
                });
            var mapper = config.CreateMapper();

            config.AssertConfigurationIsValid();


        }
    }
}
