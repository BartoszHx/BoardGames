using BoardGamesShared.Enums;
using BoardGamesShared.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BoardGames.Games.Chess.Rules
{
    internal partial class CastlingRules
    {
        private readonly IBoard board;
        private readonly IList<IPawnHistory> pawnHistoriesList;
        private readonly Func<IField, IEnumerable<IField>> wherePawCanMove;

        public CastlingRules(IBoard board, IList<IPawnHistory> pawnHistoriesList, Func<IField, IEnumerable<IField>> wherePawCanMove)
        {
            this.board = board;
            this.pawnHistoriesList = pawnHistoriesList;
            this.wherePawCanMove = wherePawCanMove;
        }

        public void DoCastlingMove(IField oldPosition, IField newPosition)
        {
            bool isCastlingMove = CastlingMove(oldPosition).Contains(newPosition);
            if (!isCastlingMove)
                return;

            bool isRightSideCastling = oldPosition.Width < newPosition.Width;

            var fieldOnThisHeightList = board.FieldList.Where(w => w.Heigh == newPosition.Heigh);

            var rockCastlingPosition = fieldOnThisHeightList.First(f => (isRightSideCastling && f.Width == board.MaxWidth)
                                                           || (!isRightSideCastling && f.Width == 1)
                                                         );

            var rockNewPosition = fieldOnThisHeightList.First(f => (isRightSideCastling && f.Width == newPosition.Width - 1)
                                                                  || (!isRightSideCastling && f.Width == newPosition.Width + 1));

            rockNewPosition.Pawn = rockCastlingPosition.Pawn;
            rockCastlingPosition.Pawn = null;
        }

        public IEnumerable<IField> CastlingMove(IField kingPosition)
        {
            CastlingCheckProgres work = new CastlingCheckProgres(kingPosition);

            KingDontDoMove(work);
            GetRockDontDoMove(work);
            GetOpponentFieldMove(work);
            KingNoMat(work);
            CheckBetweenRocks(work);

            return KingFieldMove(work);
        }

        private void CheckBetweenRocks(CastlingCheckProgres work)
        {
            if (work.IsProcesFinish) return;

            var heighList = board.FieldList.Where(w => w.Heigh == work.KingPosition.Heigh);

            foreach (var rock in work.RockPositionList)
            {
                bool isLeftRock = rock.Width == board.MinWidth;
                var pawnBetweenList = isLeftRock ? heighList.Where(w => w.Width < work.KingPosition.Width && w.Width > rock.Width && w.Pawn != null)
                                              : heighList.Where(w => w.Width > work.KingPosition.Width && w.Width < rock.Width && w.Pawn != null);

                bool isPawnsBetween = pawnBetweenList.Any(a => a.Pawn != null);
                bool canBeEnemyAttack = !isPawnsBetween && pawnBetweenList.Any(paw => work.WhereEnemyCanMove.Any(enemy => enemy.ID == paw.ID));

                if (canBeEnemyAttack)
                {
                    if (isLeftRock)
                    {
                        work.CanLeftCastling = true;
                    }
                    else
                    {
                        work.CanRightCastling = true;
                    }
                }
            }
        }

        private void KingDontDoMove(CastlingCheckProgres work)
        {
            if (work.IsProcesFinish) return;

            work.IsProcesFinish = pawnHistoriesList.Any(his => his.PawID == work.KingPosition.Pawn.ID);
        }

        private void GetRockDontDoMove(CastlingCheckProgres work)
        {
            if (work.IsProcesFinish) return;

            IEnumerable<IField> rockListOnPosition = board.FieldList.Where(w => w.Pawn?.Color == work.KingPosition.Pawn.Color && w.Pawn?.Type == PawType.RockChess
                                                                            && (w.Heigh == work.KingPosition.Heigh)
                                                                            && (w.Width == board.MinWidth || w.Width == board.MaxWidth));


            work.RockPositionList = rockListOnPosition.Where(w => !pawnHistoriesList.Any(his => his.PawID == w.ID));

            if(!work.RockPositionList.Any())
            {
                work.IsProcesFinish = true;
            }
        }      

        private void GetOpponentFieldMove(CastlingCheckProgres work)
        {
            if (work.IsProcesFinish) return;

            foreach (IField field in board.FieldList.Where(w => w.Pawn != null && w.Pawn.Color != work.KingPosition.Pawn.Color))
            {
                 work.WhereEnemyCanMove.AddRange(wherePawCanMove(field));
            }
        }
            
        private void KingNoMat(CastlingCheckProgres work)
        {
            if (work.IsProcesFinish) return;

            bool isMat = work.WhereEnemyCanMove.Any(a => a.ID == work.KingPosition.ID); //Czy to zadziała?

            if(isMat)
            {
                work.IsProcesFinish = true;
            }
        }

        private IEnumerable<IField> KingFieldMove(CastlingCheckProgres work)
        {
            List<IField> result = new List<IField>();

            if(work.IsProcesFinish)
            {
                return result;
            }

            if(work.CanLeftCastling)
            {
                result.Add(board.FieldList.FirstOrDefault(field => field.Heigh == work.KingPosition.Heigh && field.Width == 2));
            }

            if(work.CanRightCastling)
            {
                result.Add(board.FieldList.FirstOrDefault(field => field.Heigh == work.KingPosition.Heigh && field.Width == 7));
            }

            return result;
        }
    }
}
