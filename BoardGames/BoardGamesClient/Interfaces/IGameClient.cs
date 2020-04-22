using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BoardGamesClient.Models;
using BoardGamesShared.Enums;
using BoardGamesShared.Interfaces;

namespace BoardGamesClient.Interfaces
{
    public interface IGameClient
    {
        IGameData GameData { get; }
        Match MatchData { get; }
        User User { get; }
        GameTypes GameType { get; }
        Task SearchOpponentAsync();
        Task PlayMatchAsync();
        void CancelSearchOpponent();
        IEnumerable<IField> PawnWherCanMove(IField field);
        void PawnMove(IField fieldOld, IField fieldNew);
    }
}
