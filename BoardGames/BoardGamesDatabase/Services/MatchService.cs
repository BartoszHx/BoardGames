using BoardGameDatabase.Enums;
using BoardGameDatabase.Interfaces;
using BoardGameDatabase.Interfaces.Services;
using BoardGameDatabase.Interfaces.Validators;
using BoardGameDatabase.Models;
using BoardGameDatabase.Models.Entites;
using BoardGameDatabase.Services.Response;
using System;
using System.Collections.Generic;
using System.Linq;

using BoardGameDatabase.Validations;
using BoardGameDatabase.Interfaces.Buliders;

namespace BoardGameDatabase.Services
{
    internal class MatchService : IMatchService
    {
        private IBoardGameDbContext context;
        private IMatchServiceValidation validator;

        public MatchService(IBoardGameDbContext context, IMatchServiceValidationBulider matchServiceValidationBulider)
        {
            this.context = context;
            validator = matchServiceValidationBulider
                .SetContext(context)
                .Bulid();
        }

        public MatchServiceResponse Create(CreateMatch create)
        {
            ValidationResult validate = validator.Create(create);
            if (!validate.IsSucces)
            {
                return new MatchServiceResponse(ServiceRespondStatus.Error, validate.ErrorList, null);
            }

            Match match = new Match();
            match.GameTypeId = (int)create.GameType;
            match.DateStart = DateTime.Now;
            match.GameData = null;
            match.MatchUsers = new List<MatchUser>();
            foreach (int user in create.UserIdList)
            {
                match.MatchUsers.Add(new MatchUser() { UserId = user, MatchResultId = (int)Enums.MatchResults.InProgress});
            }

            this.context.Matches.Add(match);
            this.context.SaveChanges();

            //Uzupełnić dane o Userze
            foreach (var matchUser in match.MatchUsers)
            {
                matchUser.User = this.context.Users.First(f=> f.UserId == matchUser.UserId); //Dopisać tą funkcję do servicu
            }


            return new MatchServiceResponse(match);
        }

        public ServiceResponse Update(Match match)
        {
            ValidationResult valid = this.validator.Update(match);

            if (!valid.IsSucces)
            {
                return new ServiceResponse(ServiceRespondStatus.Error, valid.ErrorList);
            }

            var matchDb = this.context.Matches.First(m => m.MatchId == match.MatchId);
            

            this.context.SaveChanges();
            return new ServiceResponse();
        }
    }
}
