using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BoardGameDatabase.Interfaces;

namespace BoardGamesOnlineTest.Mocks
{
    //Straszna nazwa, pomyślec nad ich zmianą
    internal class BgDatabaseUowBuliderMock : IBoardGameUnitOfWorkBulider
    {
        private IBoardGameDbContext context;
        private bool withTransactionScope;
        public BgDatabaseUowBuliderMock(IBoardGameDbContext context)
        {
            this.context = context;
        }

        public IBoardGameUnitOfWork Bulid()
        {
            return new BoardGameDatabase.Services.BoardGameDatabaseUnitOfWork(context, withTransactionScope);
        }

        public IBoardGameUnitOfWorkBulider SetTransactionScope()
        {
            withTransactionScope = true;
            return this;
        }
    }
}
