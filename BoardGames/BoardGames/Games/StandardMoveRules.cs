using BoardGamesShared.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BoardGames.Games
{
    internal static class StandardMoveRules
    {
        public static IField Move(IField position, IBoard board, int goWidth, int goHeigh)
        {
            int width = position.Width + goWidth;
            int heigh = position.Heigh + goHeigh;
            return board.FieldList.FirstOrDefault(f => f.Heigh == heigh && f.Width == width);//&& f.Pawn?.Color != this.Color);
        }

        private static bool IncrementatinCheck(int i, int count, int direction)
        {
            return direction == 1 ? i <= count : i >= count;
        }

        private static IEnumerable<IField> MoveCount(IField position, IBoard board, int count, Func<IField, IBoard, int, IField> addField)
        {
            List<IField> retList = new List<IField>();

            int direction = count > 0 ? 1 : -1;
            for (int i = 1 * direction; IncrementatinCheck(i, count, direction); i = i + direction)
            {
                var field = addField(position, board, i);
                if (field == null || field.Pawn?.Color == position.Pawn.Color)
                    break;

                retList.Add(field);
                if (field.Pawn != null)
                    break;
            }

            return retList;
        }

        public static IEnumerable<IField> MoveVertical(IField position, IBoard board, int count)
        {
            Func<IField, IBoard, int, IField> fun = (field, bord, i) => Move(field, bord, 0, i);
            return MoveCount(position, board, count, fun);
        }


        public static IEnumerable<IField> MoveHorizontal(IField position, IBoard board, int count)
        {
            Func<IField, IBoard, int, IField> fun = (field, bord, i) => Move(field, bord, i, 0);
            return MoveCount(position, board, count, fun);
        }

        public static IEnumerable<IField> MoveCross(IField position, IBoard board, bool goLeft, bool goUp, int count)
        {
            List<IField> retList = new List<IField>();

            int directionLeft = goLeft ? -1 : 1;
            int directionUp = goUp ? 1 : -1;

            int j = 1 * directionLeft;
            for (int i = 1 * directionUp; IncrementatinCheck(i, count * directionUp, directionUp); i = i + directionUp)
            {
                var field = Move(position, board, j, i);
                if (field == null || field.Pawn?.Color == position.Pawn.Color)
                    break;

                retList.Add(field);
                if (field.Pawn != null)
                    break;

                j = j + directionLeft;
            }

            return retList;
        }

        public static IEnumerable<IField> MoveHorizontalVertical(IField position, IBoard board, int count)
        {
            List<IField> fieldList = new List<IField>();
            fieldList.AddRange(MoveHorizontal(position, board, count));
            fieldList.AddRange(MoveHorizontal(position, board, count * -1));
            fieldList.AddRange(MoveVertical(position, board, count));
            fieldList.AddRange(MoveVertical(position, board, count * -1));
            return fieldList;
        }

        public static IEnumerable<IField> MoveAllCross(IField position, IBoard board, int count)
        {
            List<IField> fieldList = new List<IField>();
            fieldList.AddRange(MoveCross(position, board, false, false, count));
            fieldList.AddRange(MoveCross(position, board, true, false, count));
            fieldList.AddRange(MoveCross(position, board, false, true, count));
            fieldList.AddRange(MoveCross(position, board, true, true, count));
            return fieldList;
        }

	    public static int IsMoveUp(IField fieldOld, IField fieldNew)
	    {
			return fieldNew.Heigh - fieldOld.Heigh > 0 ? 1 : -1;
        }

	    public static int IsMoveRight(IField fieldOld, IField fieldNew)
	    {
		    return fieldNew.Width - fieldOld.Width > 0 ? 1 : -1;
        }

    }
}
