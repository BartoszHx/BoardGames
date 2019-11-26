using BoardGames.Games.Chess;
using BoardGames.Interfaces;
using BoardGames.Models;
using BoardGamesShared.Interfaces;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardGames.Kernels
{
    class BoardGameModule : NinjectModule
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
