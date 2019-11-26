using BoardGames.Games;
using BoardGames.Games.Checkers.Rules;
using BoardGames.Kernels;
using BoardGamesShared.Enums;
using BoardGamesShared.Interfaces;
using Ninject;
using System.Collections.Generic;
using System.Linq;

namespace BoardGames.Models.Checkers.Rules
{
    internal class PawnRules
    {
	    private IBoard board;
	    private StandardCheckersMoveRules standardMoveRules;

	    public PawnRules(IBoard board)
	    {
		    this.board = board;
			this.standardMoveRules = new StandardCheckersMoveRules(board);
	    }

	    public IEnumerable<IField> BeatMove(IField field)
	    {
		    IEnumerable<IField> enemyList = StandardMoveRules.MoveAllCross(field,board, 1).Where(w => w.Pawn != null);
		    return standardMoveRules.BeatMove(field, enemyList);
	    }

	    public IEnumerable<IField> NormalMove(IField field)
	    {
		    bool goUp = standardMoveRules.DirectionMove(field) == 1;
		    return StandardMoveRules.MoveCross(field, board, true, goUp, 1)
		                            .Concat(StandardMoveRules.MoveCross(field, board, false, goUp, 1))
									.Where(w => w.Pawn == null);
	    }

        public void SetStartPositionOnBoard()
        {
	        IEnumerable<IField> pawnToSet = board.FieldList.Where(field =>
		        (field.Heigh != 4 && field.Heigh != 5) &&
		        ((field.Heigh % 2 == 1 && field.Width % 2 == 1) ||
		         (field.Heigh % 2 == 0 && field.Width % 2 == 0))
	        );

	        foreach (IField field in pawnToSet)
	        {
		        field.Pawn = KernelInstance.Get<IPawn>();
		        field.Pawn.Type = PawType.PawnCheckers;
		        //field.Pawn.Type = PawType.QueenCheckers;
                field.Pawn.Color = (field.Heigh == 8 || field.Heigh == 7 || field.Heigh == 6) ? PawColors.Black : PawColors.White;
            }
        }
    }
}
