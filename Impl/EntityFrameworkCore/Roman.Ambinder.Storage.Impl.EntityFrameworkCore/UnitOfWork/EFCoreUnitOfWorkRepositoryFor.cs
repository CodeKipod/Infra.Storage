using Roman.Ambinder.DataTypes.OperationResults;
using Roman.Ambinder.Storage.Impl.EntityFrameworkCore.Facilities.Impl;
using System;
using System.Threading;
using System.Threading.Tasks;
using Roman.Ambinder.Storage.Common.Interfaces.SingleKey.LocalChangesStore;
using Roman.Ambinder.Storage.Common.Interfaces.SingleKey.UnitOfWork;
using Roman.Ambinder.Storage.Impl.EntityFrameworkCore.Facilities.Common.Interfaces;

namespace Roman.Ambinder.Storage.Impl.EntityFrameworkCore.UnitOfWork
{
    public class EFCoreUnitOfWorkRepositoryFor<TKey, TEntity> :
        IUnitOfWorkSingleKeyRepositoryFor<TKey, TEntity>
        where TEntity : class, new()
    {
        protected readonly DbContextSafeUsageVisitor DbContextSafeUsageVisitor;

        public EFCoreUnitOfWorkRepositoryFor(
            IDbContextProvider dbContextProvider)
        {
            dbContextProvider = dbContextProvider
                ?? throw new ArgumentNullException(nameof(dbContextProvider));

            DbContextSafeUsageVisitor = new DbContextSafeUsageVisitor(dbContextProvider);

            LocalChangesRepository =
                new DbContextSingleKeyLocalStoreFor<TKey, TEntity>(DbContextSafeUsageVisitor);
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

        public ISingleKeyLocalChangesStoreFor<TKey, TEntity> LocalChangesRepository { get; }
    }
}
