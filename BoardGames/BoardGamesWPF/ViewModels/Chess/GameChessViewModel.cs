using BoardGames.Games.Chess;
using BoardGamesShared.Enums;
using BoardGamesShared.Interfaces;
using BoardGamesWPF.Models;
using System.Collections.Generic;

namespace BoardGamesWPF.ViewModels.Chess
{
    public class GameChessViewModel : GameViewModel
    {
        protected IChessGame ChessGame { get { return (IChessGame)base.Game; } }
        
        //Solo
        public GameChessViewModel()
        {

            List<IPlayer> playerList = new List<IPlayer>();
            playerList.Add(new PlayerModel("Test1",PawColors.White));
            playerList.Add(new PlayerModel("Test2", PawColors.Black));

            var game = new ChessGameCreator().StandardGame(playerList, Alert, PawnUpgradeWindow);

            base.Load(game);
        }
        

        private PawChess PawnUpgradeWindow(IEnumerable<PawChess> pawnToChoseList)
        {
            //Test
	        PawnUpgradeViewModel pawnUpgradeViewModel = new PawnUpgradeViewModel(pawnToChoseList);
            new Views.Chess.PawnUpgradeView(pawnUpgradeViewModel).ShowDialog();
	        return pawnUpgradeViewModel.SelectedPawChees.Value;
        }
    }
}
