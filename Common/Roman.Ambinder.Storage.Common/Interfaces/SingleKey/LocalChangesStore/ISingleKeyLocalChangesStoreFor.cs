using System;
using System.Threading;
using System.Threading.Tasks;
using Roman.Ambinder.DataTypes.OperationResults;

namespace Roman.Ambinder.Storage.Common.Interfaces.SingleKey.LocalChangesStore
{
    public interface ISingleKeyLocalChangesStoreFor<in TKey, TEntity> :
        ISingleKeyReadonlyRepositoryFor<TKey, TEntity>
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
