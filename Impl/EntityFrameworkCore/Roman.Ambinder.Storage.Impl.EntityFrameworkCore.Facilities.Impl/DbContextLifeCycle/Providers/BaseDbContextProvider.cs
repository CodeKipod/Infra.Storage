using System;
using System.Threading.Tasks;
using Roman.Ambinder.DataTypes.OperationResults;
using Roman.Ambinder.Storage.Impl.EntityFrameworkCore.Facilities.Common.Interfaces;

namespace Roman.Ambinder.Storage.Impl.EntityFrameworkCore.Facilities.Impl.DbContextLifeCycle.Providers
{
    public abstract class BaseDbContextProvider : IDbContextProvider
    {
        protected readonly IDbContextFactory DbContextFactory;

        protected BaseDbContextProvider(IDbContextFactory dbContextFactory,
            bool disposeAfterUsage)
        {
            DbContextFactory = dbContextFactory
                ?? throw new ArgumentException(nameof(dbContextFactory));

            DisposeAfterUsage = disposeAfterUsage;
        }

        public abstract Microsoft.EntityFrameworkCore.DbContext Get();

        public async Task<OperationResult> TryMigrateAsync(bool recreate = false)
        {
            try
            {
                await using var context = DbContextFactory.Create();

                if (recreate)
                    await context.Database.EnsureDeletedAsync().ConfigureAwait(false);

                // ReSharper disable once UnusedVariable
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