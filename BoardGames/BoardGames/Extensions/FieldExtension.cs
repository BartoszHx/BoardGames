using BoardGamesShared.Enums;
using BoardGamesShared.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace BoardGames.Extensions
{
    internal static class FieldExtension
    {
	    public static PawColors GetPawnStartColor(this IField field) //Pomyśleć czy nie przesadzam z tym extensionalem.
	    {
            return field.Heigh == 1 || field.Heigh == 2 || field.Heigh == 3 || field.Heigh == 4 ? PawColors.White : PawColors.Black;
	    }

	    public static IField InThisSamePosition(this IEnumerable<IField> fieldList, IField field)
	    {
		    return fieldList.FirstOrDefault(f => f.Width == field.Width && f.Heigh == field.Heigh);
	    }

	    public static List<IField> CopyList(this IEnumerable<IField> fieldList)
	    {
		    return fieldList.Select(field => field.Copy()).ToList();
	    }

        public static List<IField> CopyList<T>(this IEnumerable<IField> fieldList) where T : IField, new()
        {
	        return fieldList.Select(field => field.Copy<T>()).ToList();
        }

	    public static IField Copy<T>(this IField field) where T : IField, new()
	    {
		    return new T()
		    {
			    Pawn = field.Pawn,
			    Heigh = field.Heigh,
			    Width = field.Width
		    };
	    }
    }
}
