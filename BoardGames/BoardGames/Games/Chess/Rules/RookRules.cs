using BoardGamesShared.Interfaces;
using System.Collections.Generic;

namespace BoardGames.Games.Chess.Rules
{
    internal class RookRules
    {
	    private IBoard board;

	    public RookRules(IBoard board)
	    {
		    this.board = board;
	    }

	    public IEnumerable<IField> WhereCanMove(IField field)
	    {
		    return StandardMoveRules.MoveHorizontalVertical(field, board, board.MaxHeight);
        }
    }
}
