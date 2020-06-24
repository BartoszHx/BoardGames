using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BoardGameDatabase.Interfaces.Services;

namespace BoardGameDatabase.Interfaces
{
    public interface IBoardGameServices : IDisposable
    {
        IUserService UserService { get; }
        IMatchService MatchService { get; }
        bool IsTransactionScope { get; }
        void TransactionCommit();
        void TransactionRoleback();
    }
}
