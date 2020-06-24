using BoardGamesShared.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BoardGames.Games.Chess.Rules
{
    internal class KingRules
    {
	    private readonly IBoard board;
        private IList<IPawnHistory> pawnHistoriesList;
        private CastlingRules castlingRules;

        public KingRules(IBoard board, IList<IPawnHistory> pawnHistoriesList, Func<IField,IEnumerable<IField>> whereCanBeat)
        {
	        this.board = board;
            this.pawnHistoriesList = pawnHistoriesList;

            this.castlingRules = new CastlingRules(this.board, this.pawnHistoriesList, whereCanBeat);
        }

	    public IEnumerable<IField> WhereCanMove(IField field)
	    {
		    List<IField> fieldList = NormalMove(field).ToList();
		    fieldList.AddRange(CastlingMove(field));

		    return fieldList;
	    }

        public IEnumerable<IField> WhereCanBeat(IField field)
        {
            return NormalMove(field);
        }

        public void DoCastlingMove(IField oldPosition, IField newPosition)
        {
            castlingRules.DoCastlingMove(oldPosition, newPosition);
        }

        public void DoMove(IField oldPosition)
        {
            /*
            if(!KingDoMoveList.Any(kingID => kingID == oldPosition.Pawn.ID))
            {
                KingDoMoveList.Add(oldPosition.Pawn.ID);
            }
            */
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
            return castlingRules.CastlingMove(position);

        }
    }
}
