using BoardGames.Extensions;
using BoardGames.Interfaces;
using BoardGamesShared.Enums;
using BoardGamesShared.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BoardGames.Games.Chess.Rules
{
    internal class MoveChessRules: IMoveRule
    {
	    private readonly IBoard board;

	    public PawnRules PawnRules { get; }
	    public BishopRules BishopRules { get; }
        public KingRules KingRules { get; }
        public KnightRules KnightRules { get; }
        public QueenRules QueenRules { get; }
        public RookRules RookRules { get; }

        public MoveChessRules(IBoard board, Func<ILastMove> getLastMove)
	    {
		    this.board = board;

		    PawnRules = new PawnRules(board, getLastMove);
		    BishopRules = new BishopRules(board);
		    KnightRules = new KnightRules(board);
		    QueenRules = new QueenRules(board);
		    RookRules = new RookRules(board);
		    KingRules = new KingRules(board);
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

	    public IEnumerable<IField> WhereEnemyCanMove(PawColors youColor)
	    {
		    var enemyPawsList = board.FieldList.Where(w => w.Pawn != null && w.Pawn.Color != youColor);

		    List<IField> whereEnenymCanMoveList = enemyPawsList
		                                          .Where(w => w.Pawn.Type != PawType.PawnChess)
                                                  .SelectMany(WhereCanMove)
		                                          .Distinct()
		                                          .ToList();

		    var pawList = enemyPawsList.Where(w => w.Pawn.Type == PawType.PawnChess);

			foreach (IField field in pawList)
			{
			    whereEnenymCanMoveList.AddRange(PawnRules.WhereCanBeat(field));
			}

		    return whereEnenymCanMoveList;
	    }

        public void SetStartPositionOnBoard()
	    {
		    BishopRules.SetStartPositionOnBoard();
		    KingRules.SetStartPositionOnBoard();
		    KnightRules.SetStartPositionOnBoard();
		    PawnRules.SetStartPositionOnBoard();
		    QueenRules.SetStartPositionOnBoard();
		    RookRules.SetStartPositionOnBoard();
        }

	    public bool IsColorHaveCheck(PawColors color)
	    {
		    IField kingPosition = board.FieldList.First(f => f.Pawn?.Color == color && f.Pawn?.Type == PawType.KingChess);
		    return WhereEnemyCanMove(color).InThisSamePosition(kingPosition) != null;
	    }
    }
}
