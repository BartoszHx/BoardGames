using BoardGames.Games.Checkers.Rules;
using BoardGames.Models.Checkers.Rules;
using BoardGamesShared.Enums;
using BoardGamesShared.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BoardGames.Games.Checkers
{
    internal class RulesChecker
    {
	    public PawnRules PawnRules { get; private set; }
	    public QueenRules QueenRules { get; private set; }

		//pomyśleć z tym parametrem! Przerobić na statica?, niech inne modele go dziedziczą?
	    private StandardCheckersMoveRules standardRules;

        private IBoard board;

		//Wygrana jak przeciwnik nie ma pionków

		//Zrobić refaktor

	    public RulesChecker(IBoard board)
	    {
		    this.board = board;
			PawnRules = new PawnRules(board);
			QueenRules = new QueenRules(board);
			standardRules = new StandardCheckersMoveRules(board);
	    }

	    public IEnumerable<IField> WherPawnCanMove(IField field)
	    {
		    return PawHaveToBeat(field.Pawn.Color).Any() ? BeatMove(field) : NormalMove(field);
	    }

	    public IEnumerable<IField> NormalMove(IField field)
	    {
		    switch (field.Pawn.Type)
		    {
			    case PawType.PawnCheckers:  return PawnRules.NormalMove(field);
			    case PawType.QueenCheckers: return QueenRules.NormalMove(field);
			    default:                          throw new NotImplementedException();
		    }
        }

	    public IEnumerable<IField> BeatMove(IField field)
	    {
		    switch (field.Pawn.Type)
		    {
			    case PawType.PawnCheckers:  return PawnRules.BeatMove(field);
			    case PawType.QueenCheckers: return QueenRules.BeatMove(field);
			    default:                          throw new NotImplementedException();
		    }
	    }

        public bool PawnMove(IField fieldOld, IField fieldNew)
	    {
		    IEnumerable<IField> wherCanMove = WherPawnCanMove(fieldOld);
		    if (!wherCanMove.Contains(fieldNew))
		    {
				return false;
		    }

			DoBeatMove(fieldOld, fieldNew);

		    fieldNew.Pawn = fieldOld.Pawn;
		    fieldOld.Pawn = null;

            return true;
	    }

	    public IEnumerable<IField> PawHaveToBeat(PawColors color)
	    {
			List<IField> fieldList = new List<IField>();
		    IEnumerable<IField> paws = board.FieldList.Where(w => w.Pawn?.Color == color);
		    foreach (IField field in paws)
		    {
			    if (BeatMove(field).Any())
			    {
					fieldList.Add(field);
			    }
            }

		    return fieldList;
	    }

	    public void SetStartPositionOnBoard()
	    {
			PawnRules.SetStartPositionOnBoard();
	    }

        //Nie działa dla Damki. Trzeba trochę to zmienić. Oprzeć się na GetNextCrossField itp
        private void DoBeatMove(IField fieldOld, IField fieldNew)
	    {
		    bool isBeat = BeatMove(fieldOld).Contains(fieldNew);
		    if (!isBeat)
		    {
				return;
		    }

		    standardRules.GetPreviousCrossField(fieldOld, fieldNew).Pawn = null;
	    }
    }
}
