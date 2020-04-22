using BoardGames.Games.Chess;
using BoardGames.Interfaces;
using BoardGames.Models;
using BoardGamesShared.Interfaces;
using BoardGamesShared.Models;
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
            Bind<IBoard>().To<Board>();
            Bind<IField>().To<Field>();
            Bind<IPawn>().To<Pawn>();
            Bind<IPawnHistory>().To<PawnHistory>();
            Bind<IRulesChess>().To<RulesChess>();
        }
    }
}
