using Microsoft.EntityFrameworkCore;
using Roman.Ambinder.DataTypes.OperationResults;
using Roman.Ambinder.Storage.Common.Interfaces;
using Roman.Ambinder.Storage.Common.Interfaces.SingleKey;
using Roman.Ambinder.Storage.Impl.EntityFrameworkCore.Common;
using Roman.Ambinder.Storage.Impl.EntityFrameworkCore.Facilities.Common;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Roman.Ambinder.Storage.Impl.EntityFrameworkCore
{
    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    public class EFCoreSingleKeyRepositoryFor<TKey, TEntity> :
        EFCoreSingleKeyReadonlyRepositoryFor<TKey, TEntity>,
        ISingleKeyRepositoryFor<TKey, TEntity>
        where TEntity : class, new()
    {
        public EFCoreSingleKeyRepositoryFor(IDbContextProvider dbContextProvider,
            IKeyEntityValidatorFor<TKey, TEntity> keyEntityValidator = null)
            : base(trackChangesOnRetrievedEntities: false, 
                   dbContextProvider, keyEntityValidator: keyEntityValidator)
        { }

        public Task<OperationResultOf<TEntity>> TryAddAsync(
            Action<TEntity> initAction = null,
            CancellationToken cancellationToken = default)
        {
            var newEntity = new TEntity();
            initAction?.Invoke(newEntity);
            return TryAddAsync(newEntity, cancellationToken);
        }

        public Task<OperationResultOf<TEntity>> TryAddAsync(TEntity newEntity,
            CancellationToken cancellationToken = default)
        {
            return DbContextSafeUsageVisitor.TryUseAsync(async dbSession =>
            {
                var validationOpRes = KeyEntityValidator.Validate(newEntity);
                if (!validationOpRes)
                    return validationOpRes.ErrorMessage.AsFailedOpResOf<TEntity>();

                var entitySet = dbSession.Set<TEntity>();

                await entitySet.AddAsync(newEntity, cancellationToken)
                    .ConfigureAwait(false);

                return await SaveChangesAndReturnResultAsync(dbSession,
                        newEntity,
                        cancellationToken)
                    .ConfigureAwait(false);
            });
        }

        public Task<OperationResultOf<TEntity>> TryUpdateAsync(TKey key,
            Action<TEntity> updateAction,
            CancellationToken cancellationToken = default)
        {
            var validationOpRes = KeyEntityValidator.Validate(key);
            if (!validationOpRes)
                return Task.FromResult(validationOpRes.ErrorMessage.AsFailedOpResOf<TEntity>());

            return DbContextSafeUsageVisitor.TryUseAsync(async dbSession =>
            {
                var getOpRes = await InternalTryGetSingleAsync(dbSession, key,
                        trackChanges: true,
                        cancellationToken)
                    .ConfigureAwait(false);

                if (!getOpRes)
                    return getOpRes;

                var targetEntity = getOpRes.Value;

                updateAction(targetEntity);

                var valueValidationOpRes = KeyEntityValidator.Validate(targetEntity);
                if (!valueValidationOpRes)
                    return valueValidationOpRes.ErrorMessage.AsFailedOpResOf<TEntity>();

                return await SaveChangesAndReturnResultAsync(dbSession, targetEntity, cancellationToken)
                    .ConfigureAwait(false);
            });
        }

        public async Task<OperationResultOf<TEntity>> TryAddOrUpdateAsync(TKey key,
            Action<TEntity> updateOrInitAction,
            CancellationToken cancellationToken = default)
        {
            var getOpRes = await TryGetSingleAsync(key, cancellationToken)
                .ConfigureAwait(false);

            // Entity exists => Update
            if (getOpRes)
                return await TryUpdateAsync(key, updateOrInitAction, cancellationToken)
                    .ConfigureAwait(false);

            // Entity Does not exist => Add new entity
            var entity = new TEntity();
            updateOrInitAction(entity);
            return await TryAddAsync(entity, cancellationToken).ConfigureAwait(false);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Performance", "U2U1009:Async or iterator methods should avoid state machine generation for early exits (throws or synchronous returns)", Justification = "<Pending>")]
        public async Task<OperationResultOf<TEntity>> TryRemoveAsync(TKey key,
            CancellationToken cancellationToken = default)
        {
            var validationOpRes = KeyEntityValidator.Validate(key);
            if (!validationOpRes)
                return validationOpRes.ErrorMessage.AsFailedOpResOf<TEntity>();

            var getOpRes = await TryGetSingleAsync(key, cancellationToken)
                .ConfigureAwait(false);

            if (!getOpRes)
                return getOpRes;

            return await DbContextSafeUsageVisitor.TryUseAsync(async dbSession =>
            {
                var foundEntity = getOpRes.Value;

                var dbSet = dbSession.Set<TEntity>();
                dbSet.Attach(foundEntity);

                dbSet.Remove(foundEntity);

                return await SaveChangesAndReturnResultAsync(dbSession, foundEntity, cancellationToken)
                      .ConfigureAwait(false);
            }).ConfigureAwait(false);
        }

        private static async Task<OperationResultOf<TResult>> SaveChangesAndReturnResultAsync<TResult>(
          DbContext dbSession,
          TResult result,
          CancellationToken cancellationToken)
        {
            var dbChangesMade = await dbSession.SaveChangesAsync(cancellationToken)
                .ConfigureAwait(false);

            var success = dbChangesMade > 0;

            return success ? result.AsSuccessfulOpRes() :
                $"Expected for at least a single database modification to be made for {nameof(TEntity)}"
                .AsFailedOpResOf<TResult>();
        }
    }
}