using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BoardGameDatabase.Models.Entites;

namespace BoardGameDatabase.Interfaces
{
    internal interface IBoardGameDbContext : IDisposable
    {
        DbSet<GameType> GameTypes { get; set; }
        DbSet<Match> Matches { get; set; }
        DbSet<MatchResult> MatchResults { get; set; }
        DbSet<MatchUser> MatchUsers { get; set; }
        DbSet<User> Users { get; set; }
        int SaveChanges();
        DbContextTransaction BeginTransaction();
    }
}
