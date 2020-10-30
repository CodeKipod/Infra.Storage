using Microsoft.EntityFrameworkCore;
using Roman.Ambinder.Storage.Impl.EntityFrameworkCore.Tests.Entities;

namespace Roman.Ambinder.Storage.Impl.EntityFrameworkCore.Tests.Helpers
{
    public class PeopleDbContext : DbContext
    {
        public PeopleDbContext()
        {
            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            const string connectionString = "data source=.\\SQLEXPRESS;initial catalog=TestPeopleDb2;integrated security=SSPI";
            optionsBuilder.UseSqlServer(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Person> People { get; set; }
    }
}