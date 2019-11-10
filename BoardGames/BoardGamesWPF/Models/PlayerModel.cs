using BoardGamesShared.Enums;
using BoardGamesShared.Interfaces;

namespace BoardGamesWPF.Models
{
    public class PlayerModel : IPlayer
    {
        public int ID { get; set; }

        public string Name { get; set; }
        public PawColors Color { get; set; }

        public PlayerModel(string name, PawColors color)
        {
            Name = name;
            Color = color;
        }
    }
}
