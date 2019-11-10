using BoardGamesShared.Enums;
using System;
using System.Collections.Generic;

namespace BoardGamesShared.Interfaces
{
    public interface IChessGame : IGame
    {
	    Func<IEnumerable<PawChess>, PawChess> ChosePawUpgrade { get; set; }
    }
}
