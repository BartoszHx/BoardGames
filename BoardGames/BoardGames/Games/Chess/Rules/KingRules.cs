using BoardGames.Extensions;
using BoardGames.Interfaces;
using BoardGames.KernelModules;
using BoardGamesShared.Enums;
using BoardGamesShared.Interfaces;
using Ninject;
using System.Collections.Generic;
using System.Linq;

namespace BoardGames.Games.Chess.Rules
{
    internal class KingRules: IMoveRule
    {
	    private readonly List<IPawn> kingsWhoMove;
	    private readonly IBoard board;

        public KingRules(IBoard board)
        {
	        this.board = board;
		    kingsWhoMove = new List<IPawn>();
	    }
	    public IEnumerable<IField> WhereCanMove(IField field)
	    {
		    List<IField> fieldList = NormalMove(field).ToList();
		    fieldList.AddRange(CastlingMove(field));

		    if (fieldList.Count == 0)
			    return fieldList;

		    return fieldList;
	    }

	    public void SetStartPositionOnBoard()
	    {
		    IEnumerable<IField> whereSetPawn = board.FieldList.Where(w => (w.Width == 4 && w.Heigh == 8 ) || (w.Width == 5 && w.Heigh == 1 ));
		    foreach (IField field in whereSetPawn)
		    {
			    field.Pawn = KernelInstance.ChessKernel.Get<IPawn>();
			    field.Pawn.Type = PawType.KingChess;
			    field.Pawn.Color = field.GetPawnStartColor();
            }
	    }

	    private bool IsKingDoMove(IField field)
	    {
		    return kingsWhoMove.Contains(field.Pawn);
	    }

        public void DoCastlingMove(IField oldPosition, IField newPosition)
        {
            bool isCastlingMove = CastlingMove(oldPosition).Contains(newPosition);
            if (!isCastlingMove)
                return;

            bool isRightSideCastling = oldPosition.Width < newPosition.Width;
            
            var fieldOnThisHeightList = board.FieldList.Where(w => w.Heigh == newPosition.Heigh);

            var rockCastlingPosition = fieldOnThisHeightList.First(f => (isRightSideCastling && f.Width == board.MaxWidth)
                                                           || (!isRightSideCastling && f.Width == 1)
                                                         );

            var rockNewPosition = fieldOnThisHeightList.First(f => (isRightSideCastling && f.Width == newPosition.Width - 1)
                                                                  || (!isRightSideCastling && f.Width == newPosition.Width + 1));

	        rockNewPosition.Pawn = rockCastlingPosition.Pawn;
	        rockCastlingPosition.Pawn = null;
        }

        public IEnumerable<IField> NormalMove(IField position)
        {
            List<IField> fieldList = new List<IField>();

            fieldList.AddRange(StandardMoveRules.MoveHorizontalVertical(position, board, 1));
            fieldList.AddRange(StandardMoveRules.MoveAllCross(position, board, 1));

            return fieldList;
        }

        public IEnumerable<IField> CastlingMove(IField position)
        {
            List<IField> fieldList = new List<IField>();


            if (IsKingDoMove(position))
                return fieldList;

            var heighList = board.FieldList.Where(w => w.Heigh == position.Heigh);
            bool canDoCastlingOnRightSide = !heighList.Any(w => w.Width > position.Width && w.Width < board.MaxWidth && w.Pawn != null);
            bool canDoCastlingOnLeftSide = !heighList.Any(w => w.Width < position.Width && w.Width > 1 && w.Pawn != null);

            if (canDoCastlingOnRightSide)
                fieldList.Add(heighList.First(f => f.Width == board.MaxWidth - 1));

            if (canDoCastlingOnLeftSide)
                fieldList.Add(heighList.First(f => f.Width == 2));

            return fieldList;
        }

	    public void AddKingsMove(IField field)
	    {
		    if (!kingsWhoMove.Contains(field?.Pawn))
		    {
				kingsWhoMove.Add(field.Pawn);
		    }
	    }
    }
}
