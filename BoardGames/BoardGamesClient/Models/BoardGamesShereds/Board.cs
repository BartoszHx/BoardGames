using System;
using System.Collections.Generic;
using System.Text;
using BoardGamesShared.Interfaces;

namespace BoardGamesClient.Models
{
    internal class Board : IBoard
    {
        public int MaxHeight { get; set; }

        public int MaxWidth { get; set; }

        public int MinHeight { get; set; }

        public int MinWidth { get; set; }

        public ICollection<IField> FieldList { get; set; }

        public IBoard Copy() //Tego nie ma być!
        {
            throw new NotImplementedException();
        }
    }
}
