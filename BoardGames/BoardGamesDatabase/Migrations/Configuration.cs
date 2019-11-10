using BoardGameDatabase.Models.Entites;
using System.Data.Entity.Migrations;

namespace BoardGameDatabase.Migrations
{


    internal sealed class Configuration : DbMigrationsConfiguration<DbContexts.BoardGameDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
            ContextKey = "BoardGameDatabase";
        }

        protected override void Seed(DbContexts.BoardGameDbContext context)
        {
            context.GameTypes.AddOrUpdate(new GameType() { GameTypeId = 2 });
            context.GameTypes.AddOrUpdate(new GameType() { GameTypeId = 1 });//Szachy

            context.MatchResults.AddOrUpdate(new MatchResult() { MatchResultId = (int)Enums.MatchResults.InProgress, Name = "InProgress" });
            context.MatchResults.AddOrUpdate(new MatchResult() { MatchResultId = (int)Enums.MatchResults.Disconnected, Name = "Disconnected" });
            context.MatchResults.AddOrUpdate(new MatchResult() { MatchResultId = (int)Enums.MatchResults.Draw, Name = "Draw" });
            context.MatchResults.AddOrUpdate(new MatchResult() { MatchResultId = (int)Enums.MatchResults.Loss, Name = "Loss" });
            context.MatchResults.AddOrUpdate(new MatchResult() { MatchResultId = (int)Enums.MatchResults.Win, Name = "Win" });
        }
    }
}
