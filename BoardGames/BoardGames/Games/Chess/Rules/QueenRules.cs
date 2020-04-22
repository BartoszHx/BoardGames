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
    internal class QueenRules : IMoveRule
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
	    public void SetStartPositionOnBoard(ref int idIncrementation)
	    {
		    IEnumerable<IField> whereSetPawn = board.FieldList.Where(w => (w.Width == 5 && w.Heigh == 8) || (w.Width == 4 && w.Heigh == 1));
		    foreach (IField field in whereSetPawn)
		    {
			    field.Pawn = KernelInstance.Get<IPawn>();
			    field.Pawn.Type = PawType.QueenChess;
			    field.Pawn.Color = field.GetPawnStartColor();
                field.Pawn.ID = ++idIncrementation;
            }
        }
    }
}
