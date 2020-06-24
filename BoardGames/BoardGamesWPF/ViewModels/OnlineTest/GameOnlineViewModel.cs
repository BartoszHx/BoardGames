using BoardGamesWPF.ViewModels.Helpers;
using GalaSoft.MvvmLight.CommandWpf;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Media;
using BoardGamesShared.Enums;
using BoardGamesShared.Interfaces;
using BoardGamesClient.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;
using BoardGamesWPF.Models;
using BoardGamesWPF.ViewModels.Chess;

namespace BoardGamesWPF.ViewModels.OnlineTest
{
    public class GameOnlineViewModel : NotifyPropertyChanged
    {
        protected IGameClient gameClient;

        //public ObservableCollection<FieldViewModel> FieldList { get; set; }
        public int BoardHeight { get => gameClient.GameData.Board.MaxHeight + 1;  }
        public int BoardWidth { get => gameClient.GameData.Board.MaxWidth + 1;  }
        public FieldViewModel SelectedField { get; set; }
        public RelayCommand<FieldViewModel> ButtonClickCommand { get; private set; }

        public RelayCommand RefreshClickCommand { get; set; }


        public Brush PlayerTurnColor => gameClient.GameData.PlayerTurn.Color == PawColors.White ? Brushes.Wheat : Brushes.Black; //Bardziej do testu

        public Brush YouColorTest => gameClient.GameData.PlayerList.FirstOrDefault(f => gameClient.User.UserId == f.ID)?.Color == PawColors.White ? Brushes.Wheat : Brushes.Black;

		//Test
		public MessagesViewModel Messages { get; set; }
        public int MaxHeight { get; set; }
        public int MaxWidth { get; set; }
        public int MinHeight { get; set; }
        public int MinWidth { get; set; }
        //public ICollection<IField> FieldList { get; set; }

        public ObservableCollection<FieldViewModel> FieldList { get; set; }

        public GameOnlineViewModel()
        {
            Messages = new MessagesViewModel();
            FieldList = new ObservableCollection<FieldViewModel>();


            Random rand = new Random();
            int randomValue = rand.Next(31,130);

            //Test
            /* Warcaby
            var bulider = BoardGamesClient.ClientBulider.BulidGame();
            gameClient =
                bulider.SetActionMessage(Alert)
                .SetUser(new BoardGamesClient.Models.User { UserId = randomValue, Name = "Test" + randomValue })
                .SetCheckerGame(Alert)
                .SetFereshViewTest(RefreshButton)
                .Bulid();
            */

            //Szachy
            
            gameClient = BoardGamesClient.ClientBulider
                .BulidGame()
                .SetActionMessage(Alert)
                .SetUser(new BoardGamesClient.Models.User { UserId = randomValue, Name = "Test" + randomValue })
                .SetChessGame(Alert, PawnUpgradeWindow)
                .SetRereshViewAction(RefreshButton)
                .Bulid();
            Load(gameClient);

            ButtonClickCommand = new RelayCommand<FieldViewModel>(ButtonClick);
            RefreshClickCommand = new RelayCommand(RefreshButton);

        }

        public async void Load(IGameClient gameClient)
        {
            this.gameClient = gameClient;

            await gameClient.SearchOpponentAsync();

            FieldList = new ObservableCollection<FieldViewModel>();
            foreach (var field in gameClient.GameData.Board.FieldList)
                FieldList.Add(new FieldViewModel(field));

            gameClient.PlayMatchAsync();
            RefreshButton();
        }

        public void RefreshButton()
        {
            //Test2
            FieldList.Clear();
            foreach (var field in gameClient.GameData.Board.FieldList)
            {
                FieldList.Add(new FieldViewModel(field));
            }

            foreach (var field in FieldList)
            {
                field.AllOnPropertyChanged();
            }

            OnPropertyChanged("BoardHeight");
            OnPropertyChanged("BoardWidth");
            OnPropertyChanged("SelectedField");
            OnPropertyChanged(nameof(PlayerTurnColor));
            OnPropertyChanged("FieldList");
            OnPropertyChanged("YouColorTest");

        }

        public void ButtonClick(FieldViewModel field)
        {
            if (field.CanMove) DoMove(field);
            else CheckMove(field);
        }

        private void CheckMove(FieldViewModel field)
        {
            SelectedField = field;

            unselectFieldCanMove();
            var canMoveFieldList = gameClient.PawnWherCanMove(field.Field);

            foreach (var fie in canMoveFieldList)
            {
                FieldList.First(f => f.Field == fie).CanMove = true;
            }
        }

        private void DoMove(FieldViewModel field)
        {
            gameClient.PawnMove(SelectedField.Field, field.Field);
            unselectFieldCanMove();
            foreach (var fie in FieldList.Where(w => w.CanMove).ToList())
                fie.CanMove = false;

            SelectedField = null;
            RefreshButton();
        }

        private void unselectFieldCanMove()
        {
            foreach (var fie in FieldList.Where(w => w.CanMove).ToList())
                fie.CanMove = false;
        }

		//Test
	    public void Alert(MessageContents content)
	    {
            Alert(content.ToString());
        }

        //Test
        public void Alert(Dictionary<string, string> dictionary)
        {
            string message = "";
            foreach(var msg in dictionary)
            {
                message += string.Format($"{msg.Key}: {msg.Value}", System.Environment.NewLine);
            }
            Alert(message);
        }


        public void Alert(string message)
        {
            unselectFieldCanMove();
            foreach (var fie in FieldList.Where(w => w.CanMove).ToList())
                fie.CanMove = false;

            Messages.Show(message);
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
