using System.Collections.Generic;
using System.Threading.Tasks;
using BoardGamesOnline.Interfaces;
using BoardGamesOnline.Models;

namespace BoardGamesOnline.Operations
{
    //Wie kto szuka przeciwnika
    //Ma cały algorytm przyznawania itp
    internal class SearchOpponentOperation : ISearchOpponentOperation
    {
        private List<SearchOpponent> searchOpponentList;

        public IReadOnlyList<SearchOpponent> SearchOpponentList => this.searchOpponentList;

        public SearchOpponentOperation()
        {
            this.searchOpponentList = new List<SearchOpponent>();
        }

        public async Task AddToSearch(SearchOpponent searchOpponent)
        {
            throw new System.NotImplementedException();

            //Dodać i oznajmić kiedy znaleziono przeciwnika
            //potem, zrobić tak że ta klasa funkcjonuje cały czas
        }

        public void CancelSearch(int userID)
        {
            throw new System.NotImplementedException();
        }
    }
}
