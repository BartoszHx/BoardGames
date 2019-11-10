using BoardGameDatabase.Extensions;
using BoardGameDatabase.Interfaces.Validators;
using BoardGameDatabase.Models;
using BoardGameDatabase.Models.Entites;
using BoardGameDatabase.Operations;
using DbContexts;
using System.Linq;

using BoardGameDatabase.Enums;
using BoardGameDatabase.Interfaces;

namespace BoardGameDatabase.Validations
{
    internal class MatchServiceValidation : IMatchServiceValidation
    {
        private IValidationModel<Match> matchValidation;
        private IBoardGameDbContext context;

        public MatchServiceValidation(IValidationModel<Match> matchValid, IBoardGameDbContext context)
        {
            this.matchValidation = matchValid;
            this.context = context;
        }

        public ValidationResult Create(CreateMatch create)
        {
            ValidationResult validation = new ValidationResult();
            /*
            if (!JsonOperation.IsJsonFormat(create.GameData))
            {
                validation.AddError(ValidationKey.MatchNoJsonFormatGameData);
            }*/

            if (create.UserIdList == null)
            {
                validation.AddError(ValidationKey.MatchNoUsers);
            }
            else
            {
                foreach (var userId in create.UserIdList)
                {
                    if (!context.Users.Any(u => u.UserId == userId))
                    {
                        validation.AddError(ValidationKey.MatchNoUser, userId.ToString());
                    }
                }
            }

            return validation;
        }

        public ValidationResult Update(Match match)
        {
            if (match != null && match.MatchId == 0)
            {
                ValidationResult validation = new ValidationResult();
                validation.AddError(ValidationKey.MatchNoId);
                return validation;
            }
            else
            {
                return this.matchValidation.Validate(match);
            }
        }
    }
}
