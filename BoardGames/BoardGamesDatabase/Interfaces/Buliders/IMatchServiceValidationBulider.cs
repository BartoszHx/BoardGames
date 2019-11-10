using BoardGameDatabase.Interfaces.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardGameDatabase.Interfaces.Buliders
{
    internal interface IMatchServiceValidationBulider
    {
        IMatchServiceValidation Bulid();
        IMatchServiceValidationBulider SetContext(IBoardGameDbContext context);
    }
}
