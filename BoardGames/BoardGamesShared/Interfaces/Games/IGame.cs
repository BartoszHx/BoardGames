using System;
using System.Collections.Generic;

namespace BoardGamesShared.Interfaces
{
    public interface IGame : IGameData
    {
	    void StartGame(IEnumerable<IPlayer> playerList);
	    IEnumerable<IField> PawnWherCanMove(IField field);
	    void PawnMove(IField fieldOld, IField fieldNew);
		Action<Enums.MessageContents> Alert { get; set; }
    }
}
