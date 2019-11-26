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

	    public static IBoard Copy<T>(this IBoard board)
		    where T : IBoard, new()
	    {
		    return new T()
		    {
			    MaxHeight = board.MaxHeight,
			    MinHeight = board.MinHeight,
			    MaxWidth = board.MaxWidth,
			    MinWidth = board.MinWidth,
			    FieldList = board.FieldList
		    };
	    }

        public static IBoard FullCopy<T1,T2>(this IBoard board)
		    where T1: IBoard, new()
			where T2: IField, new()
	    {
		    return new T1()
		    {
			    MaxHeight = board.MaxHeight,
			    MinHeight = board.MinHeight,
			    MaxWidth = board.MaxWidth,
			    MinWidth = board.MinWidth,
			    FieldList = board.FieldList.CopyList<T2>()
		    };
	    }

	    public static IBoard FullCopy<T>(this IBoard board)
		    where T : IBoard, new()
	    {
		    return new T()
		    {
			    MaxHeight = board.MaxHeight,
			    MinHeight = board.MinHeight,
			    MaxWidth = board.MaxWidth,
			    MinWidth = board.MinWidth,
			    FieldList = board.FieldList.CopyList()
		    };
	    }

	    public static void SetStartBoard(this IBoard board)
	    {
		    for (int i = board.MinHeight; i <= board.MaxHeight; i++)
		    for (int j = board.MinWidth; j <= board.MaxWidth; j++)
		    {
			    IField field = KernelInstance.Get<IField>();
			    field.Heigh = i;
			    field.Width = j;
			    board.FieldList.Add(field);
		    }
	    }
    }
}
