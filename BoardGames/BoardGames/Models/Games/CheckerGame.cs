using BoardGames.Interfaces;
using System;
using System.Collections.Generic;

namespace BoardGames.Models.Games
{
    internal class CheckerGame : IGame
    {
	    public IPlayer PlayerTurn { get; set; }
	    public IList<IPlayer> PlayerList { get; set; }
	    public IBoard Board { get; set; }
	    public Action<string> Alert { get; }

        public CheckerGame()
        {
			/*
            kernel = new StandardKernel(new CheckersModule());
            Initialize();
			*/
        }

	    public void StartGame()
	    {
		    throw new NotImplementedException();
	    }


	    public IEnumerable<IField> PawnWherCanMove(IField field)
	    {
		    throw new NotImplementedException();
	    }

        public void PawnMove(IField fieldOld, IField fieldNew)
        {
            throw new NotImplementedException();
        }

    }
}
