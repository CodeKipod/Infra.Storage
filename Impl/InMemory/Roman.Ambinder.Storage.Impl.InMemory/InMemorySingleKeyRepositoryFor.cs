using Roman.Ambinder.DataTypes.OperationResults;
using Roman.Ambinder.Storage.Common.Interfaces.SingleKey;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Roman.Ambinder.Storage.Impl.InMemory
{
    public class InMemorySingleKeyRepositoryFor<TKey, TEntity> :
        InMemorySingleKeyReadonlyRepositoryFor<TKey, TEntity>,
        ISingleKeyRepositoryFor<TKey, TEntity>
        where TEntity : class, new()
    {
        private readonly Func<TEntity, TKey> _keyProvider;

        public InMemorySingleKeyRepositoryFor(Func<TEntity, TKey> keyProvider)
        {
            _keyProvider = keyProvider;
        }

        public Task<OperationResultOf<TEntity>> TryAddAsync(
            Action<TEntity> initAction = null,
            CancellationToken cancellationToken = default)
        {
            var newEntity = new TEntity();
            initAction?.Invoke(newEntity);

            return TryAddAsync(newEntity, cancellationToken);
        }

        public Task<OperationResultOf<TEntity>> TryAddAsync(
            TEntity newEntity,
            CancellationToken cancellationToken = default)
        {
            var key = _keyProvider(newEntity);
            var success = Store.TryAdd(key, newEntity);
            var opRes = success ? newEntity.AsSuccessfulOpRes() :
                $"Entity with '{key}' already exist".AsFailedOpResOf<TEntity>();

            return Task.FromResult(opRes);
        }

        public Task<OperationResultOf<TEntity>> TryAddOrUpdateAsync(TKey key,
            Action<TEntity> updateOrInitAction,
            CancellationToken cancellationToken = default)
        {
            if (Store.TryGetValue(key, out var existingValue))
            {
                updateOrInitAction(existingValue);
                return Task.FromResult(existingValue.AsSuccessfulOpRes());
            }

            var newEntity = new TEntity();
            updateOrInitAction(newEntity);
            Store.TryAdd(key, newEntity);

            return Task.FromResult(newEntity.AsSuccessfulOpRes());
        }

        public Task<OperationResultOf<TEntity>> TryRemoveAsync(TKey key,
            CancellationToken cancel = default)
        {
            return Task.FromResult(Store.TryRemove(key, out var entity) ?
                entity.AsSuccessfulOpRes() :
                $"No {key} matching result was found".AsFailedOpResOf<TEntity>());
        }

        public async Task<OperationResultOf<TEntity>> TryUpdateAsync(TKey key,
            Action<TEntity> updateAction,
            CancellationToken cancellationToken = default)
        {
            var getOpRes = await TryGetSingleAsync(key, cancellationToken)
                .ConfigureAwait(false);
            if (!getOpRes.Success)
                return getOpRes;

            updateAction(getOpRes.Value);

            return getOpRes.Value.AsSuccessfulOpRes();
        }
    }
}
