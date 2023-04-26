using client.Model;
using Microsoft.EntityFrameworkCore;

namespace DbPgSql
{
    public class pgsql : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql("Host=localhost;Database=api-cafeteria;User Id=postgres;Port=5432;Password=Syx@2022;");

        public DbSet<Client> Clients { get; set; }
        public DbSet<Seller> Sellers { get; set; }
        public DbSet<Coffee> Coffee { get; set; }
    }
}