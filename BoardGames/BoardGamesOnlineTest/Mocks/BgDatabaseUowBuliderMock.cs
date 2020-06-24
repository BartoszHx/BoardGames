using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BoardGameDatabase.Interfaces;

namespace BoardGamesOnlineTest.Mocks
{
    //Straszna nazwa, pomyślec nad ich zmianą
    internal class BgDatabaseUowBuliderMock : IBoardGameServiceBulider
    {
        private IBoardGameDbContext context;
        private bool withTransactionScope;
        public BgDatabaseUowBuliderMock(IBoardGameDbContext context)
        {
            this.context = context;
        }

        public IBoardGameServices Bulid()
        {
            return new BoardGameDatabase.Services.BoardGameDatabaseUnitOfWork(context, withTransactionScope);
        }

        public IBoardGameServiceBulider SetTransactionScope()
        {
            withTransactionScope = true;
            return this;
        }
    }
}
