using BoardGamesShared.Enums;
using BoardGamesShared.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardGames.Interfaces
{
    public interface IChessGameBulider
    {
        Action<MessageContents> Alert { get; }
        Func<IEnumerable<PawChess>, PawChess> ChosePawUpgrade { get; }
        IBoard Board { get; }
        IPlayer PlayerTurn { get; }
        IList<IPlayer> PlayerList { get;}
        IList<IPawnHistory> PawnHistoriesList { get;}
        int Turn { get;}
        List<PawChess> PawToChoseList { get; }
    }
}

