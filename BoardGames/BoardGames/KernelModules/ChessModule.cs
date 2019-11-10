using BoardGames.Games.Chess;
using BoardGames.Interfaces;
using BoardGames.Models;
using BoardGamesShared.Interfaces;
using Ninject.Modules;

namespace BoardGames.KernelModules
{
    internal class ChessModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IBoard>().To<BoardModel>();
            Bind<IField>().To<FieldModel>();
            Bind<IPawn>().To<PawModel>();
            Bind<ILastMove>().To<LastMoveModel>();
	        Bind<IRulesChess>().To<RulesChess>();
        }
    }
}