using BoardGameDatabase.Models;
using BoardGameDatabase.Models.Entites;
using BoardGameDatabase.Services.Response;
using System;

namespace BoardGameDatabase.Interfaces.Services
{
    public interface IMatchService
    {
        MatchServiceResponse Create(CreateMatch create);
        ServiceResponse Update(Match match);
    }
}
