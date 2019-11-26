using BoardGames.Extensions;
using BoardGames.Games.Checkers;
using BoardGames.Kernels;
using BoardGamesShared.Enums;
using BoardGamesShared.Interfaces;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BoardGames.Games.Checkers
{
    internal class CheckerGame : IGame
    {
	    public IPlayer PlayerTurn { get; set; }
	    public IList<IPlayer> PlayerList { get; set; }
	    public IBoard Board { get; set; }
	    public Action<MessageContents> Alert { get; set; }

	    private RulesChecker Rules; //Poźniej na interface

        public CheckerGame()
        {
	        Board = KernelInstance.Get<IBoard>();
			Rules = new RulesChecker(Board);
        }

	    public void StartGame(IEnumerable<IPlayer> playerList)
	    {
		    Board.MaxHeight = 8;
		    Board.MaxWidth = 8;
		    Board.MinHeight = 1;
		    Board.MinWidth = 1;
            Board.SetStartBoard();
            Rules.SetStartPositionOnBoard();
            PlayerList = playerList.ToList();
        }

	    public IEnumerable<IField> PawnWherCanMove(IField field)
	    {
		    return field.Pawn != null ? Rules.WherPawnCanMove(field) : new List<IField>();
	    }

        public void PawnMove(IField fieldOld, IField fieldNew)
        {
	        if (fieldOld.Pawn.Color != PlayerTurn.Color)
	        {
				return;
	        }

            bool isBeatMove = Rules.BeatMove(fieldOld).Contains(fieldNew);

            Rules.PawnMove(fieldOld, fieldNew);

	        bool canBeatNextPaw = Rules.BeatMove(fieldNew).Any();
            if (isBeatMove && canBeatNextPaw)
	        {
				return;
	        }

			//Nie mam pojęcia czy: Pionek jest na ostatnim polu, dostał się tam po przez bicie, może bić dalej, to czy zamienia się w damkę czy kontynuje bicie, czy jedno i drugie?
	        if (Rules.QueenRules.CanChangePawnToQueen(fieldNew))
	        {
		        fieldNew.Pawn.Type = PawType.QueenCheckers; //Przenieść to do zasad damki?
	        }

	        if (IsWin(PlayerTurn.Color))
	        {
		        Alert(MessageContents.Checkmate); //Zmienić Message
				return;
	        }

	        PlayerTurn = PlayerList.FirstOrDefault(p => p != PlayerTurn);

        }

	    private bool IsWin(PawColors color)
	    {
		    return !Board.FieldList.Any(w => w.Pawn != null && w.Pawn.Color != color);
	    }
    }
}
