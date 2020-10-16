using Roman.Ambinder.DataTypes.OperationResults;
using Roman.Ambinder.Storage.Common.Interfaces.SingleKey;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Roman.Ambinder.Storage.Common.Interfaces.LocalChangesStore
{
    public interface ISingleKeyLocalChangesStoreFor<TKey, TEntity> :
        ISingleKeyReadonlyRepositoryFor<TKey, TEntity>
        where TEntity : class, new()
    {
        OperationResult TryAdd(TEntity newEntity);

        //OperationResult TryAddRange(IEnumerable<TEntity> newEntities);

        Task<OperationResult> TryUpdateAsync(TKey key,
            Action<TEntity> updateAction,
            CancellationToken cancellation = default);
        Task<OperationResult> TryRemoveAsync(TKey key,
            CancellationToken cancellationToken = default);
    }
}
