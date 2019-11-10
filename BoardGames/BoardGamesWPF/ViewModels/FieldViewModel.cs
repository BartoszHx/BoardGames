using BoardGamesWPF.ViewModels.Helpers;
using System.Windows.Media;
using BoardGamesShared.Enums;
using BoardGamesShared.Interfaces;

namespace BoardGamesWPF.ViewModels
{
    //Zrobić tutaj porządek
    public class FieldViewModel : NotifyPropertyChanged
    {
        #region #Properties

        public IField Field { get; private set; }

        public int Heigh => 9 - Field.Heigh; // Pomyśleć nad tym
        public int Width => Field.Width;
        public string Name => getName();
        public Brush Color => getColor();

        private bool canMove;
        public bool CanMove
        {
            get => canMove;
            set { canMove = value; OnPropertyChanged(); OnPropertyChanged(nameof(Color)); }
        }

        #endregion

        #region Constructors

        public FieldViewModel(IField field)
        {
            this.Field = field;
        }

        #endregion

        #region Methods

        public void AllOnPropertyChanged()
        {
            OnPropertyChanged(nameof(Heigh));
            OnPropertyChanged(nameof(Width));
            OnPropertyChanged(nameof(CanMove));
            OnPropertyChanged(nameof(Name));
            OnPropertyChanged(nameof(Color));
        }

        private string getName()
        {
            if (Field.Pawn == null)
                return null;

            switch (Field.Pawn.Type)
            {
                case PawType.BishopChess: return "Goniec";
                case PawType.KingChess: return "Król";
                case PawType.KnightChess: return "Koń";
                case PawType.PawnChess: return "Pionek";
                case PawType.QueenChess: return "Królowa";
                case PawType.RockChess: return "Wieża";
                case PawType.PawnCheckers: return "Pionek";
                case PawType.QueenCheckers: return "Queen";
                default: return null;
            }
        }

        private Brush getColor()
        {
            if (canMove)
                return new SolidColorBrush(Colors.Red);

            if (Field.Pawn == null)
                return new SolidColorBrush(Colors.Wheat);

            return new SolidColorBrush(Field.Pawn.Color == PawColors.Black ? Colors.DarkGray : Colors.White);
        }
        #endregion
    }
}
