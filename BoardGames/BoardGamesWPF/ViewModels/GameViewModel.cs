using BoardGamesWPF.ViewModels.Helpers;
using GalaSoft.MvvmLight.CommandWpf;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Media;
using BoardGamesShared.Enums;
using BoardGamesShared.Interfaces;

namespace BoardGamesWPF.ViewModels
{
    public class GameViewModel : NotifyPropertyChanged
    {
        protected IGame Game;
        public ObservableCollection<FieldViewModel> FieldList { get; set; }
        public int BoardHeight { get => Game.Board.MaxHeight+1;  }
        public int BoardWidth { get => Game.Board.MaxWidth+1;  }
        public FieldViewModel SelectedField { get; set; }
        public RelayCommand<FieldViewModel> ButtonClickCommand { get; private set; }

        public Brush PlayerTurnColor => Game.PlayerTurn.Color == PawColors.White ? Brushes.Wheat : Brushes.Black; //Bardziej do testu

		//Test
		public MessagesViewModel Messages { get; set; }

        public void Load(IGame game)
        {
            Game = game;

            List<IPlayer> PlayerList = new List<IPlayer>();
            PlayerList.Add(new Models.PlayerModel("Gracz1", PawColors.White));
            PlayerList.Add(new Models.PlayerModel("Gracz2", PawColors.Black));

            //Pamiętać, całą robotę ma zrobić framwork!
            Game.PlayerList = PlayerList.ToList();
            Game.PlayerTurn = (PlayerList.First());

            Messages = new MessagesViewModel();
            //Game.Alert = Messages.Show;
            //Game.Alert = Alert;

            Game.StartGame(PlayerList);
            
            FieldList = new ObservableCollection<FieldViewModel>();
            foreach (var field in Game.Board.FieldList)
                FieldList.Add(new FieldViewModel(field));
                

            ButtonClickCommand = new RelayCommand<FieldViewModel>(ButtonClick);
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
            var canMoveFieldList = Game.PawnWherCanMove(field.Field);

            foreach (var fie in canMoveFieldList)
                FieldList.First(f => f.Field == fie).CanMove = true;
        }

        private void DoMove(FieldViewModel field)
        {
            Game.PawnMove(SelectedField.Field, field.Field);
            unselectFieldCanMove();
            foreach (var fie in FieldList)
                fie.AllOnPropertyChanged();

            SelectedField = null;
			OnPropertyChanged(nameof(PlayerTurnColor));
        }

        private void unselectFieldCanMove()
        {
            foreach (var fie in FieldList.Where(w => w.CanMove).ToList())
                fie.CanMove = false;
        }

		//Test
	    public void Alert(MessageContents content)
	    {
		    unselectFieldCanMove();
            foreach (var fie in FieldList)
			    fie.AllOnPropertyChanged();

			Messages.Show(content.ToString()); //Dać Tutaj converter na wiadomości
        }

    }
}
