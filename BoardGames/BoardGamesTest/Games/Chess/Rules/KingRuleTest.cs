using BoardGames.Buliders;
using BoardGames.Games.Chess;
using BoardGamesShared.Enums;
using BoardGamesShared.Interfaces;
using BoardGamesTest.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardGamesTest.Games.Chess.Rules
{
    [TestFixture]
    public class KingRuleTest
    {
        [Test]
        public void CheckOnlyKnight()
        {
            IBoard board = new ChessBoardBulider()
                                .SetPawn(PawChess.King, 1, 1, PawColors.Black)
                                .SetPawn(PawChess.King, 2, 5, PawColors.White)
                                .SetPawn(PawChess.Queen, 4, 3, PawColors.Black)
                                .Bulid();

            IChessGame game = ChessHelper.SetChessGame(board);

            IField whiteKing = game.Board.FieldList.First(f => f.Pawn?.Type == PawType.KingChess && f.Pawn?.Color == PawColors.White);

            //Act
            IEnumerable<IField> moveList = game.PawnWherCanMove(whiteKing);

            //Assert
            Assert.AreEqual(moveList.Count(), 6);
            Assert.IsTrue(moveList.Any(a => a.Heigh == 3 & a.Width == 5));
            Assert.IsTrue(moveList.Any(a => a.Heigh == 3 & a.Width == 6));
            Assert.IsTrue(moveList.Any(a => a.Heigh == 2 & a.Width == 4));
            Assert.IsTrue(moveList.Any(a => a.Heigh == 2 & a.Width == 6));
            Assert.IsTrue(moveList.Any(a => a.Heigh == 1 & a.Width == 4));
            Assert.IsTrue(moveList.Any(a => a.Heigh == 1 & a.Width == 5));
        }

        [Test]
        public void CheckWithPawns()
        {
            IBoard board = new ChessBoardBulider()
                    .SetPawn(PawChess.King, 1, 1, PawColors.Black)
                    .SetPawn(PawChess.Queen, 4, 3, PawColors.Black)
                    .SetPawn(PawChess.King, 2, 5, PawColors.White)
                    .SetPawn(PawChess.Pawn, 2, 4, PawColors.White)
                    .SetPawn(PawChess.Knight, 5, 5, PawColors.White)
                    .SetPawn(PawChess.Rock, 3, 8, PawColors.White)
                    .Bulid();

            IChessGame game = ChessHelper.SetChessGame(board);

            IField whitePawn = game.Board.FieldList.First(f => f.Pawn?.Type == PawType.PawnChess);
            IField whiteKnight = game.Board.FieldList.First(f => f.Pawn?.Type == PawType.KnightChess);
            IField whiteRock = game.Board.FieldList.First(f => f.Pawn?.Type == PawType.RockChess);

            //Act
            IEnumerable<IField> pawnMoveList = game.PawnWherCanMove(whitePawn);
            IEnumerable<IField> knightMoveList = game.PawnWherCanMove(whiteKnight);
            IEnumerable<IField> rockMoveList = game.PawnWherCanMove(whiteRock);

            //Assert
            Assert.AreEqual(pawnMoveList.Count(), 1);
            Assert.IsTrue(pawnMoveList.Any(a => a.Heigh == 3 & a.Width == 4));

            Assert.AreEqual(knightMoveList.Count(), 2);
            Assert.IsTrue(knightMoveList.Any(a => a.Heigh == 3 & a.Width == 4));
            Assert.IsTrue(knightMoveList.Any(a => a.Heigh == 4 & a.Width == 3));

            Assert.AreEqual(rockMoveList.Count(), 1);
            Assert.IsTrue(rockMoveList.Any(a => a.Heigh == 3 & a.Width == 4));

        }

        [Test]
        public void CheckWithEnemyPawns()
        {
            IBoard board = new ChessBoardBulider()
                    .SetPawn(PawChess.King, 1, 1, PawColors.Black)
                    .SetPawn(PawChess.Queen, 4, 3, PawColors.Black)
                    .SetPawn(PawChess.Rock, 7, 5, PawColors.Black)
                    .SetPawn(PawChess.Bishop, 5, 8, PawColors.Black)
                    .SetPawn(PawChess.King, 2, 5, PawColors.White)
                    .SetPawn(PawChess.Pawn, 2, 4, PawColors.White)
                    .SetPawn(PawChess.Knight, 5, 5, PawColors.White)
                    .SetPawn(PawChess.Rock, 3, 6, PawColors.White)
                    .Bulid();

            IChessGame game = ChessHelper.SetChessGame(board);

            IField whiteKing = game.Board.FieldList.First(f => f.Pawn?.Type == PawType.KingChess && f.Pawn?.Color == PawColors.White);
            IField whitePawn = game.Board.FieldList.First(f => f.Pawn?.Type == PawType.PawnChess && f.Pawn?.Color == PawColors.White);
            IField whiteKnight = game.Board.FieldList.First(f => f.Pawn?.Type == PawType.KnightChess && f.Pawn?.Color == PawColors.White);
            IField whiteRock = game.Board.FieldList.First(f => f.Pawn?.Type == PawType.RockChess && f.Pawn?.Color == PawColors.White);

            //Act
            IEnumerable<IField> kingMoveList = game.PawnWherCanMove(whiteKing);
            IEnumerable<IField> pawnMoveList = game.PawnWherCanMove(whitePawn);
            IEnumerable<IField> knightMoveList = game.PawnWherCanMove(whiteKnight);
            IEnumerable<IField> rockMoveList = game.PawnWherCanMove(whiteRock);

            //Assert
            Assert.AreEqual(kingMoveList.Count(), 4);
            Assert.IsTrue(kingMoveList.Any(a => a.Heigh == 3 & a.Width == 5));
            Assert.IsTrue(kingMoveList.Any(a => a.Heigh == 2 & a.Width == 6));
            Assert.IsTrue(kingMoveList.Any(a => a.Heigh == 1 & a.Width == 4));
            Assert.IsTrue(kingMoveList.Any(a => a.Heigh == 1 & a.Width == 5));

            Assert.AreEqual(pawnMoveList.Count(), 1);
            Assert.IsTrue(pawnMoveList.Any(a => a.Heigh == 3 & a.Width == 4));

            Assert.AreEqual(knightMoveList.Count(), 0);

            Assert.AreEqual(rockMoveList.Count(), 0);
        }
    }
}
