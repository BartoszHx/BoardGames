using BoardGamesShared.Interfaces;
using System.Collections.Generic;

namespace BoardGames.Interfaces
{
    internal interface IMoveRule //Pomyślec czy ten interfejs jest potrzebny, jak na razie służy do tego aby były takie same nazwy
    {
	    IEnumerable<IField> WhereCanMove(IField field);
        void SetStartPositionOnBoard(ref int idIncrementation);
    }
}
