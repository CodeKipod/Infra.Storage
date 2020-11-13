using Microsoft.EntityFrameworkCore;
using Roman.Ambinder.DataTypes.OperationResults;
using Roman.Ambinder.Storage.Impl.EntityFrameworkCore.Facilities.Common;
using System;
using System.Threading.Tasks;

namespace Roman.Ambinder.Storage.Impl.EntityFrameworkCore.Facilities.Impl
{
    public class DependencyInjectionDbContextProviderOf<TDbContext> :
        IDbContectProviderOf<TDbContext>
        where TDbContext : DbContext
    {
        private readonly DbContext _dbContext;

        public bool DisposeAfterUsage => false;

        public DependencyInjectionDbContextProviderOf(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public DbContext Get() => _dbContext;

        public async Task<OperationResult> TryMigrateAsync(bool recreate = false)
        {
            try
            {
                if (recreate)
                    await _dbContext.Database.EnsureDeletedAsync().ConfigureAwait(false);

                var newDbCreated = await _dbContext.Database.EnsureCreatedAsync();
                //if (newDbCreated)
                //await context.Database.MigrateAsync();

                return OperationResult.Successful;
            }
            catch (Exception ex) { return ex.AsFailedOpRes(); }
        }
    }
}