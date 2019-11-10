using BoardGamesOnline.Interfaces;
using BoardGamesOnline.Interfaces.Services;
using BoardGamesOnline.Models;
using BoardGamesShared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BoardGameDatabase.Enums;
using BoardGameDatabase.Interfaces;
using BoardGameDatabase.Models;
using BoardGameDatabase.Services.Response;
using BoardGamesOnline.Operations;

namespace BoardGamesOnline.Services.GameOnline
{
    public class GameOnlineService : IGameOnlineService
    {
        private IBoardGameUnitOfWorkBulider bulider;
        public List<SearchOpponent> SearchForMetchUsers { get; set; } //Nie dictionary
        public List<IGamePlay> PlayedMatches { get; set; }

        //Nie ma połączniea z bazą?
        public GameOnlineService(IBoardGameUnitOfWorkBulider bulider)
        {
            this.bulider = bulider;
            SearchForMetchUsers = new List<SearchOpponent>();
            this.PlayedMatches = new List<IGamePlay>();
        }

        public async Task<Match> SearchOpponentAsync(SearchOpponent searchOpponent)
        {
            //jak na razie test
            SearchForMetchUsers.Add(searchOpponent);

            Match findOpponent = null;
            while (findOpponent == null)
            {
                await Task.Delay(1000);
                if (searchOpponent.IsCancel)
                {
                    break;
                }
                findOpponent = this.PlayedMatches.FirstOrDefault(f => f.Match.MatchUsers.Any(a => a.User.UserId == searchOpponent.UserId))?.Match;
            }

            return findOpponent;
        }

        //Nie wygląda to na dobre rozwiązanie...
        public void CancelSearchOpponentAsync(int userId)
        {
            var cancelUser = SearchForMetchUsers.FirstOrDefault(f => f.UserId == userId);
            if (cancelUser != null)
            {
                cancelUser.IsCancel = true;
                SearchForMetchUsers.Remove(cancelUser);
            }
        }

        public async Task ConnectPlayersToMatchAsync()
        {
            while (0 != 1)
            {
                await Task.Delay(1000);
                ConnectPlayersToMatch(GameTypes.Chess); 
                ConnectPlayersToMatch(GameTypes.Checkers);
            }
        }

        public void ConnectPlayersToMatch(GameTypes gameType)
        {
            if (!SearchForMetchUsers.Any())
            {
                return;
            }
            //Zrobić refaktor
            //może się przydać lock
            var first = SearchForMetchUsers.FirstOrDefault(f => f.GameType == gameType);
            if (first == null)
            {
                return;
            }

            var secend = SearchForMetchUsers.FirstOrDefault(f => f.GameType == gameType && f.UserId != first.UserId);
            if (secend == null)
            {
                return;
            }

            using (IBoardGameUnitOfWork service = bulider.Bulid())
            {
                SearchForMetchUsers.Remove(first);
                SearchForMetchUsers.Remove(secend);

                List<int> userList = new List<int>{first.UserId, secend.UserId};
                //Pomyśłec co z GameData!!!! Wstępnie niech trzyma dane historyczne. Nie jest potrzebny przy created
                MatchServiceResponse respons = service.MatchService.Create(new CreateMatch { GameType = (int)gameType, UserIdList = userList });

                if (respons.Status == ServiceRespondStatus.Error)
                {
                    //Czemu exception, Nie powino być sytuacji że tego nie zapisze. Jeszcze to przemyśleć
                    throw new Exception(String.Join(String.Empty, respons.Messages.SelectMany(s => s.Key + " - " + s.Value + ". ")));
                }

                IGamePlay gamepPlay = new GamePlay();
                gamepPlay.Match = Mapping.Mapper.Map<Match>(respons.Match);

                this.PlayedMatches.Add(gamepPlay);
            }
        }
    }
}
