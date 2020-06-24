using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardGameDatabase.Interfaces
{
    public interface IBoardGameServiceBulider
    {
        IBoardGameServiceBulider SetTransactionScope();
        IBoardGameServices Bulid();
    }
}
