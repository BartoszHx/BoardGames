using BoardGames.Extensions;
using BoardGames.Interfaces;
using BoardGames.KernelModules;
using BoardGamesShared.Enums;
using BoardGamesShared.Interfaces;
using Ninject;
using Ninject.Parameters;
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
	    public Action<MessageContents> Alert { get; set; } //Pokminić jak to ma wygladać
	    public Func<IEnumerable<PawChess>,PawChess> ChosePawUpgrade { get; set; }

	    private readonly List<PawChess> pawToChoseList;

	    private IEnumerable<PawColors> ColorsInGame => PlayerList.Select(s => s.Color);
	    private readonly IRulesChess Rules;
	    protected IKernel kernel => KernelInstance.ChessKernel;

	    public ChessGame()
        {
		    Board = kernel.Get<IBoard>();
	        Rules = kernel.Get<IRulesChess>(new ConstructorArgument("board", Board));
			PlayerList = new List<IPlayer>();

			pawToChoseList = new List<PawChess>(){ PawChess.Bishop, PawChess.Knight, PawChess.Queen, PawChess.Rock };
        }

	    public void StartGame(IEnumerable<IPlayer> playerList)
	    {
		    Board.MaxHeight = 8;
		    Board.MaxWidth = 8;
		    Board.MinHeight = 1;
		    Board.MinWidth = 1;
            SetStartBoard();
			Rules.SetStartPositionPaws();
            PlayerList = playerList.ToList(); //Dać walidację?
			SetStartPlayers();
	    }

	    private void SetStartBoard()
	    {
		    Board.SetStartBoard(kernel);
        }

	    private void SetStartPlayers()
        {
            bool isNotSetFirstPlayer = PlayerList.All(a => a.Color == PawColors.White)
                                    || PlayerList.All(a => a.Color == PawColors.Black);

            if (!isNotSetFirstPlayer)
            {
                PlayerTurn = PlayerList.First(f => f.Color == PawColors.White);
                return;
            }

            Random rand = new Random();

		    var playerFirst = PlayerList[rand.Next(0, 1)];
		    var playerSecond = PlayerList.First(f => f != playerFirst);

		    playerFirst.Color = PawColors.White;
		    playerSecond.Color = PawColors.Black;

		    PlayerTurn = playerFirst;
        }

	    public IEnumerable<IField> PawnWherCanMove(IField field)
	    {
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
			    Alert(MessageContents.IncorrectMovement);
			    return;
		    }

			//Pomyśl. Zamisat mieć dwie moetody, gdzie szachmat musi wywolać szach. Przez co wołamy dwa razy tą samą metodę
			//Lepiej było by sprawdzić czy jest szach, zanotować tą informację (np. w Enum?), a potem sprawdzić czy jest szachmat.

		    PawColors? colorInIntreaction = Rules.IsCheckmateOnColor(ColorsInGame);
		    if (colorInIntreaction.HasValue)
		    {
			    Alert(MessageContents.Checkmate);
			    return;
            }

		    colorInIntreaction = Rules.IsCheckOnColor(ColorsInGame);
		    if (colorInIntreaction.HasValue)
		    {
			    Alert(MessageContents.Check);
		    }

            if (Rules.IsPawnUpgrade(fieldNew))
            {
	            PawChess pawChosed = ChosePawUpgrade(pawToChoseList);
	            fieldNew.Pawn.Type = (PawType)pawChosed;
            }

		    PlayerTurn = PlayerList.FirstOrDefault(p => p != PlayerTurn);
	    }
    }
}
