using Roman.Ambinder.DataTypes.OperationResults;
using Roman.Ambinder.Storage.Common.Interfaces.Common;
using Roman.Ambinder.Storage.Impl.EntityFrameworkCore.Facilities.Common;
using Roman.Ambinder.Storage.Impl.EntityFrameworkCore.Facilities.Impl;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Roman.Ambinder.Storage.Impl.EntityFrameworkCore.Common
{
    public abstract class BaseEFCoreUnitOfWorkRepositoryFor<TKey, TEntity> :
        IUnitOfWorkRepositoryFor<TKey, TEntity>
        where TEntity : class, new()
    {
        protected readonly DbContextSafeUsageVisitor DbContextSafeUsageVisitor;

        protected BaseEFCoreUnitOfWorkRepositoryFor(IDbContextProvider dbContextProvider)
        {
            dbContextProvider = dbContextProvider
                ?? throw new ArgumentNullException(nameof(dbContextProvider));

            Repository =
                new EFCoreRepositoryFor<TKey, TEntity>(dbContextProvider);

            DbContextSafeUsageVisitor = new DbContextSafeUsageVisitor(dbContextProvider);
        }

        public Task<OperationResult> TryCommitChangesAsync(
            CancellationToken cancellationToken = default)
        {
            return DbContextSafeUsageVisitor.TryUseAsync(async dbSession =>
            {
                var changes = await dbSession.SaveChangesAsync(cancellationToken)
                    .ConfigureAwait(false);

                return changes > 0 ?
                    OperationResult.Successful :
                    "No changes were made".AsFailedOpRes();
            });
        }

        public void Dispose() => DbContextSafeUsageVisitor.Dispose();

        public IRepositoryFor<TKey, TEntity> Repository { get; }
    }
}