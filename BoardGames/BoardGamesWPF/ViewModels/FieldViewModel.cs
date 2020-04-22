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

        public int HeighView => 9 - Field.Heigh; // Pomyśleć nad tym
        public int WidthView => Field.Width;
        public string Name => getName();
        public Brush Color => getColor();

        private bool canMove;
        public bool CanMove
        {
            get => canMove;
            set { canMove = value; OnPropertyChanged(nameof(Color)); }
        }

        //public IPawn Pawn { get => Field.Pawn; set { Field.Pawn = value; OnPropertyChanged(nameof(Pawn)); } }

        #endregion

        #region Constructors

        public FieldViewModel(IField field)
        {
            Field = field;
        }

        #endregion

        #region Methods

        public void AllOnPropertyChanged()
        {
            OnPropertyChanged(nameof(HeighView));
            OnPropertyChanged(nameof(WidthView));
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
                case PawType.QueenCheckers: return "Królowa";
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

        public override string ToString()
        {
            return string.Format($"H: {HeighView} W: {WidthView} {Name} {Color}");
        }
        #endregion
    }
}
