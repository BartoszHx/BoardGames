using System;
using System.Data.Entity;

using BoardGameDatabase.Interfaces;
using BoardGameDatabase.Interfaces.Services;
using DbContexts;

using Ninject.Parameters;

namespace BoardGameDatabase.Services
{
    internal class BoardGameDatabaseUnitOfWork : IBoardGameUnitOfWork
    {
        private readonly IBoardGameDbContext context;
        private readonly DbContextTransaction contextTransaction;

        public bool IsTransactionScope { get; private set; }

        private IUserService userService;
        public IUserService UserService => Creator(ref userService);

        private IMatchService matchService;
        public IMatchService MatchService => Creator(ref matchService);

        internal BoardGameDatabaseUnitOfWork(IBoardGameDbContext bdContext, bool withTransaction = false)
        {
            this.context = bdContext;
            if (withTransaction)
            {
                IsTransactionScope = true;
                contextTransaction = this.context.BeginTransaction();
            }
        }

        private T Creator<T>(ref T service) where T : class
        {
            return service ?? (service = StaticKernel.Get<T>(new ConstructorArgument("context", this.context)));
        }

        public void Dispose()
        {
            this.context.Dispose();
            this.contextTransaction?.Dispose();
        }

        public void TransactionCommit()
        {
            if (this.contextTransaction == null)
            {
                throw new Exception("Transaction scope is not set");
            }
            this.contextTransaction.Commit();
        }

        public void TransactionRoleback()
        {
            if (this.contextTransaction == null)
            {
                throw new Exception("Transaction scope is not set");
            }
            this.contextTransaction.Rollback();
        }
    }
}
