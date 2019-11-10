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
    internal class RulesChess: IRulesChess
    {
	    private readonly IBoard board;
	    public ILastMove LastMove { get; set; }

	    private readonly MoveChessRules MoveRules;

	    public RulesChess(IBoard board)
	    {
		    this.board = board;
		    LastMove = null;
			MoveRules = new MoveChessRules(board, () => LastMove);
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

	    public IEnumerable<IField> MoveWithoutCheck(IField fieldStart, IEnumerable<IField> fieldList)
	    {
		    IBoard futureBoard = board.Copy();
            MoveChessRules futureMoveChees = new MoveChessRules(futureBoard, () => LastMove);

		    futureBoard.FieldList.InThisSamePosition(fieldStart).Pawn = null;

		    List<IField> toRemoveList = new List<IField>();

		    foreach (IField field in fieldList)
		    {
			    IField fieldBoard = futureBoard.FieldList.InThisSamePosition(field);
			    IPawn fieldBoardPaw = fieldBoard.Pawn;
			    fieldBoard.Pawn = fieldStart.Pawn;

			    bool canBeCheck = futureMoveChees.IsColorHaveCheck(fieldStart.Pawn.Color);
                if (canBeCheck)
			    {
				    toRemoveList.Add(field);
			    }

			    fieldBoard.Pawn = fieldBoardPaw;
		    }

		    return fieldList.Where(w => toRemoveList.InThisSamePosition(w) == null);
	    }

        public bool PawnMove(IField fieldOld, IField fieldNew)
	    {
		    bool canPawnMoveTher = this.PawnWherCanMove(fieldOld).Contains(fieldNew);
		    if (!canPawnMoveTher)
		    {
                return false;
		    }

		    if (fieldOld.Pawn.Type == PawType.KingChess)
		    {
				MoveRules.KingRules.DoCastlingMove(fieldOld,fieldNew);
			    MoveRules.KingRules.AddKingsMove(fieldOld);
		    }

		    if (fieldOld.Pawn.Type == PawType.PawnChess)
		    {
			    MoveRules.PawnRules.DoMove(fieldOld,fieldNew);
		    }

		    fieldNew.Pawn = fieldOld.Pawn;
			fieldOld.Pawn = null;

		    LastMove = new LastMoveModel() //przemyśleć ten model
		    {
			    OldPosition = fieldOld,
			    NewPosition = fieldNew,
			    Pawn = fieldNew.Pawn
		    };


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
