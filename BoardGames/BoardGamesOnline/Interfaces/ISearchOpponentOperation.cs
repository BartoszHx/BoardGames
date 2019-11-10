using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BoardGameDatabase.Models.Entites;
using BoardGamesOnline.Models;

namespace BoardGamesOnline.Interfaces
{
    internal interface ISearchOpponentOperation
    {
        IReadOnlyList<SearchOpponent> SearchOpponentList { get; }

        Task AddToSearch(SearchOpponent searchOpponent);

        void CancelSearch(int userID);

        //Parowanie?
    }
}
