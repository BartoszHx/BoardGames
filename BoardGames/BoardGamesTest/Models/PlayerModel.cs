using BoardGamesShared.Enums;
using BoardGamesShared.Interfaces;

namespace BoardGamesTest.Models
{
    internal class PlayerModel : IPlayer //Czy to że nie mam modelu w kodzie jest poprawne czy nie?
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public PawColors Color { get; set; }
    }
}
