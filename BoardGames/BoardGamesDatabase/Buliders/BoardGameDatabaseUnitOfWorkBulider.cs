using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BoardGameDatabase.Interfaces;
using Ninject.Parameters;

namespace BoardGameDatabase.Buliders
{
    public class BoardGameUnitOfWorkBulider : IBoardGameServiceBulider
    {
        private bool withTransaction;

        public IBoardGameServices Bulid()
        {
            return StaticKernel.Get<IBoardGameServices>(new ConstructorArgument("withTransaction", withTransaction, true));
        }

        public IBoardGameServiceBulider SetTransactionScope()
        {
            withTransaction = true;
            return this;
        }
    }
}
