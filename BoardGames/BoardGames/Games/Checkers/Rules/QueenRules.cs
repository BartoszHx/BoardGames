using BoardGamesShared.Enums;
using BoardGamesShared.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace BoardGames.Games.Checkers.Rules
{
    internal class QueenRules
    {
	    private IBoard board { get; }
	    private StandardCheckersMoveRules standardMoveRules;

        public QueenRules(IBoard board)
	    {
		    this.board = board;
		    standardMoveRules = new StandardCheckersMoveRules(board);
	    }

	    public IEnumerable<IField> NormalMove(IField field)
	    {
		    return StandardMoveRules.MoveAllCross(field, board, board.MaxHeight).Where(w => w.Pawn == null);
        }

        public IEnumerable<IField> BeatMove(IField field)
        {
			IEnumerable<IField> enemyModelList = StandardMoveRules.MoveAllCross(field, board, board.MaxHeight).Where(w => w.Pawn != null);
	        return standardMoveRules.BeatMove(field, enemyModelList);
        }

	    public bool CanChangePawnToQueen(IField field)
	    {
		    return (field.Heigh == board.MaxHeight && field.Pawn?.Color == PawColors.White) ||
			         (field.Heigh == board.MinWidth && field.Pawn?.Color == PawColors.Black);
	    }
    }
}
