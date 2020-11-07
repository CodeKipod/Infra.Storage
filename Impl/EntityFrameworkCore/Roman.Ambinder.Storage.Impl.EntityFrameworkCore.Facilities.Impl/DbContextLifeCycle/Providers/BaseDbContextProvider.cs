using Roman.Ambinder.DataTypes.OperationResults;
using Roman.Ambinder.Storage.Impl.EntityFrameworkCore.Facilities.Common;
using System;
using System.Threading.Tasks;

namespace Roman.Ambinder.Storage.Impl.EntityFrameworkCore.Facilities.Impl
{
    public abstract class BaseDbContextProvider : IDbContextProvider
    {
        protected readonly IDbContextFactory _dbContextFactory;

        protected BaseDbContextProvider(IDbContextFactory dbContextFactory,
            bool disposeAfterUsage)
        {
            _dbContextFactory = dbContextFactory
                ?? throw new ArgumentException(nameof(dbContextFactory));

            DisposeAfterUsage = disposeAfterUsage;
        }

        public abstract Microsoft.EntityFrameworkCore.DbContext Get();

        public async Task<OperationResult> TryMigrateAsync(bool recreate = false)
        {
            try
            {
                await using var context = _dbContextFactory.Create();

                if (recreate)
                    await context.Database.EnsureDeletedAsync().ConfigureAwait(false);

                var newDbCreated = await context.Database.EnsureCreatedAsync();
                //if (newDbCreated)
                //await context.Database.MigrateAsync();

                return OperationResult.Successful;
            }
            catch (Exception ex) { return ex.AsFailedOpRes(); }
        }

        public bool DisposeAfterUsage { get; }
    }
}