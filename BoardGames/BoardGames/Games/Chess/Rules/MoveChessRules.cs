using BoardGames.Extensions;
using BoardGamesShared.Enums;
using BoardGamesShared.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BoardGames.Games.Chess.Rules
{
    internal class MoveChessRules//: IMoveRule
    {
	    private readonly IBoard board;

	    public PawnRules PawnRules { get; }
	    public BishopRules BishopRules { get; }
        public KingRules KingRules { get; }
        public KnightRules KnightRules { get; }
        public QueenRules QueenRules { get; }
        public RookRules RookRules { get; }

        public MoveChessRules(IBoard board, IList<IPawnHistory> pawnHistoriesList)
	    {
		    this.board = board;

		    PawnRules = new PawnRules(board, pawnHistoriesList);
		    BishopRules = new BishopRules(board);
		    KnightRules = new KnightRules(board);
		    QueenRules = new QueenRules(board);
		    RookRules = new RookRules(board);
		    KingRules = new KingRules(board, pawnHistoriesList, WhereCanBeat);
        }

	    public IEnumerable<IField> WhereCanMove(IField field)
	    {
		    if (field.Pawn == null)
		    {
			    return new List<IField>();
		    }

		    switch (field.Pawn.Type)
		    {
			    case PawType.PawnChess:   return PawnRules.WhereCanMove(field);
			    case PawType.BishopChess: return BishopRules.WhereCanMove(field);
			    case PawType.KingChess:   return KingRules.WhereCanMove(field);
			    case PawType.KnightChess: return KnightRules.WhereCanMove(field);
			    case PawType.QueenChess:  return QueenRules.WhereCanMove(field);
			    case PawType.RockChess:   return RookRules.WhereCanMove(field);
			    default:
				    throw new ArgumentOutOfRangeException();
		    }
        }

        public IEnumerable<IField> WhereCanBeat(IField field)
        {
            if (field.Pawn == null)
            {
                return new List<IField>();
            }

            switch (field.Pawn.Type)
            {
                case PawType.PawnChess: return PawnRules.WhereCanBeat(field);
                case PawType.BishopChess: return BishopRules.WhereCanMove(field);
                case PawType.KingChess: return KingRules.WhereCanBeat(field);
                case PawType.KnightChess: return KnightRules.WhereCanMove(field);
                case PawType.QueenChess: return QueenRules.WhereCanMove(field);
                case PawType.RockChess: return RookRules.WhereCanMove(field);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public IEnumerable<IField> WhereEnemyCanMove(PawColors youColor)
	    {
		    var enemyPawsList = board.FieldList.Where(w => w.Pawn != null && w.Pawn.Color != youColor);

		    List<IField> whereEnenymCanMoveList = enemyPawsList
                                                  .SelectMany(WhereCanBeat)
		                                          .Distinct()
		                                          .ToList();

		    return whereEnenymCanMoveList;
	    }

	    public bool IsColorHaveCheck(PawColors color)
	    {
		    IField kingPosition = board.FieldList.First(f => f.Pawn?.Color == color && f.Pawn?.Type == PawType.KingChess);
            return IsColorHaveCheck(color, kingPosition);
	    }

        //Test
        public bool IsColorHaveCheck(PawColors color, IField kingPosition)
        {
            return WhereEnemyCanMove(color).InThisSamePosition(kingPosition) != null;
        }
        
    }
}
