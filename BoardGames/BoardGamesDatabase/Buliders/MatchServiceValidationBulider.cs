using BoardGameDatabase.Interfaces;
using BoardGameDatabase.Interfaces.Buliders;
using BoardGameDatabase.Interfaces.Validators;
using Ninject.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardGameDatabase.Buliders
{
    internal class MatchServiceValidationBulider : IMatchServiceValidationBulider
    {
        private IBoardGameDbContext context;

        public IMatchServiceValidation Bulid()
        {
            return StaticKernel.Get<IMatchServiceValidation>(new ConstructorArgument("context", context, true));
        }

        public IMatchServiceValidationBulider SetContext(IBoardGameDbContext context)
        {
            this.context = context;
            return this;
        }
    }
}
