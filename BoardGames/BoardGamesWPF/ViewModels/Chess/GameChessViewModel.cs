using BoardGamesShared.Enums;
using BoardGamesShared.Interfaces;
using System.Collections.Generic;

namespace BoardGamesWPF.ViewModels.Chess
{
    public class GameChessViewModel : GameViewModel
    {
        protected IChessGame ChessGame { get { return (IChessGame)base.Game; } }

        public GameChessViewModel()
            :base(GameTypes.Chess)
        {
	        ChessGame.ChosePawUpgrade = PawnUpgradeWindow;
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
