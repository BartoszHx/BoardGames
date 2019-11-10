using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BoardGames.Factories;
using BoardGamesClient.Buliders;
using BoardGamesClient.Configurations.AutoMappers;
using BoardGamesClient.Interfaces;
using BoardGamesClient.Models;
using BoardGamesClient.Responses;
using BoardGamesClient.Servers;
using BoardGamesShared.Enums;
using BoardGamesShared.Interfaces;

namespace BoardGamesClient.Clients
{
    internal class GameClient : IGameClient
    {
        private GameServer gameServer;

        private IGame game;

        private Action<Dictionary<string, string>> message;

        public GamePlay GamePlay { get; private set; }

        public User User { get; private set; }

        public GameClient(GameClientBulider bulider)
        {
            this.gameServer = new GameServer(bulider.ServerConnector);
            this.message = bulider.Message;
            this.User = bulider.User;
        }

        public async Task SearchOpponentAsync(GameTypes gametype)
        {
            try
            {
                var search = this.gameServer.SearchOpponentAsync(gametype, User.UserId);
                var respons = await search;

                if (respons.Status != ServiceResponseStatus.Ok)
                {
                    this.message(respons.Messages);
                    return;
                }

                //W teori działa

                this.GamePlay = new GamePlay();
                this.game = GameFactory.Create(gametype);

                var playerList = respons.Match.MatchUsers.Select<User, IPlayer>(s => new Player { Name = s.Name, ID = s.UserId }).ToList();
                this.game.StartGame(playerList);

                this.GamePlay.Game = this.game;
                this.GamePlay.Match = respons.Match;

                await this.gameServer.PlayMatchConnect(new PlayMatch { GamePlay = this.GamePlay, UserId = User.UserId });
            }
            catch (Exception ex)
            {
                //Póżniej usunąć try catch
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
            this.game.PawnMove(fieldOld, fieldNew);
            //if(this.game.a)
            this.GamePlay.Game = this.game;

            this.gameServer.PlayMatchSend(new PlayMatch{GamePlay = this.GamePlay, UserId = User.UserId });
        }
    }
}
