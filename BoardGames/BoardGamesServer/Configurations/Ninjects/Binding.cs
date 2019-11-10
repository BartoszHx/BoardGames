using BoardGameDatabase.Buliders;
using BoardGameDatabase.Interfaces;
using BoardGamesOnline.Interfaces.Services;
using Ninject.Modules;

namespace BoardGamesServer.Configurations
{
    public class Binding : NinjectModule
    {
        public override void Load()
        {
            this.Bind<IBoardGameUnitOfWorkBulider>().To<BoardGameUnitOfWorkBulider>();
            this.Bind<IGameOnlineService>().To<BoardGamesOnline.Services.GameOnline.GameOnlineService>();
            this.Bind<IUserService>().To<BoardGamesOnline.Services.Users.UserService>();
        }
    }
}
