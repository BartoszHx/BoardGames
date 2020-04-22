using BoardGamesShared.Enums;
using BoardGamesShared.Interfaces;
using System.Collections.Generic;

namespace BoardGamesWPF.ViewModels.Chess
{
    public class GameChessViewModel : GameViewModel
    {
        protected IChessGame ChessGame { get { return (IChessGame)base.Game; } }
        
        //Solo
        public GameChessViewModel()
        {
            var game = new BoardGames.Buliders.ChessGameBulider()
                .SetAlertMessage(Alert)
                .SetChosePawUpgradeFunction(PawnUpgradeWindow)
                .Bulid();

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
