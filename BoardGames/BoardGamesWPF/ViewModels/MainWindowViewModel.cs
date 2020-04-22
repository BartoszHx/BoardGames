using BoardGamesWPF.ViewModels.Chess;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BoardGamesWPF.ViewModels.Checkers;

namespace BoardGamesWPF.ViewModels
{
    public class MainWindowViewModel
    {
        public RelayCommand ChessClickCommand { get; set; }
        public RelayCommand CheckersClickCommand { get; set; }

        public MainWindowViewModel()
        {
            ChessClickCommand = new RelayCommand(ChessClick);
            CheckersClickCommand = new RelayCommand(CheckersClick);

            //Test
            //new Views.GameView(new ViewModels.OnlineTest.GameOnlineViewModel()).ShowDialog();
            new Views.GameView(new ViewModels.GameViewModel()).ShowDialog();

            //Ponieważ jestem leniwy, albo oszczędzam swój czas
            //ChessClick();
            //CheckersClick();
        }

        private void ChessClick()
        {
            new Views.GameView(new GameChessViewModel()).ShowDialog();
        }

        private void CheckersClick()
        {
            new Views.GameView(new GameCheckerViewModel()).ShowDialog();
        }
    }
}
