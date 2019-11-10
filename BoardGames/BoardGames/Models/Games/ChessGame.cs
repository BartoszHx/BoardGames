using BoardGames.Enums;
using BoardGames.Games.Chess;
using BoardGames.Interfaces;
using BoardGames.KernelModules;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using PawChess = BoardGames.Enums.PawChess;

namespace BoardGames.Models.Games
{
    internal class ChessGame : IChessGame
    {
	    public IPlayer PlayerTurn { get; set; }
	    public IList<IPlayer> PlayerList { get; set; }
	    public IBoard Board { get; set; }
	    public Action<string> Alert { get; private set; } //Pokminić jak to ma wygladać
	    public Func<PawChess, List<PawChess>> ChosePawUpgrade { get; private set; }

        private IField selectedField;
	    private readonly RulesChess Rules; //Interfeca IRulesCheess
	    protected IKernel kernel; //Zobaczyć co dalej


	    public ChessGame()
	    {
		    kernel = new StandardKernel(new ChessModule());

		    Board = kernel.Get<IBoard>();
		    Rules = new RulesChess(Board, kernel);
			PlayerList = new List<IPlayer>();
        }

	    public void StartGame()
	    {
			SetStartBoard();
			Rules.SetStartPositionPaws();
			//SetStartPlayers();
	    }

	    private void SetStartBoard()
	    {
		    for (int i = Board.MinHeight; i <= Board.MaxHeight; i++)
		    for (int j = Board.MinWidth; j <= Board.MaxWidth; j++)
		    {
			    IField field = kernel.Get<IField>();
			    field.Heigh = i;
			    field.Width = j;
				Board.FieldList.Add(field);
		    }
        }

	    private void SetStartPlayers()
	    {

		    Random rand = new Random();

		    var playerFirst = PlayerList[rand.Next(0, 1)];
		    var playerSecend = PlayerList.First(f => f != playerFirst);

		    playerFirst.Color = PawColors.White;
		    playerSecend.Color = PawColors.Black;

		    PlayerTurn = playerFirst;
        }

	    public IEnumerable<IField> PawnWherCanMove(IField field)
	    {
			//Druga kwestia!!! Nie może być ruchu, który doprowadzi do szachu
			//Czy po tym ruchu, król będzie bity?
		    return Rules.PawnWherCanMove(field);
	    }

		//pomyśleć co zwrócić. Myślę że teoretycznie ma to być void
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
			    Alert(MessagesChess.IncorrectMovement);
			    return;
		    }

			/* Pomyśleć jaki PawColor powinnien pojawić się w parametrach. Czy może całkiem coś innego powinnien pokazać?
			 //Coś typu jaki gracz jest w szachu itp.
		    if (Rules.IsCheckmate())
		    {
			    //Komunikatr i zakończenie gry
			    Alert(MessagesChess.Checkmate);
			    return; //
		    }

            if (Rules.IsCheck())
            {
	            Alert(MessagesChess.Check);
            }
			*/
		    if (Rules.IsPawnUpgrade())
		    {
				//komunikat, plus dać wybór na jaki pionek zmienić
		    }

		    PlayerTurn = PlayerList.FirstOrDefault(p => p != PlayerTurn);
	    }
    }
}
