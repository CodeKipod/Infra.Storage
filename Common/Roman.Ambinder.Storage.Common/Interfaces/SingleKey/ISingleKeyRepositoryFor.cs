using System;
using System.Threading;
using System.Threading.Tasks;
using Roman.Ambinder.DataTypes.OperationResults;

namespace Roman.Ambinder.Storage.Common.Interfaces.SingleKey
{
    public interface ISingleKeyRepositoryFor<TKey, TEntity> :
        ISingleKeyReadonlyRepositoryFor<TKey, TEntity>
        where TEntity : class, new()
    {
        Task<OperationResultOf<TEntity>> TryAddAsync(
            Action<TEntity> initAction = null,
            CancellationToken cancellationToken = default);

        Task<OperationResultOf<TEntity>> TryAddAsync(TEntity newEntity,
            CancellationToken cancellationToken = default);

        Task<OperationResultOf<TEntity>> TryUpdateAsync(TKey key,
            Action<TEntity> updateAction,
            CancellationToken cancellationToken = default);

        Task<OperationResultOf<TEntity>> TryAddOrUpdateAsync(TKey key,
            Action<TEntity> updateOrInitAction,
            CancellationToken cancellationToken = default);

        Task<OperationResultOf<TEntity>> TryRemoveAsync(TKey key,
            CancellationToken cancellationToken = default);
    }
}