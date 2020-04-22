using System;
using System.Collections.Generic;
using System.Text;
using BoardGamesClient.Clients;
using BoardGamesClient.Interfaces;
using BoardGamesClient.Models;
using BoardGamesClient.Servers;
using BoardGamesShared.Enums;
using BoardGamesShared.Interfaces;

namespace BoardGamesClient.Buliders
{
    internal class GameClientBulider : IGameClientBulider
    {
        internal Action<Dictionary<string, string>> Message { get; private set; }
        internal ServerConnector ServerConnector { get; private set; }
        internal User User { get; private set; }
        internal IGame Game { get; private set; }
        internal GameTypes GameType { get; private set; }
        internal Action RefreshViewAction { get; private set; }

        public GameClientBulider()
        {
            ServerConnector = Configurations.ServerConfiguration.GetServerConnector();
        }

        public IGameClientBulider SetActionMessage(Action<Dictionary<string, string>> message)
        {
            this.Message = message;
            return this;
        }

        public IGameClientBulider SetUser(User user)
        {
            this.User = user;
            return this;
        }

        public IGameClientBulider SetChessGame(Action<MessageContents> alert, Func<IEnumerable<PawChess>, PawChess> chosePawUpgrade)
        {
            GameType = GameTypes.Chess;
            Game = new BoardGames.Buliders.ChessGameBulider()
                .SetAlertMessage(alert)
                .SetChosePawUpgradeFunction(chosePawUpgrade)
                .Bulid();
            return this;
        }

        public IGameClientBulider SetCheckerGame(Action<MessageContents> alert)
        {
            GameType = GameTypes.Checkers;
            Game = new BoardGames.Buliders.CheckerGameBulider()
                .SetAlertMessage(alert)
                .Bulid();
            return this;
        }

        public IGameClient Bulid()
        {
            this.validBuild();
            //Tutaj ma być Ninject
            return new GameClient(this);
        }

        private void validBuild()
        {
            if (Message == null)
            {
                throw new Exception("Message is not set");
            }

            if (User == null)
            {
                throw  new Exception("User is not set");
            }

            if(Game == null)
            {
                throw new Exception("Game is not set");
            }

            if(RefreshViewAction == null)
            {
                throw new Exception("RefreshViewAction is not set");
            }
        }

        public IGameClientBulider SetRereshViewAction(Action refreshView)
        {
            this.RefreshViewAction = refreshView;
            return this;
        }
    }
}
