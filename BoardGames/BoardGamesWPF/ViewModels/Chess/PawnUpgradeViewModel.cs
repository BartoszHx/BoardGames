using BoardGamesShared.Enums;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;

namespace BoardGamesWPF.ViewModels.Chess
{
    //public Enums 
    public class PawnUpgradeViewModel
    {
	    public IEnumerable<PawChess> PawChessList { get; set; }
	    public PawChess? SelectedPawChees { get; set; }

	    public Action CloseWindow { get; set; }
	    public RelayCommand ChosedPawCommand { get; private set; }

	    public PawnUpgradeViewModel(IEnumerable<PawChess> pawChessList)
	    {
		    PawChessList = pawChessList;
			ChosedPawCommand = new RelayCommand(ChosedPaw);
	    }

	    private void ChosedPaw()
	    {
		    if (!SelectedPawChees.HasValue)
		    {
				return;
		    }

		    CloseWindow();
	    }
    }
}
