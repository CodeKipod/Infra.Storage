using NHibernate;
using Roman.Ambinder.DataTypes.OperationResults;
using Roman.Ambinder.Storage.Common.Interfaces;
using Roman.Ambinder.Storage.Common.Interfaces.Common;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Roman.Ambinder.Storage.Impl.NHibernate
{
    public class NHibernateRepositoryFor<TKey, TEntity> :
        NHibernateReadonlyRepositoryFor<TKey, TEntity>,
        IRepositoryFor<TKey, TEntity>
        where TEntity : class, new()
    {
        private bool _saveAfterChange;

        public NHibernateRepositoryFor(
            IStoreSessionSafeUsageVisitor<ISession> storeSessionSafeUsageVisitor,
            IKeyEntityValidatorFor<TKey, TEntity> keyEntityValidator)
            : base(storeSessionSafeUsageVisitor, keyEntityValidator)
        {
        }
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
            return StoreSessionSafeUsageVisitor.TryUseAsync(async dbSession =>
            {
                var validationOpRes = KeyEntityValidator.Validate(newEntity);
                if (!validationOpRes)
                    return validationOpRes.ErrorMessage.AsFailedOpResOf<TEntity>();

                await dbSession.SaveAsync(dbSession, cancellationToken)
                    .ConfigureAwait(false);

                return await SaveChangesAndReturnResultAsync(dbSession,
                        newEntity,
                        cancellationToken)
                    .ConfigureAwait(false);
            });
        }

        public Task<OperationResultOf<TEntity>> TryAddOrUpdateAsync(TKey key,
            Action<TEntity> updateOrInitAction,
            CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResultOf<TEntity>> TryRemoveAsync(TKey key,
            CancellationToken cancellationToken = default)
        {
            return StoreSessionSafeUsageVisitor.TryUseAsync(async dbSession =>
            {
                var validationOpRes = KeyEntityValidator.Validate(key);
                if (!validationOpRes)
                    return validationOpRes.ErrorMessage.AsFailedOpResOf<TEntity>();

                var getOpRes = await this.TryGetSingleAsync(key, cancellationToken)
                    .ConfigureAwait(false);

                if (!getOpRes)
                    return getOpRes;

                var existingEntity = getOpRes.Value;

                await dbSession.DeleteAsync(existingEntity, cancellationToken)
                    .ConfigureAwait(false);

                return await SaveChangesAndReturnResultAsync(
                         dbSession, existingEntity, cancellationToken)
                    .ConfigureAwait(false);
            });
        }

        public Task<OperationResultOf<TEntity>> TryUpdateAsync(TKey key,
            Action<TEntity> updateAction,
            CancellationToken cancellationToken = default)
        {
            return StoreSessionSafeUsageVisitor.TryUseAsync(async dbSession =>
            {
                var validationOpRes = KeyEntityValidator.Validate(key);
                if (!validationOpRes)
                    return validationOpRes.ErrorMessage.AsFailedOpResOf<TEntity>();

                var getOpRes = await this.TryGetSingleAsync(key, cancellationToken)
                    .ConfigureAwait(false);

                if (!getOpRes)
                    return getOpRes;

                var existingEntity = getOpRes.Value;
                updateAction(existingEntity);

                await dbSession.UpdateAsync(getOpRes.Value, cancellationToken)
                    .ConfigureAwait(false);

                return await SaveChangesAndReturnResultAsync(
                    dbSession, existingEntity, cancellationToken)
                .ConfigureAwait(false);

            });
        }

        private async Task<OperationResultOf<TResult>> SaveChangesAndReturnResultAsync<TResult>(
         ISession dbSession,
         TResult result,
         CancellationToken cancellationToken)
        {
            if (!_saveAfterChange)
                return result.AsSuccessfulOpRes();

            await dbSession.FlushAsync(cancellationToken);
            //var dbChangesMade = await dbSession.SaveAsync(cancellationToken)
            //    .ConfigureAwait(false);

            var success = true;//dbChangesMade > 0;

            return success ? result.AsSuccessfulOpRes() :
                $"Expected for at least a single database modification to be made for {nameof(TEntity)}"
                .AsFailedOpResOf<TResult>();
        }
    }
}
