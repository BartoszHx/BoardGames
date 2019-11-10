namespace BoardGameDatabase.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.GameType",
                c => new
                    {
                        GameTypeId = c.Int(nullable: false),
                        Name = c.String(maxLength: 50, unicode: false),
                    })
                .PrimaryKey(t => t.GameTypeId)
                .Index(t => t.Name, unique: true);
            
            CreateTable(
                "dbo.Match",
                c => new
                    {
                        MatchId = c.Int(nullable: false, identity: true),
                        GameTypeId = c.Int(nullable: false),
                        DateStart = c.DateTime(nullable: false),
                        DateEnd = c.DateTime(),
                        GameData = c.String(),
                    })
                .PrimaryKey(t => t.MatchId)
                .ForeignKey("dbo.GameType", t => t.GameTypeId, cascadeDelete: true)
                .Index(t => t.GameTypeId);
            
            CreateTable(
                "dbo.MatchUser",
                c => new
                    {
                        MatchUserId = c.Int(nullable: false, identity: true),
                        MatchId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                        MatchResultId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.MatchUserId)
                .ForeignKey("dbo.Match", t => t.MatchId, cascadeDelete: true)
                .ForeignKey("dbo.MatchResult", t => t.MatchResultId, cascadeDelete: true)
                .ForeignKey("dbo.User", t => t.UserId, cascadeDelete: true)
                .Index(t => t.MatchId)
                .Index(t => t.UserId)
                .Index(t => t.MatchResultId);
            
            CreateTable(
                "dbo.MatchResult",
                c => new
                    {
                        MatchResultId = c.Int(nullable: false),
                        Name = c.String(maxLength: 50, unicode: false),
                    })
                .PrimaryKey(t => t.MatchResultId)
                .Index(t => t.Name, unique: true);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Email = c.String(maxLength: 100, unicode: false),
                        Password = c.String(maxLength: 48),
                    })
                .PrimaryKey(t => t.UserId)
                .Index(t => t.Email, unique: true);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MatchUser", "UserId", "dbo.User");
            DropForeignKey("dbo.MatchUser", "MatchResultId", "dbo.MatchResult");
            DropForeignKey("dbo.MatchUser", "MatchId", "dbo.Match");
            DropForeignKey("dbo.Match", "GameTypeId", "dbo.GameType");
            DropIndex("dbo.User", new[] { "Email" });
            DropIndex("dbo.MatchResult", new[] { "Name" });
            DropIndex("dbo.MatchUser", new[] { "MatchResultId" });
            DropIndex("dbo.MatchUser", new[] { "UserId" });
            DropIndex("dbo.MatchUser", new[] { "MatchId" });
            DropIndex("dbo.Match", new[] { "GameTypeId" });
            DropIndex("dbo.GameType", new[] { "Name" });
            DropTable("dbo.User");
            DropTable("dbo.MatchResult");
            DropTable("dbo.MatchUser");
            DropTable("dbo.Match");
            DropTable("dbo.GameType");
        }
    }
}
