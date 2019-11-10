using BoardGameDatabase.Models;
using BoardGameDatabase.Models.Entites;
using BoardGameDatabase.Validations;

namespace BoardGameDatabase.Interfaces.Validators
{
    internal interface IMatchServiceValidation
    {
        //void SetDbContext(IBoardGameDbContext context);

        ValidationResult Create(CreateMatch create);

        ValidationResult Update(Match match);
    }
}
