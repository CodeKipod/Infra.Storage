using System;
using System.Threading;
using System.Threading.Tasks;
using Roman.Ambinder.DataTypes.OperationResults;

namespace Roman.Ambinder.Storage.Common.Interfaces.NotUsed
{
    public interface ICommandRepositoryFor<in TKey, TEntity>
    {
        Task<OperationResult> TryAddAsync(TEntity entity,
            CancellationToken cancellationToken = default);

        Task<OperationResult> TryUpdateAsync(TKey key,
            Action<TEntity> updateAction,
            CancellationToken cancellationToken = default);

        Task<OperationResult> TryRemoveAsync(TKey key,
            CancellationToken cancellationToken = default);
    }
}