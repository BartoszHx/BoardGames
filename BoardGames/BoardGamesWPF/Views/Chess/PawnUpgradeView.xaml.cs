using BoardGamesWPF.ViewModels.Chess;
using System.Windows;

namespace BoardGamesWPF.Views.Chess
{
    /// <summary>
    /// Interaction logic for PawnUpgradeView.xaml
    /// </summary>
    public partial class PawnUpgradeView : Window
    {
        public PawnUpgradeView(PawnUpgradeViewModel pawnUpgradeViewModel)
        {
	        this.DataContext = pawnUpgradeViewModel;
	        pawnUpgradeViewModel.CloseWindow = Close;
            InitializeComponent();
        }
    }
}
