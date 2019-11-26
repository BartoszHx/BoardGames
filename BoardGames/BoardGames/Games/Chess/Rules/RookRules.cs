using BoardGames.Extensions;
using BoardGames.Interfaces;
using BoardGames.Kernels;
using BoardGamesShared.Enums;
using BoardGamesShared.Interfaces;
using Ninject;
using System.Collections.Generic;
using System.Linq;

namespace BoardGames.Games.Chess.Rules
{
    internal class RookRules : IMoveRule
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
	    public void SetStartPositionOnBoard()
	    {
		    IEnumerable<IField> whereSetPawn = board.GetFieldListInEndHeigh().Where(w => w.Width == 1 || w.Width == 8);
		    foreach (IField field in whereSetPawn)
		    {
			    field.Pawn = KernelInstance.Get<IPawn>();
			    field.Pawn.Type = PawType.RockChess;
			    field.Pawn.Color = field.GetPawnStartColor();
            }
	    }
    }
}
