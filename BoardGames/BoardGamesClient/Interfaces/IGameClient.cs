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
        GamePlay GamePlay { get; } //Zastanowić się nad tym
        User User { get; }
        Task SearchOpponentAsync(GameTypes gametype);
        void CancelSearchOpponent();
        IEnumerable<IField> PawnWherCanMove(IField field);
        void PawnMove(IField fieldOld, IField fieldNew);
    }
}
