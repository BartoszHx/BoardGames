using BoardGameDatabase.Extensions;
using BoardGameDatabase.Interfaces.Validators;
using BoardGameDatabase.Models.Entites;
using BoardGameDatabase.Operations;
using System;

using BoardGameDatabase.Enums;

namespace BoardGameDatabase.Validations
{
    internal class MatchValidation : IValidationModel<Match>
    {
        private ValidationResult valid;

        private Match match;

        public ValidationResult Validate(Match obj)
        {
            valid = new ValidationResult();
            match = obj;

            IsNull();
            if (!valid.IsSucces)
            {
                return valid;
            }

            IsGameData();
            IsCorrectDateStart();
            IsCorrectEndDate();

            return valid;
        }

        private void IsNull()
        {
            if (match == null)
            {
                valid.AddError(ValidationKey.IsNull);
                return;
            }

            if (match.MatchId == 0)
            {
                this.valid.AddError(ValidationKey.MatchNoId);
            }
        }

        private void IsGameData()
        {
            //Pomyśleć, jak na razie zezwalam na null w GameData
            if (string.IsNullOrEmpty(this.match.GameData))
            {
                return;
            }

            if (!JsonOperation.IsJsonFormat(match.GameData))
            {
                valid.AddError(ValidationKey.MatchNoJsonFormatGameData);
            }
        }

        private void IsCorrectDateStart()
        {
            if (this.match.DateStart == DateTime.MinValue)
            {
                this.valid.AddError(ValidationKey.MatchNullDateStart);
            }
        }

        private void IsCorrectEndDate()
        {
            if (this.match.DateEnd.HasValue && this.match.DateEnd.Value < this.match.DateStart)
            {
                this.valid.AddError(ValidationKey.MatchIncorectDateEnd);
            }
        }
    }
}
