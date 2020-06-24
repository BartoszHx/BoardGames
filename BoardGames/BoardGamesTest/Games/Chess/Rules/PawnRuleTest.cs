using BoardGames.Buliders;
using BoardGamesShared.Enums;
using BoardGamesShared.Interfaces;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardGamesTest.Games.Chess.Rules
{
    [TestFixture]
    internal class PawnRuleTest
    {
        [Test]
        public void FirstMovChose()
        {
            IBoard board = new ChessBoardBulider()
                    .SetPawn(PawChess.King, 8, 4, PawColors.Black)
                    .SetPawn(PawChess.King, 1, 5, PawColors.White)
                    .SetPawn(PawChess.Pawn, 2, 4, PawColors.White)
                    .Bulid();

            IChessGame game = ChessHelper.SetChessGame(board);

            IField whitePawn = game.Board.FieldList.First(f => f.Heigh == 2 && f.Width == 4);

            //Act
            IEnumerable<IField> moveList = game.PawnWherCanMove(whitePawn);


            //Assert
            Assert.AreEqual(moveList.Count(), 2);
            Assert.IsTrue(moveList.Any(a => a.Heigh == 3 & a.Width == 4));
            Assert.IsTrue(moveList.Any(a => a.Heigh == 4 & a.Width == 4));
        }

        [Test]
        public void DoubleMove()
        {
            IBoard board = new ChessBoardBulider()
                    .SetPawn(PawChess.King, 8, 4, PawColors.Black)
                    .SetPawn(PawChess.King, 1, 5, PawColors.White)
                    .SetPawn(PawChess.Pawn, 2, 4, PawColors.White)
                    .Bulid();

            IChessGame game = ChessHelper.SetChessGame(board);

            IField whitePawn = game.Board.FieldList.First(f => f.Heigh == 2 && f.Width == 4);

            //Act
            var firstWherCanMove = game.PawnWherCanMove(whitePawn);
            var secendField = firstWherCanMove.First(f => f.Heigh == 4);
            game.PawnMove(whitePawn, secendField);
            IEnumerable<IField> secendWherCanMove = game.PawnWherCanMove(secendField); //Czy to wyjdzie?


            //Assert
            /*
            Assert.AreEqual(moveList.Count(), 2);
            Assert.IsTrue(moveList.Any(a => a.Heigh == 3 & a.Width == 4));
            Assert.IsTrue(moveList.Any(a => a.Heigh == 4 & a.Width == 4));
            */
        }
    }
}
