using BoardGames.Buliders;
using BoardGames.Extensions;
using BoardGamesShared.Enums;
using BoardGamesShared.Interfaces;
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
	    public Action<MessageContents> Alert { get; private set; }
        public IList<IPawnHistory> PawnHistoriesList { get; set; }
        public int Turn { get; set; }

        private RulesChecker Rules;

        public CheckerGame(CheckerGameBulider bulider)
        {
            //Tutaj zrobić refaktor
            Board = KernelInstance.Get<IBoard>();
			Rules = new RulesChecker(Board);
            PawnHistoriesList = new List<IPawnHistory>();
            Alert = bulider.Alert;

            Board.MaxHeight = 8;
            Board.MaxWidth = 8;
            Board.MinHeight = 1;
            Board.MinWidth = 1;
            Turn = 1;
            Board.SetStartBoard();
            Rules.SetStartPositionOnBoard();
            PlayerList = bulider.PlayerList;
            SetColorAndStartPlayer();
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

	        bool canBeatNextPaw = fieldNew.Pawn!= null && Rules.BeatMove(fieldNew).Any();
            if (isBeatMove && canBeatNextPaw)
	        {
				return;
	        }

			//Nie mam pojęcia czy: Pionek jest na ostatnim polu, dostał się tam po przez bicie, może bić dalej, to czy zamienia się w damkę czy kontynuje bicie, czy jedno i drugie?
	        if (Rules.QueenRules.CanChangePawnToQueen(fieldNew))
	        {
		        fieldNew.Pawn.Type = PawType.QueenCheckers; //Przenieść to do zasad damki?
	        }

	        if (CheckGameStatus())
	        {
				return;
	        }

	        PlayerTurn = PlayerList.FirstOrDefault(p => p.ID != PlayerTurn.ID);
        }

        public bool CheckGameStatus()
        { 
            var boardColorStatus = Board.FieldList.Where(w => w.Pawn != null)
                .Select(s => s.Pawn.Color)
                .Distinct();

            bool isEndGame = boardColorStatus.Count() == 1;
            if(isEndGame)
            {
                var pawColorWin = boardColorStatus.First();
                if(pawColorWin == PawColors.White)
                {
                    Alert(MessageContents.WinWhite);
                }
                else
                {
                    Alert(MessageContents.WinBlack);
                }

                return true;
            }

            return false;
        }

        //Do Refaktoryzacji
        private void SetColorAndStartPlayer()
        {
            Random rand = new Random();
            int chosenOneee = rand.Next(PlayerList.Count);

            var firstPlayer = PlayerList[chosenOneee];
            firstPlayer.Color = PawColors.White;
            PlayerTurn = firstPlayer;

            var secendPlayer = PlayerList.First(f => f != firstPlayer);
            secendPlayer.Color = PawColors.Black;
        }
    }
}
