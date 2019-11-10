using BoardGameDatabase.Models.Entites;
using BoardGamesServerIntegrationTest.Contextx;

namespace BoardGamesServerIntegrationTest.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<BoardGameDbContextTest>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
            ContextKey = "BoardGameDatabaseTest";
        }

        protected override void Seed(BoardGameDbContextTest context)
        {
            context.GameTypes.AddOrUpdate(new GameType() { GameTypeId = 1, Name = "Chess" }); //Szachy
            context.GameTypes.AddOrUpdate(new GameType() { GameTypeId = 2, Name = "Checkers"});

            context.MatchResults.AddOrUpdate(new MatchResult() { MatchResultId = (int)BoardGameDatabase.Enums.MatchResults.InProgress, Name = "InProgress" });
            context.MatchResults.AddOrUpdate(new MatchResult() { MatchResultId = (int)BoardGameDatabase.Enums.MatchResults.Disconnected, Name = "Disconnected" });
            context.MatchResults.AddOrUpdate(new MatchResult() { MatchResultId = (int)BoardGameDatabase.Enums.MatchResults.Draw, Name = "Draw" });
            context.MatchResults.AddOrUpdate(new MatchResult() { MatchResultId = (int)BoardGameDatabase.Enums.MatchResults.Loss, Name = "Loss" });
            context.MatchResults.AddOrUpdate(new MatchResult() { MatchResultId = (int)BoardGameDatabase.Enums.MatchResults.Win, Name = "Win" });
        }
    }
}
