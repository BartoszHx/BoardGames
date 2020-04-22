using BoardGameDatabase.Buliders;
using BoardGameDatabase.Interfaces;
using BoardGameDatabase.Interfaces.Buliders;
using BoardGameDatabase.Interfaces.Services;
using BoardGameDatabase.Interfaces.Validators;
using BoardGameDatabase.Models.Entites;
using BoardGameDatabase.Services;
using BoardGameDatabase.Validations;
using DbContexts;
using Ninject.Extensions.Factory;
using Ninject.Modules;

namespace BoardGameDatabase.Configurations
{
    public class Binding : NinjectModule
    {
        public override void Load()
        {
            this.Bind<IBoardGameDbContext>().To<BoardGameDbContext>();

            this.Bind<IUserService>().To<UserService>();
            this.Bind<IMatchService>().To<MatchService>();

            //this.Bind<IBoardGameService>().To<ServiceUnitOfWork>();
            this.Bind<IBoardGameUnitOfWork>().ToProvider(new Providers.BoardGameServiceProvider());
            this.Bind<IBoardGameUnitOfWorkBulider>().To<Buliders.BoardGameUnitOfWorkBulider>();

            this.Bind<IMatchServiceValidation>().To<MatchServiceValidation>();
            this.Bind<IMatchServiceValidationBulider>().To<MatchServiceValidationBulider>();
            this.Bind<IPasswordValidation>().To<PasswordValidation>();
            this.Bind<IUserServiceValidation>().To<UserServiceValidation>();

            this.Bind<IValidationModel<Match>>().To<MatchValidation>(); //Hmm raczej to zmienić, aby było na odwrót?
            this.Bind<IValidationModel<User>>().To<UserValidation>();

            this.Bind<IValidationMessage>().To<ValidationMessage>();
        }
    }
}
