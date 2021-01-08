using Microsoft.EntityFrameworkCore;

namespace Roman.Ambinder.Storage.Impl.EntityFrameworkCore.Facilities.Impl.Test.Entities
{
    public class MyDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            const string connectionString = "data source=.\\SQLEXPRESS;initial catalog=CompositeKeyTest;integrated security=SSPI";
            optionsBuilder.UseSqlServer(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SameValueTypeComposedKeysEntity>()
                .HasKey(c => new { c.Key1, c.Key2, c.Key3 });

            modelBuilder.Entity<DifferentValueTypesComposedKeysEntity>()
             .HasKey(c => new { c.Key1, c.Key2, c.Key3 });

            modelBuilder.Entity<SameRefTypeKeysComposedEntity>()
             .HasKey(c => new { c.Key1, c.Key2, c.Key3 });
        }

        public DbSet<SameValueTypeComposedKeysEntity> CompositeValueKeyItems { get; set; }

        public DbSet<SameRefTypeKeysComposedEntity> CompositeRefTypeKeyItems { get; set; }

        public DbSet<SingleValueTypeKeyEntity> SingleValueTypeKeyItems { get; set; }

        public DbSet<SingleRefTypeKeyEntity> SingleRefTypeKeyItems { get; set; }
    }
}