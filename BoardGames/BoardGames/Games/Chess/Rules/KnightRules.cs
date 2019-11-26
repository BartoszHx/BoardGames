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
    internal class KnightRules : IMoveRule
    {
	    private IBoard board;
	    public KnightRules(IBoard board)
	    {
		    this.board = board;
	    }
        public IEnumerable<IField> WhereCanMove(IField field)
	    {
		    List<IField> retList = new List<IField>();

		    AddToList(field, retList, 2, 1);
		    AddToList(field, retList, 2, -1);
		    AddToList(field, retList, -2, 1);
		    AddToList(field, retList, -2, -1);
		    AddToList(field, retList, 1, 2);
		    AddToList(field, retList, 1, -2);
		    AddToList(field, retList, -1, 2);
		    AddToList(field, retList, -1, -2);

		    return retList;
        }

        private void AddToList(IField position, IList<IField> fieldList, int goWidth, int goHeight)
        {
            var field = StandardMoveRules.Move(position, board, goWidth, goHeight);

            bool isEmpty = field == null;
            bool isMyPawn = !isEmpty && field.Pawn != null && field.Pawn.Color == position.Pawn.Color;

            if (isEmpty || isMyPawn)
                return;

            fieldList.Add(field);
        }

	    public void SetStartPositionOnBoard()
	    {
		    IEnumerable<IField> whereSetPawn = board.GetFieldListInEndHeigh().Where(w => w.Width == 2 || w.Width == 7);
		    foreach (IField field in whereSetPawn)
		    {
			    field.Pawn = KernelInstance.Get<IPawn>();
			    field.Pawn.Type = PawType.KnightChess;
			    field.Pawn.Color = field.GetPawnStartColor();
            }
	    }
    }
}
