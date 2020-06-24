using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BoardGameDatabase.Interfaces;
using BoardGameDatabase.Services;
using Ninject.Activation;

namespace BoardGameDatabase.Configurations.Providers
{
    public class BoardGameServiceProvider : Provider<IBoardGameServices>
    {
        protected override IBoardGameServices CreateInstance(IContext context)
        {
            bool withTransaction = (bool)context.Parameters.First(f => f.Name == "withTransaction").GetValue(context, null);
            return new BoardGameDatabaseUnitOfWork(StaticKernel.Get<IBoardGameDbContext>(), withTransaction);
        }
    }
}
