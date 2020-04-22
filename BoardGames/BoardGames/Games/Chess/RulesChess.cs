using BoardGames.Extensions;
using BoardGames.Games.Chess.Rules;
using BoardGames.Interfaces;
using BoardGames.Models;
using BoardGamesShared.Enums;
using BoardGamesShared.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace BoardGames.Games.Chess
{
    internal class RulesChess : IRulesChess
    {
	    private readonly IBoard board;

	    private readonly MoveChessRules MoveRules;
        private readonly IList<IPawnHistory> pawnHistoriesList;


        public RulesChess(IBoard board, IList<IPawnHistory> pawnHistoriesList)
	    {
		    this.board = board;
            this.pawnHistoriesList = pawnHistoriesList;
            MoveRules = new MoveChessRules(board, this.pawnHistoriesList);
            
        }

        public IEnumerable<IField> PawnWherCanMove(IField field)
        {
	        if (field.Pawn == null)
	        {
				return new List<IField>();
	        }

	        IEnumerable<IField> wherCanMoveList = MoveRules.WhereCanMove(field);
	        wherCanMoveList = MoveWithoutCheck(field, wherCanMoveList);
	        return wherCanMoveList;
        }

        public IEnumerable<IField> PawnWherCanBeat(IField field)
        {
            if (field.Pawn == null)
            {
                return new List<IField>();
            }

            IEnumerable<IField> wherCanMoveList = MoveRules.WhereCanBeat(field);
            wherCanMoveList = MoveWithoutCheck(field, wherCanMoveList);
            return wherCanMoveList;
        }

	    public IEnumerable<IField> MoveWithoutCheck(IField fieldStart, IEnumerable<IField> fieldList) // Na nowo to ogarnąć
	    {
            //Próba 1. Działanie na orginale
            List<IField> toRemoveList = new List<IField>();
            IPawn pawnStart = fieldStart.Pawn;
            fieldStart.Pawn = null;


            foreach (IField field in fieldList)
            {
                IField fieldBoard = board.FieldList.InThisSamePosition(field);
                IPawn fieldBoardPaw = fieldBoard.Pawn;
                fieldBoard.Pawn = pawnStart;

                bool canBeCheck = MoveRules.IsColorHaveCheck(pawnStart.Color);

                if (canBeCheck)
                {
                    toRemoveList.Add(field);
                }

                fieldBoard.Pawn = fieldBoardPaw;
            }

            fieldStart.Pawn = pawnStart;

            return fieldList.Where(w => toRemoveList.InThisSamePosition(w) == null);
        }

        public bool IsColorHaveCheck(PawColors color)
        {
            IField kingPosition = board.FieldList.First(f => f.Pawn?.Color == color && f.Pawn?.Type == PawType.KingChess);
            return MoveRules.WhereEnemyCanMove(color).InThisSamePosition(kingPosition) != null;
        }

        public bool PawnMove(IField fieldOld, IField fieldNew)
	    {
		    bool canPawnMoveTher = this.PawnWherCanMove(fieldOld).Contains(fieldNew);
		    if (!canPawnMoveTher)
		    {
                return false;
		    }
            //Tu zrobić refaktor, nie ma być IF
		    if (fieldOld.Pawn.Type == PawType.KingChess)
		    {
				MoveRules.KingRules.DoCastlingMove(fieldOld,fieldNew);
                MoveRules.KingRules.DoMove(fieldOld);
		    }

		    if (fieldOld.Pawn.Type == PawType.PawnChess)
		    {
			    MoveRules.PawnRules.DoMove(fieldOld,fieldNew);
		    }

		    fieldNew.Pawn = fieldOld.Pawn;
			fieldOld.Pawn = null;

            return true;//Czy potrzebne jest to?
	    }

	    public void SetStartPositionPaws()
	    {
			MoveRules.SetStartPositionOnBoard();
        }

	    public bool IsPawnUpgrade(IField field)
	    {
		    return MoveRules.PawnRules.IsPawnUpgrade(field);
	    }

        //Jest szach
		//Podejście - Sprawdzamy, cz po ruchu danego pionka, jest szach
        public PawColors? IsCheckOnColor(IEnumerable<PawColors> colorList)
        {
	        foreach (PawColors color in colorList)
	        {
		        if (MoveRules.IsColorHaveCheck(color))
		        {
			        return color;
		        }
	        }

	        return null;
        }

		//Jest szachmat
	    public PawColors? IsCheckmateOnColor(IEnumerable<PawColors> colorList)
	    {
            foreach (PawColors color in colorList)
		    {
			    if (MoveRules.IsColorHaveCheck(color))
			    {
					List<IField> whereCanMoveList = new List<IField>();

				    foreach (IField field in board.FieldList.Where(w=> w.Pawn?.Color == color))
				    {
					    whereCanMoveList.AddRange(PawnWherCanMove(field));
				    }

				    if (whereCanMoveList.Count == 0)
				    {
					    return color;
				    }

			    }
		    }

		    return null;
	    }
    }
}
