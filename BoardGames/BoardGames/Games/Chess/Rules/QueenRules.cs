using BoardGamesShared.Interfaces;
using System.Collections.Generic;

namespace BoardGames.Games.Chess.Rules
{
    internal class QueenRules
    {
	    private IBoard board;
	    public QueenRules(IBoard board)
	    {
		    this.board = board;
	    }

        public IEnumerable<IField> WhereCanMove(IField field)
	    {
		    List<IField> fieldList = new List<IField>();
		    fieldList.AddRange(StandardMoveRules.MoveHorizontalVertical(field, board, board.MaxHeight));
		    fieldList.AddRange(StandardMoveRules.MoveAllCross(field, board, board.MaxHeight));

		    return fieldList;
        }
    }
}
