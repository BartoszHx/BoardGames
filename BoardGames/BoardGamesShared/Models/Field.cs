using BoardGamesShared.Interfaces;

namespace BoardGamesShared.Models
{
    public class Field : IField
    {
        public int ID { get; set; }
        public int Heigh { get; set; }
        public int Width { get; set; }
        public IPawn Pawn { get; set; }

        public override string ToString()
        {
            return Pawn?.Type + " " + Pawn.Color.ToString();
        }
    }
}
