using Roman.Ambinder.DataTypes.OperationResults;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Roman.Ambinder.Storage.Common.Interfaces.Common.UnitOfWork
{
    public interface ILocalChangesStoreFor<TKey, TEntity> :
        IRepositoryGetOperationsFor<TKey, TEntity>
        where TEntity : class, new()
    {
        OperationResult TryAdd(TEntity newEntity);

        Task<OperationResult> TryUpdateAsync(TKey key,
            Action<TEntity> updateAction,
            CancellationToken cancellation = default);

        Task<OperationResult> TryRemoveAsync(TKey key,
            CancellationToken cancellationToken = default);
    }
}