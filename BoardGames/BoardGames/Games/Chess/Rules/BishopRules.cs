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
    internal class BishopRules : IMoveRule
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
	    public void SetStartPositionOnBoard()
	    {
		    IEnumerable<IField> whereSetPawn = board.GetFieldListInEndHeigh().Where(w => w.Width == 3 || w.Width == 6);
		    foreach (IField field in whereSetPawn)
		    {
			    field.Pawn = KernelInstance.Get<IPawn>();
			    field.Pawn.Type = PawType.BishopChess;
                field.Pawn.Color = field.GetPawnStartColor();
		    }
	    }
    }
}
