using BoardGames.Extensions;
using BoardGamesShared.Enums;
using BoardGamesShared.Interfaces;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardGames.Buliders
{
    public class ChessBoardBulider
    {
        private int maxHeight;
        private int maxWidth;
        private List<IField> fieldList;
        private int pawId;

        public ChessBoardBulider()
        {
            fieldList = new List<IField>();
            pawId = 1;
            maxHeight = 8;
            maxWidth = 8;
        }

        public ChessBoardBulider SetPawn(PawChess pawnType, int height, int width, PawColors color)
        {
            //Przydała by się walidacja
            var pawn = KernelInstance.Get<IPawn>();
            pawn.Color = color;
            pawn.ID = pawId;
            pawn.Type = (PawType)pawnType;

            IField field = KernelInstance.Get<IField>();
            field.Heigh = height;
            field.Width = width;
            field.Pawn = pawn;

            fieldList.Add(field);

            pawId++;

            return this;
        }

        public ChessBoardBulider SetAllPawnInHeigh(PawChess pawnType, int height,PawColors color)
        {
            for(int i = 1; i <= maxWidth; i++)
            {
                SetPawn(pawnType, height, i, color);
            }

            return this;
        }

        public ChessBoardBulider SetPawnInTwoCorner(PawChess pawnType, int height, int firstWidth, PawColors color)
        {
            int secendWidth = maxWidth - firstWidth + 1;
            SetPawn(pawnType, height, firstWidth, color);
            SetPawn(pawnType, height, secendWidth, color);
            return this;
        }


        public IBoard Bulid()
        {
            IBoard board = SetBoard();
            SetPawnInFieldList(board);

            return board;
        }

        private IBoard SetBoard()
        {
            IBoard board = KernelInstance.Get<IBoard>();
            board.MaxHeight = maxHeight;
            board.MaxWidth = maxWidth;
            board.MinHeight = 1;
            board.MinWidth = 1;

            board.SetStartBoard();

            return board;
        }

        private void SetPawnInFieldList(IBoard board)
        {
            foreach (var field in fieldList)
            {
                var boardField = board.FieldList.First(f => f.Heigh == field.Heigh && f.Width == field.Width);
                boardField.Pawn = field.Pawn;
            }
        }
    }
}
