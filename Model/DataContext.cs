using Microsoft.EntityFrameworkCore;
using System.IO;

namespace CoronaInfoAppCore.Model
{
    public class DataContext : DbContext
    {
        public DbSet<Country> Countries { get; set; }
        public DbSet<Case> Cases { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(File.ReadAllText(@"..\..\..\Model\ConnectionString.txt"));
        }
    }
}
