using BoardGameDatabase.Interfaces;
using BoardGameDatabase.Models.Entites;
using System.Data.Entity;

namespace DbContexts
{
    internal class BoardGameDbContext : DbContext, IBoardGameDbContext
    {
        public BoardGameDbContext()
            : base("BoardGameDbContext")
        {
        }

	    public virtual DbSet<GameType> GameTypes { get; set; }
	    public virtual DbSet<Match> Matches { get; set; }
	    public virtual DbSet<MatchResult> MatchResults { get; set; }
	    public virtual DbSet<MatchUser> MatchUsers { get; set; }
	    public virtual DbSet<User> Users { get; set; }

        public DbContextTransaction BeginTransaction()
        {
            return Database.BeginTransaction();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<System.Data.Entity.ModelConfiguration.Conventions.ManyToManyCascadeDeleteConvention>();
            modelBuilder.Entity<User>().Property(p => p.Password); //Mapowanie internal property

            base.OnModelCreating(modelBuilder); 
        }
    }
}