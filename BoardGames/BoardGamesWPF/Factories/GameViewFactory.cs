using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardGamesWPF.Factories
{
    public static class GameViewFactory
    {
        public static ViewModels.GameViewModel Create(BoardGames.Enums.GameType game)
        {
            switch (game)
            {
                case BoardGames.Enums.GameType.Chess: return new ViewModels.Chess.GameChessViewModel();
                case BoardGames.Enums.GameType.Checkers: return new ViewModels.GameViewModel(BoardGames.Enums.GameType.Checkers);
                default: throw new Exception("Empty enum for method GameViewFactory");
            }
        }
    }
}
