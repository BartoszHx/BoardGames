using BoardGames.Interfaces;
using Ninject;
using System.Collections.Generic;
using System.Linq;
using BoardGamesShared.Interfaces;

namespace BoardGames.Extensions
{
    internal static class BoardExtension
    {
	    public static IEnumerable<IField> GetFieldListInEndHeigh(this IBoard board)
	    {
		    return board.FieldList.Where(w => w.Heigh == board.MinHeight || w.Heigh == board.MaxHeight);
	    }

	    public static IBoard Copy(this IBoard board)
	    {
            IBoard newBoard = KernelInstance.Get<IBoard>();
            newBoard.MaxHeight = board.MaxHeight;
            newBoard.MinHeight = board.MinHeight;
            newBoard.MaxWidth = board.MaxWidth;
            newBoard.MinWidth = board.MinWidth;
            newBoard.FieldList = board.FieldList;

            return newBoard;
	    }

        public static IBoard FullCopy(this IBoard board)
	    {
            IBoard newBoard = KernelInstance.Get<IBoard>();
            newBoard.MaxHeight = board.MaxHeight;
            newBoard.MinHeight = board.MinHeight;
            newBoard.MaxWidth = board.MaxWidth;
            newBoard.MinWidth = board.MinWidth;
            newBoard.FieldList = board.FieldList.CopyList();

            return newBoard;
        }

	    public static void SetStartBoard(this IBoard board)
	    {
            int idIncrementation = 0;
		    for (int i = board.MinHeight; i <= board.MaxHeight; i++)
		    for (int j = board.MinWidth; j <= board.MaxWidth; j++)
		    {
			    IField field = KernelInstance.Get<IField>();
			    field.Heigh = i;
			    field.Width = j;
                field.ID = ++idIncrementation;
                board.FieldList.Add(field);
            }
	    }
    }
}
