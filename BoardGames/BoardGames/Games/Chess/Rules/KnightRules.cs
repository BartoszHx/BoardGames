using BoardGamesShared.Interfaces;
using System.Collections.Generic;

namespace BoardGames.Games.Chess.Rules
{
    internal class KnightRules
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
    }
}
