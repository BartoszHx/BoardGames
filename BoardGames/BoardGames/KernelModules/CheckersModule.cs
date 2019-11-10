using BoardGames.Interfaces;
using BoardGames.Models;
using BoardGames.Models.Checkers;
using BoardGamesShared.Interfaces;
using Ninject.Modules;

namespace BoardGames.KernelModules
{
    internal class CheckersModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IBoard>().To<BoardModel>();//Pomyśleć
            Bind<IField>().To<FieldModel>();
            Bind<IPawn>().To<PawModel>();
            Bind<ILastMove>().To<LastMoveModel>();
        }
    }
}
