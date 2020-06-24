using BoardGames.Interfaces;
using BoardGamesShared.Enums;
using BoardGamesShared.Interfaces;
using BoardGamesShared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using PawChess = BoardGamesShared.Enums.PawChess;

namespace BoardGames.Games.Chess
{
    internal class ChessGame : IChessGame
    {
	    public IPlayer PlayerTurn { get; set; }
	    public IList<IPlayer> PlayerList { get; set; }
	    public IBoard Board { get; set; }
	    public Action<MessageContents> Alert { get; private set; }
	    public Func<IEnumerable<PawChess>, PawChess> ChosePawUpgrade { get; private set; }
        public IList<IPawnHistory> PawnHistoriesList { get; set; }
        public int Turn { get; set; }

        private readonly List<PawChess> pawToChoseList;

        private IEnumerable<PawColors> colorsInGame;
	    private IRulesChess Rules;

	    public ChessGame(IChessGameBulider bulider)
        {
            Board = bulider.Board;
            PlayerList = bulider.PlayerList;
            PlayerTurn = bulider.PlayerTurn;
            pawToChoseList = bulider.PawToChoseList;
            Alert = bulider.Alert;
            ChosePawUpgrade = bulider.ChosePawUpgrade;
            PawnHistoriesList = bulider.PawnHistoriesList;
            Turn = bulider.Turn;

            Rules = new RulesChess(Board, PawnHistoriesList);
            colorsInGame = PlayerList.Select(s => s.Color);
        }

        public void StartGame(IEnumerable<IPlayer> playerList)
	    {
            //Zastanowić sie z tym
            Turn = 1;
            PlayerList = playerList.ToList(); 
			//SetStartPlayers();
            colorsInGame = PlayerList.Select(s => s.Color);
        }

	    public IEnumerable<IField> PawnWherCanMove(IField field)
	    {
		    return Rules.PawnWherCanMove(field);
	    }

        //Refaktor, metoda jest już nie czytelna
        public void PawnMove(IField fieldCurrent, IField fieldNew) 
	    {
		    bool canPlayerPlayThisPaw = fieldCurrent.Pawn.Color == PlayerTurn.Color;
		    if (!canPlayerPlayThisPaw)
		    {
			    return;
		    }

		    bool isMoveDone = Rules.PawnMove(fieldCurrent, fieldNew);
		    if (!isMoveDone)
		    {
			    Alert(MessageContents.IncorrectMovement);
			    return;
		    }



            //Pomyśl. Zamisat mieć dwie moetody, gdzie szachmat musi wywolać szach. Przez co wołamy dwa razy tą samą metodę
            //Lepiej było by sprawdzić czy jest szach, zanotować tą informację (np. w Enum?), a potem sprawdzić czy jest szachmat.

            if(CheckGameStatus())
            {
                return;
            }


            if (Rules.IsPawnUpgrade(fieldNew))
            {
                PawChess pawChosed = ChosePawUpgrade(pawToChoseList);
                fieldNew.Pawn.Type = (PawType)pawChosed;
            }


            IPawnHistory history = KernelInstance.Get<IPawnHistory>();
            history.Turn = Turn;
            history.PreviusFiledID = fieldCurrent.ID;
            history.CurrentFiledID = fieldNew.ID;
            history.PawID = fieldNew.Pawn.ID;

            PawnHistoriesList.Add(history); //Myśę że można by zrobić extensiona

            Turn++;
            PlayerTurn = PlayerList.First(p => p.ID != PlayerTurn.ID);
	    }

        public bool CheckGameStatus()
        {
            PawColors? colorInIntreaction = Rules.IsCheckmateOnColor(colorsInGame);
            if (colorInIntreaction.HasValue)
            {
                if (colorInIntreaction.Value == PawColors.White)
                {
                    Alert(MessageContents.WinWhite);
                }
                else
                {
                    Alert(MessageContents.WinBlack);
                }

                return true;
            }

            colorInIntreaction = Rules.IsCheckOnColor(colorsInGame);
            if (colorInIntreaction.HasValue)
            {
                Alert(MessageContents.Check);
            }

            return false;
        }
    }
}
