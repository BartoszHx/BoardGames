using BoardGamesShared.Enums;
using BoardGamesShared.Interfaces;
using BoardGamesWPF.ViewModels.Helpers;

namespace BoardGamesWPF.ViewModels
{
    public class PawnViewModel : NotifyPropertyChanged
    {
        private IPawn pawn { get => fieldViewModel.Field.Pawn; }
        private FieldViewModel fieldViewModel;

        private string name;
        public string Name
        {
            get => name;
            set { name = value; OnPropertyChanged(); }
        }

        public PawColors Color { get => pawn.Color; }
        public PawType Type { get => pawn.Type; }


        public PawnViewModel(FieldViewModel fieldVM)
        {
            fieldViewModel = fieldVM;
            //this.pawn = pawn;
            nameFactory();
        }

        public void AllOnPropertyChanged()
        {
            OnPropertyChanged(nameof(Name));
            OnPropertyChanged(nameof(Color));
            OnPropertyChanged(nameof(Type));
            //OnPropertyChanged(nameof(CanMove));
        }

            private void nameFactory()
        {
            switch(pawn.Type)
            {
                case PawType.BishopChess: Name = "Goniec"; break;
                case PawType.KingChess: Name = "Król"; break;
                case PawType.KnightChess: Name = "Koń"; break;
                case PawType.PawnChess: Name = "Pionek"; break;
                case PawType.QueenChess: Name = "Królowa"; break;
                case PawType.RockChess: Name = "Wieża"; break;
                case PawType.QueenCheckers: Name = "Królowa"; break;
                case PawType.PawnCheckers: Name = "Pionek"; break;
            }

        }
    }
}
