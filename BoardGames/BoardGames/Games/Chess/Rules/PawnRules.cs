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
        private readonly IBoard board;
        private IList<IPawnHistory> pawnHistoriesList;

        public PawnRules(IBoard board, IList<IPawnHistory> pawnHistoriesList)
        {
	        this.board = board;
            this.pawnHistoriesList = pawnHistoriesList;
        }

	    public IEnumerable<IField> WhereCanMove(IField field)
	    {
		    List<IField> fieldList = new List<IField>();
		    fieldList.AddRange(NormalMove(field));
            fieldList.AddRange(WhereCanBeat(field));

            return fieldList;
	    }

        public IEnumerable<IField> WhereCanBeat(IField field)
        {
            List<IField> fieldList = new List<IField>();
            fieldList.AddRange(BeatMove(field));
            fieldList.AddRange(BeatingInPassingMove(field));

            return fieldList;
        }

	    public void SetStartPositionOnBoard(ref int idIncrementation)
	    {
		    IEnumerable<IField> whereSetPawn = board.FieldList.Where(w => w.Heigh == 7 || w.Heigh == 2);
		    foreach (IField field in whereSetPawn)
		    {
			    field.Pawn = KernelInstance.Get<IPawn>();
			    field.Pawn.Type = PawType.PawnChess;
			    field.Pawn.Color = field.GetPawnStartColor();
                field.Pawn.ID = ++idIncrementation;
            }
	    }

        private bool canMove(IField field) => field != null && field.Pawn == null;
        private int directionMove(PawColors color) => color == PawColors.Black ? -1 : 1;

        private bool PawDoMove(IPawn pawn) => pawnHistoriesList.Any(a => a.PawID == pawn.ID);

        public IEnumerable<IField> NormalMove(IField field)
        {
            List<IField> fieldList = new List<IField>();

            var firstMove = StandardMoveRules.Move(field, board, 0, 1 * directionMove(field.Pawn.Color));
            bool canDoFirstMove = canMove(firstMove);
            if (canDoFirstMove)
                fieldList.Add(firstMove);

            if (!PawDoMove(field.Pawn))
            {
                var secendMove = StandardMoveRules.Move(field, board, 0, 2 * directionMove(field.Pawn.Color));
                bool canDoSecendMove = canDoFirstMove && canMove(secendMove);
                if (canDoSecendMove)
                    fieldList.Add(secendMove);
            }

            return fieldList;
        }

        public IEnumerable<IField> BeatMove(IField field)
        {
            List<IField> fieldList = new List<IField>();

            var moveLeft = StandardMoveRules.Move(field, board, -1, 1 * directionMove(field.Pawn.Color));
            if (moveLeft != null)
                fieldList.Add(moveLeft);

            var moveRight = StandardMoveRules.Move(field, board, 1, 1 * directionMove(field.Pawn.Color));
            if (moveRight != null)
                fieldList.Add(moveRight);

            return fieldList.Where(f => f != null && f.Pawn != null && f.Pawn.Color != field.Pawn.Color);
        }

        public IEnumerable<IField> BeatingInPassingMove(IField field)
        {
            List<IField> fieldList = new List<IField>();
            if (pawnHistoriesList.Count == 0)
                return fieldList;

            bool isDarkBeatingInPassing = isBeatingInPassing(PawColors.Black,field, 4, 2);
            bool isWhiteBeatingInPassing = isBeatingInPassing(PawColors.White,field, 5, 7);

            if (isDarkBeatingInPassing || isWhiteBeatingInPassing)
            {
                var newLast = board.FieldList.First(f => f.ID == pawnHistoriesList.Last().CurrentFiledID);
                bool isGoLeft = field.Width > newLast.Width;
                bool isGoUp = directionMove(field.Pawn.Color) > 0;
                fieldList.AddRange(StandardMoveRules.MoveCross(field, board, isGoLeft, isGoUp, 1));
            }

            return fieldList;
        }
        
        private bool isBeatingInPassing(PawColors color, IField field, int heighPosition, int heightOldPosition)
        {
            IPawnHistory lastMove = pawnHistoriesList.LastOrDefault();
            if(lastMove == null)
            {
                return false;
            }

            var pawn = board.FieldList.First(f => f.Pawn?.ID == lastMove.PawID).Pawn;
            var fieldOldPosition = board.FieldList.First(f => f.ID == lastMove.PreviusFiledID);
            var fieldCurrentPosition = board.FieldList.First(f => f.ID == lastMove.CurrentFiledID);

            return field.Pawn.Color == color
               && field.Heigh == heighPosition
               && pawn != null
               && pawn.Color != field.Pawn.Color
               && pawn.Type == field.Pawn.Type
               && fieldOldPosition.Heigh == heightOldPosition
               && (fieldOldPosition.Width == field.Width - 1
                   || fieldOldPosition.Width == field.Width + 1)
               && fieldCurrentPosition.Heigh == heighPosition
               && (fieldCurrentPosition.Width == field.Width - 1
                   || fieldCurrentPosition.Width == field.Width + 1);
        }


        public void DoMove(IField oldPosition, IField newPosition)
        {
            bool isBeatingIaPassingMove = BeatingInPassingMove(oldPosition).Any(a=> a == newPosition);
            if (isBeatingIaPassingMove)
                board.FieldList.First(f => f.ID == pawnHistoriesList.Last().CurrentFiledID).Pawn = null; //Stworzyć metodę dla last move?
        }

	    public bool IsPawnUpgrade(IField field)
	    {
		    return field.Pawn.Type == PawType.PawnChess && board.GetFieldListInEndHeigh().Contains(field);
	    }
    }
}
