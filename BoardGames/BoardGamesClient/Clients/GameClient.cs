using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BoardGamesClient.Buliders;
using BoardGamesClient.Configurations.AutoMappers;
using BoardGamesClient.Interfaces;
using BoardGamesClient.Models;
using BoardGamesClient.Responses;
using BoardGamesClient.Servers;
using BoardGamesShared.Enums;
using BoardGamesShared.Interfaces;
using BoardGamesShared.Models;

namespace BoardGamesClient.Clients
{
    internal class GameClient : IGameClient
    {
        private GameServer gameServer;
        private IGame game;
        private Action<Dictionary<string, string>> message;
        private bool isOpponentSearchComplited;//Test

        public IGameData GameData { get => (IGameData)game; set => SetGameData(value); }

        public Match MatchData { get; private set; }

        public User User { get; private set; }
        public GameTypes GameType { get; private set; }

        public Action RefreshViewTest { get; private set; }

        private void SetGameData(IGameData value)
        {
            game.PlayerTurn = value.PlayerTurn;
            game.PlayerList = value.PlayerList;
            //game.Board.FieldList = value.Board.FieldList;
            //Test, chodzi o to aby nie przypisywac wartości na nowe tylko mapować dane. Dzięki temu powino się uniknąc problemów

            foreach(var fieldValue in value.Board.FieldList)
            {
                var field = game.Board.FieldList.First(f => f.ID == fieldValue.ID);
                field.Heigh = fieldValue.Heigh;
                field.Width = fieldValue.Width;
                field.Pawn = fieldValue.Pawn;
            }

        }

        public GameClient(GameClientBulider bulider)
        {
            this.gameServer = new GameServer(bulider.ServerConnector);
            this.message = bulider.Message;
            this.User = bulider.User;
            this.game = bulider.Game;
            this.GameType = bulider.GameType;
            this.RefreshViewTest = bulider.RefreshViewAction;
        }

        public async Task SearchOpponentAsync()
        {
            try
            {
                var search = this.gameServer.SearchOpponentAsync(GameType, User.UserId);
                var respons = await search;

                if (respons.Status != ServiceResponseStatus.Ok)
                {
                    this.message(respons.Messages);
                    return;
                }

                var playerList = respons.Match.MatchUsers.Select<MatchUser, IPlayer>(s => new Player { Name = s.User.Name, ID = s.User.UserId }).ToList();
                this.game.StartGame(playerList);

                this.MatchData = respons.Match;

                isOpponentSearchComplited = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task PlayMatchAsync()
        {
            try
            {
                if(!isOpponentSearchComplited)
                {
                    var message = new Dictionary<string, string>();
                    message.Add("SearchOpponentNotUse", "Nie wykonano operacji szukania");
                    this.message(message);
                    return;
                }

                await this.gameServer.PlayMatchConnect(PlayMatchResponsAction, MatchData.MatchId);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public void CancelSearchOpponent()
        {
            this.gameServer.CancelSearchOpponent(User.UserId);
        }

        public IEnumerable<IField> PawnWherCanMove(IField field)
        {
            return this.game.PawnWherCanMove(field);
        }

        public void PawnMove(IField fieldOld, IField fieldNew)
        {
            bool isPlayerTurn = User.UserId == game.PlayerTurn.ID;
            if (!isPlayerTurn)
            {
                return;
            }

            this.game.PawnMove(fieldOld, fieldNew); 

            GamePlay gamePlay = new GamePlay { GameData = GameData, Match = MatchData };
            this.gameServer.PlayMatchSend(new PlayMatch{GamePlay = gamePlay, UserId = User.UserId });
        }

        private void PlayMatchResponsAction(GamePlay gamePlay)
        {
            //Co tutaj zrobić?!
            //Nie mapowa modele tylko zrobić poprawne przepisywanie
            //Czyli szukamy wszystkich pionków i ustawiamy je na nowych polach
            //Bez żadnej zmiany modeli. I tak z pozostałymi kwestiami
            MatchData = gamePlay.Match;
            map(gamePlay.GameData);
            RefreshViewTest();
            game.CheckGameStatus();


            /*
            //Pytanie, tylko gra czy także widok?
            GameData = gamePlay.GameData;
            MatchData = gamePlay.Match;
            //Sprawdzić czy jest wygrana/przegrana itp...
            //Albo request niech przekazuje informacje o stanie gry?
            RefreshViewTest();

            //Dwa podejścia
            //1 = po każdym requescie, sprawdzamy jak wyglada status gry, czyli czy jest koniec (szachmat) itp.
            //2 = przesyłamy w requescie jaki jest status gry i go przekazujemy

            game.CheckGameStatus(); 
            */
        }

        private void map(IGameData data)
        {
            GameData.Turn = data.Turn;
            GameData.PlayerTurn = GameData.PlayerList.First(w => w.ID == data.PlayerTurn.ID);
            GameData.PawnHistoriesList = data.PawnHistoriesList;

            List<IPawn> pawnList = GameData.Board.FieldList.Where(w => w.Pawn != null).Select(s => s.Pawn).ToList();

            //Podejście 1, łatwe, aktualizacja całgo pola
            foreach(var field in GameData.Board.FieldList)
            {
                var fieldRespons = data.Board.FieldList.First(f => f.ID == field.ID);
                bool isNoChange = (field.Pawn == null && fieldRespons.Pawn == null) || (field.Pawn?.ID == fieldRespons.Pawn?.ID);
                if (!isNoChange)
                {
                    field.Pawn = fieldRespons.Pawn == null ? null : pawnList.First(f => f.ID == fieldRespons.Pawn.ID);
                }

            }

            //GameData.Board.FieldList
            /*
            var pawnFieldRespons = data.Board.FieldList.Where(w => w.Pawn != null).Select(s => new { FieldId = s.ID, PawnID = s.Pawn.ID});
            var pawnField = GameData.Board.FieldList.Where(w => w.Pawn != null).Select(s => new { FieldId = s.ID, PawnID = s.Pawn.ID });
            var joinPawn = pawnField.Join()
            */
        }
    }
}
