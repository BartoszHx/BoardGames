using BoardGamesShared.Enums;
using BoardGamesShared.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace BoardGames.Games.Checkers.Rules
{
    internal class StandardCheckersMoveRules
    {
	    private IBoard board;

        public StandardCheckersMoveRules(IBoard board)
        {
	        this.board = board;
        }

	    public int DirectionMove(IField field)
	    {
		    return field.Pawn.Color == PawColors.Black ? -1 : 1;
	    }

	    public IEnumerable<IField> BeatMove(IField field, IEnumerable<IField> fieldEnemyList)
	    {
		    return fieldEnemyList.Select(fieldEnemy => GetNextCrossField(field, fieldEnemy)).Where(crossField => crossField != null && crossField.Pawn == null);
	    }

	    public IField GetNextCrossField(IField fieldOld, IField fieldNew)
	    {
		    int heigh = fieldNew.Heigh + StandardMoveRules.IsMoveUp(fieldOld, fieldNew);
		    int width = fieldNew.Width + StandardMoveRules.IsMoveRight(fieldOld, fieldNew);

            return board.FieldList.FirstOrDefault(f => f.Heigh == heigh && f.Width == width);
	    }

	    public IField GetPreviousCrossField(IField fieldOld, IField fieldNew)
	    {
		    int heigh = fieldNew.Heigh - StandardMoveRules.IsMoveUp(fieldOld, fieldNew);
		    int width = fieldNew.Width - StandardMoveRules.IsMoveRight(fieldOld, fieldNew);

            return board.FieldList.FirstOrDefault(f => f.Heigh == heigh && f.Width == width);
	    }
    }
}
