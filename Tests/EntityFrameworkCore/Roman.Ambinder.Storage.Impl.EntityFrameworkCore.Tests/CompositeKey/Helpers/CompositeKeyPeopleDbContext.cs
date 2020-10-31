using Microsoft.EntityFrameworkCore;
using Roman.Ambinder.Storage.Impl.EntityFrameworkCore.Tests.SingleKey.Entities;

namespace Roman.Ambinder.Storage.Impl.EntityFrameworkCore.Tests.CompositeKey.Helpers
{
    public class CompositeKeyPeopleDbContext : DbContext
    {
        public CompositeKeyPeopleDbContext()
        {
            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            const string connectionString = "data source=.\\SQLEXPRESS;initial catalog=CompositeKeyPeople;integrated security=SSPI";
            optionsBuilder.UseSqlServer(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CompsiteKeyPerson>()
                 .HasKey(c => new { c.Key1, c.Key2, c.Key3 });
        }

        public DbSet<CompsiteKeyPerson> People { get; set; }
    }
}