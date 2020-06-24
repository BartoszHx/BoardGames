using BoardGamesShared.Interfaces;
using System.Collections.Generic;

namespace BoardGames.Games.Chess.Rules
{
    internal class BishopRules
    {
	    private readonly IBoard board;

	    public BishopRules(IBoard board)
	    {
		    this.board = board;
	    }
	    public IEnumerable<IField> WhereCanMove(IField field)
	    {
		    return StandardMoveRules.MoveAllCross(field, board, board.MaxHeight);
	    }
    }
}
