using Microsoft.EntityFrameworkCore;

namespace Roman.Ambinder.Storage.Impl.EntityFrameworkCore.Tests.Common
{
    public class SingleEntityTypeDbContextOf<TEntity> : DbContext
        where TEntity : class, new()
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectionString =
                $"data source=.\\SQLEXPRESS;initial catalog={typeof(TEntity).Name};integrated security=SSPI";
            optionsBuilder.UseSqlServer(connectionString);
        }

        public DbSet<TEntity> Items { get; set; }
    }
}
