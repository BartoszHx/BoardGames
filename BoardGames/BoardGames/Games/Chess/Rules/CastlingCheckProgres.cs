using BoardGamesShared.Interfaces;
using System.Collections.Generic;

namespace BoardGames.Games.Chess.Rules
{
    internal partial class CastlingRules
    {
        private class CastlingCheckProgres
        {
            public IField KingPosition { get; set; }
            public IEnumerable<IField> RockPositionList { get; set; }
            public List<IField> WhereEnemyCanMove { get; set; }
            public bool CanLeftCastling { get; set; }
            public bool CanRightCastling { get; set; }
            public bool IsProcesFinish { get; set; }

            public CastlingCheckProgres(IField kingPosition)
            {
                KingPosition = kingPosition;
                WhereEnemyCanMove = new List<IField>();
                IsProcesFinish = false;
            }
        }
    }
}
