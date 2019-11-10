using BoardGames.Games.Chess;
using BoardGamesTest.Models;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using BoardGamesShared.Enums;
using BoardGamesShared.Interfaces;

namespace BoardGamesTest.Games.Chess
{
    [TestFixture]
    public class ChessGameTest
    {
        [Test]
        public void StartGame()
        {
            IChessGame game = setChessGame();

            Assert.AreEqual(game.Board.MaxHeight, 8);
            Assert.AreEqual(game.Board.MaxWidth, 8);
            Assert.AreEqual(game.Board.MinWidth, 1);
            Assert.AreEqual(game.Board.MinHeight, 1);

            Assert.AreEqual(game.Board.FieldList.Count, 64);

            Assert.AreEqual(game.Board.FieldList.Where(a => a.Heigh == 8 && a.Pawn?.Color == PawColors.Black).Count(), 8);
            Assert.AreEqual(game.Board.FieldList.Where(a => a.Heigh == 7 && a.Pawn?.Color == PawColors.Black).Count(), 8);
            Assert.AreEqual(game.Board.FieldList.Where(a => a.Heigh == 1 && a.Pawn?.Color == PawColors.White).Count(), 8);
            Assert.AreEqual(game.Board.FieldList.Where(a => a.Heigh == 2 && a.Pawn?.Color == PawColors.White).Count(), 8);

            Assert.AreEqual(game.Board.FieldList.Where(a => a.Heigh == 3 && a.Pawn == null).Count(), 8);
            Assert.AreEqual(game.Board.FieldList.Where(a => a.Heigh == 4 && a.Pawn == null).Count(), 8);
            Assert.AreEqual(game.Board.FieldList.Where(a => a.Heigh == 5 && a.Pawn == null).Count(), 8);
            Assert.AreEqual(game.Board.FieldList.Where(a => a.Heigh == 6 && a.Pawn == null).Count(), 8);
        }

        [Test]
        public void PawnWherCanMoveStartGame()
        {
            IChessGame game = setChessGame();

            var pawn1Black = game.Board.FieldList.First(f => f.Heigh == 2 && f.Width == 1);
            var pawn1White = game.Board.FieldList.First(f => f.Heigh == 7 && f.Width == 1);
            var pawnKingBlack = game.Board.FieldList.First(f => f.Pawn?.Type == PawType.KingChess && f.Pawn?.Color == PawColors.Black);
            var pawnKingWhite = game.Board.FieldList.First(f => f.Pawn?.Type == PawType.KingChess && f.Pawn?.Color == PawColors.White);

            //Act
            var result1Black = game.PawnWherCanMove(pawn1Black);
            var result1White = game.PawnWherCanMove(pawn1White);
            var resultKingBlack = game.PawnWherCanMove(pawnKingBlack);
            var resultKingWhite = game.PawnWherCanMove(pawnKingWhite);

            //Assert
            Assert.AreEqual(result1Black.Count(), 2);
            Assert.AreEqual(result1Black.Any(a=> a.Width == 1 && a.Heigh == 3), true);
            Assert.AreEqual(result1Black.Any(a=> a.Width == 1 && a.Heigh == 4), true);

            Assert.AreEqual(result1White.Count(), 2);
            Assert.AreEqual(result1White.Any(a => a.Width == 1 && a.Heigh == 6), true);
            Assert.AreEqual(result1White.Any(a => a.Width == 1 && a.Heigh == 5), true);

            Assert.AreEqual(resultKingBlack.Count(), 0);
            Assert.AreEqual(resultKingWhite.Count(), 0);
        }

        [Test]
        public void PawnMoveStartGame()
        {
            IChessGame game = setChessGame();
            var whitePlayer = new PlayerModel { Color = PawColors.White, Name = "White" };
            var blackPlayer = new PlayerModel { Color = PawColors.Black, Name = "Black" };
            game.PlayerList = new List<IPlayer> { whitePlayer, blackPlayer };
            game.PlayerTurn = whitePlayer;

            var pawn1White = game.Board.FieldList.First(f => f.Heigh == 2 && f.Width == 1);
            var pawn1Black = game.Board.FieldList.First(f => f.Heigh == 7 && f.Width == 1);

            var epmtyFile1WhiteCorrect = game.Board.FieldList.First(f => f.Heigh == 3 && f.Width == 1);
            var epmtyFile1BlackCorrect = game.Board.FieldList.First(f => f.Heigh == 6 && f.Width == 1);

            //Act
            game.PawnMove(pawn1White, epmtyFile1WhiteCorrect);
            game.PawnMove(pawn1Black, epmtyFile1BlackCorrect);

            //Assert
            Assert.NotNull(game.Board.FieldList.First(f => f.Heigh == 6 && f.Width == 1).Pawn);
            Assert.NotNull(game.Board.FieldList.First(f => f.Heigh == 3 && f.Width == 1).Pawn);
        }

        private IChessGame setChessGame()
        {
            IChessGame game = new ChessGame();
            List<PlayerModel> playerList = new List<PlayerModel>();
            playerList.Add(new PlayerModel { Color = PawColors.White, ID = 1, Name = "Test1" });
            playerList.Add(new PlayerModel { Color = PawColors.Black, ID = 2, Name = "Test1" });
            game.StartGame(playerList);
            return game;
        }
    }
}
