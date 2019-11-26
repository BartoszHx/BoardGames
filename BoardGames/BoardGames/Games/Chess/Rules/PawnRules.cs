using BoardGames.Extensions;
using BoardGames.Interfaces;
using BoardGames.Kernels;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using BoardGamesShared.Enums;
using BoardGamesShared.Interfaces;

namespace BoardGames.Games.Chess.Rules
{
    internal class PawnRules : IMoveRule
    {
	    private readonly List<IPawn> pawsWhoMove;
	    private readonly IBoard board;
	    private readonly Func<ILastMove> getLastMove;

	    private ILastMove lastMove => getLastMove();

        public PawnRules(IBoard board, Func<ILastMove> getLastMove )
        {
	        this.board = board;
            pawsWhoMove = new List<IPawn>();
	        this.getLastMove = getLastMove;
        }

	    public IEnumerable<IField> WhereCanMove(IField field)
	    {
		    List<IField> fieldList = new List<IField>();
		    fieldList.AddRange(NormalMove(field));
		    fieldList.AddRange(BeatMove(field));
		    fieldList.AddRange(BeatingInPassingMove(field));

		    return fieldList;
	    }

	    public void SetStartPositionOnBoard()
	    {
		    IEnumerable<IField> whereSetPawn = board.FieldList.Where(w => w.Heigh == 7 || w.Heigh == 2);
		    foreach (IField field in whereSetPawn)
		    {
			    field.Pawn = KernelInstance.Get<IPawn>();
			    field.Pawn.Type = PawType.PawnChess;
			    field.Pawn.Color = field.GetPawnStartColor();
            }
	    }

        private bool canMove(IField field) => field != null && field.Pawn == null;
        private int directionMove(PawColors color) => color == PawColors.Black ? -1 : 1;

	    private bool PawDoMove(IField field) => pawsWhoMove.Contains(field.Pawn);

        public IEnumerable<IField> NormalMove(IField field)
        {
            List<IField> fieldList = new List<IField>();

            var firstMove = StandardMoveRules.Move(field, board, 0, 1 * directionMove(field.Pawn.Color));
            bool canDoFirstMove = canMove(firstMove);
            if (canDoFirstMove)
                fieldList.Add(firstMove);

            if (!PawDoMove(field))
            {
                var secendMove = StandardMoveRules.Move(field, board, 0, 2 * directionMove(field.Pawn.Color));
                bool canDoSecendMove = canDoFirstMove && canMove(secendMove);
                if (canDoSecendMove)
                    fieldList.Add(secendMove);
            }

            return fieldList;
        }

        public IEnumerable<IField> WhereCanBeat(IField field)
        {
            List<IField> fieldList = new List<IField>();

	        var moveLeft = StandardMoveRules.Move(field, board, -1, 1 * directionMove(field.Pawn.Color));
			if(moveLeft != null)
				fieldList.Add(moveLeft);

	        var moveRight = StandardMoveRules.Move(field, board, 1, 1 * directionMove(field.Pawn.Color));
			if(moveRight != null)
				fieldList.Add(moveRight);

	        return fieldList;
        }

        public IEnumerable<IField> BeatMove(IField field)
        {
            IEnumerable<IField> fieldList = WhereCanBeat(field);
            fieldList = fieldList.Where(f => f != null && f.Pawn != null && f.Pawn.Color != field.Pawn.Color);
            return fieldList;
        }

        public IEnumerable<IField> BeatingInPassingMove(IField field)
        {
            List<IField> fieldList = new List<IField>();
            if (lastMove == null)
                return fieldList;

            bool isDarkBeatingInPassing = isBeatingInPassing(PawColors.Black,field, 4, 2);
            bool isWhiteBeatingInPassing = isBeatingInPassing(PawColors.White,field, 5, 7);

            if (isDarkBeatingInPassing || isWhiteBeatingInPassing)
            {
                bool isGoLeft = field.Width > lastMove.NewPosition.Width;
                bool isGoUp = directionMove(field.Pawn.Color) > 0;
                fieldList.AddRange(StandardMoveRules.MoveCross(field, board, isGoLeft, isGoUp, 1));
            }

            return fieldList;
        }
        
        private bool isBeatingInPassing(PawColors color, IField field, int heighPosition, int heightOldPosition)
        {

            return field.Pawn.Color == color
                   && field.Heigh == heighPosition
                   && lastMove.Pawn != null
                   && lastMove.Pawn.Color != field.Pawn.Color
                   && lastMove.Pawn.Type == field.Pawn.Type
                   && lastMove.OldPosition.Heigh == heightOldPosition
                   && (lastMove.OldPosition.Width == field.Width - 1
                       || lastMove.OldPosition.Width == field.Width + 1)
                   && lastMove.NewPosition.Heigh == heighPosition
                   && (lastMove.NewPosition.Width == field.Width - 1
                       || lastMove.NewPosition.Width == field.Width + 1);
        }


        public void DoMove(IField oldPosition, IField newPosition)
        {
            bool isBeatingIaPassingMove = BeatingInPassingMove(oldPosition).Any(a=> a == newPosition);
            if(isBeatingIaPassingMove)
                lastMove.NewPosition.Pawn = null;

            //base.DoMove(oldPosition, newPosition);

			if(!pawsWhoMove.Contains(oldPosition.Pawn))
				pawsWhoMove.Add(oldPosition.Pawn);
        }

	    public bool IsPawnUpgrade(IField field)
	    {
		    return field.Pawn.Type == PawType.PawnChess && board.GetFieldListInEndHeigh().Contains(field);
	    }
    }
}
