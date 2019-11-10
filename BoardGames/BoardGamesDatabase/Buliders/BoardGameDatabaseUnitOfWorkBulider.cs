using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BoardGameDatabase.Interfaces;
using Ninject.Parameters;

namespace BoardGameDatabase.Buliders
{
    public class BoardGameUnitOfWorkBulider : IBoardGameUnitOfWorkBulider
    {
        private bool withTransaction;

        public IBoardGameUnitOfWork Bulid()
        {
            return StaticKernel.Get<IBoardGameUnitOfWork>(new ConstructorArgument("withTransaction", withTransaction, true));
        }

        public IBoardGameUnitOfWorkBulider SetTransactionScope()
        {
            withTransaction = true;
            return this;
        }
    }
}
