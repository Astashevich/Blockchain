using Microsoft.EntityFrameworkCore;

namespace Blockchain.DbContexts
{
    public class BlockchainDbContext : DbContext
    {
        public DbSet<Block> Blocks { get; set; }

        public BlockchainDbContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=Blockchain;Trusted_Connection=True;");
        }
    }
}
