using Microsoft.EntityFrameworkCore;
using Roman.Ambinder.DataTypes.OperationResults;
using Roman.Ambinder.Storage.Common.Interfaces;
using Roman.Ambinder.Storage.Common.Interfaces.UnitOfWork;
using Roman.Ambinder.Storage.Impl.EntityFrameworkCore.Common;
using Roman.Ambinder.Storage.Impl.EntityFrameworkCore.Facilities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Roman.Ambinder.Storage.Impl.EntityFrameworkCore.SingleKey.UnitOfWork
{
    public class DbContextSingleKeyLocalStoreFor<TKey, TEntity> :
        BaseDbContextStorageFor<TKey, TEntity>,
        ISingleKeyLocalChangesStoreFor<TKey, TEntity>
         where TEntity : class, new()
    {
        public DbContextSingleKeyLocalStoreFor(
            IDbContextSafeUsageVisitor dbContextSafeUsageVisitor,
            IKeyEntityValidatorFor<TKey, TEntity> keyEntityValidator = null)
            : base(dbContextSafeUsageVisitor, keyEntityValidator)
        { }

        public Task<OperationResultOf<TEntity>> TryGetSingleAsync(TKey key,
                 CancellationToken cancellation = default,
                 params Expression<Func<TEntity, object>>[] toBeIncluded)
        {
            var validationOpRes = KeyEntityValidator.Validate(key);
            if (!validationOpRes)
                return Task.FromResult(validationOpRes.ErrorMessage.
                    AsFailedOpResOf<TEntity>());

            return DbContextSafeUsageVisitor.TryUseAsync(dbSession =>
                InternalTryGetSingleAsync(dbSession, key,
                    trackChanges: true,
                    cancellation,
                    toBeIncluded));
        }

        protected async Task<OperationResultOf<TEntity>> InternalTryGetSingleAsync(
            DbContext dbSession,
            TKey key,
            bool trackChanges,
            CancellationToken cancellation,
            params Expression<Func<TEntity, object>>[] toBeIncluded)
        {
            var query = trackChanges ?
                dbSession.Set<TEntity>().AppendIncludeExpressions(toBeIncluded) :
                dbSession.Set<TEntity>().AsNoTracking().AppendIncludeExpressions(toBeIncluded);

            var buildKeyPredicateOpRes = PrimaryKeyExpressionBuilder.TryBuildForSingleKey<TKey, TEntity>(dbSession, key);
            if (!buildKeyPredicateOpRes)
                return buildKeyPredicateOpRes.ErrorMessage.AsFailedOpResOf<TEntity>();

            var filterExpression = buildKeyPredicateOpRes.Value;
            var foundEntity = await query.SingleOrDefaultAsync(filterExpression, cancellation)
                .ConfigureAwait(false);

            var success = foundEntity != null;

            return success
                ? foundEntity.AsSuccessfulOpRes()
                : $"Failed to find '{key}' matching entity'".AsFailedOpResOf<TEntity>();
        }

        public Task<OperationResultOf<IReadOnlyCollection<TEntity>>> TryGetMultipleAsync(
            Expression<Func<TEntity, bool>> filter,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            CancellationToken cancellationToken = default,
            params Expression<Func<TEntity, object>>[] toBeIncluded)
        {
            return DbContextSafeUsageVisitor.TryUseAsync(async dbSession =>
            {
                var query = dbSession.Set<TEntity>()
                    .AsNoTracking()
                    .Where(filter)
                    .AppendIncludeExpressions(toBeIncluded);

                if (orderBy != null)
                    query = orderBy(query);

                IReadOnlyCollection<TEntity> foundEntities =
                    await query.ToArrayAsync(cancellationToken)
                        .ConfigureAwait(false);

                var success = foundEntities != null && foundEntities.Count > 0;

                if (success)
                    return foundEntities.AsSuccessfulOpRes();

                return "Failed to find any filter matching entities"
                    .AsFailedOpResOf<IReadOnlyCollection<TEntity>>();
            });
        }

        public OperationResult TryAdd(TEntity newEntity)
        {
            var validateEntityOpRes = KeyEntityValidator.Validate(newEntity);
            if (!validateEntityOpRes) return validateEntityOpRes;

            return DbContextSafeUsageVisitor.TryUse((dbSession) => dbSession.Add(newEntity));
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Performance",
            "U2U1009:Async or iterator methods should avoid state machine generation for early exits (throws or synchronous returns)",
            Justification = "<Pending>")]
        public async Task<OperationResult> TryRemoveAsync(TKey key,
            CancellationToken cancellationToken = default)
        {
            var validateKeyOpRes = KeyEntityValidator.Validate(key);
            if (!validateKeyOpRes) return validateKeyOpRes;

            var getOpRes = await TryGetSingleAsync(key, cancellationToken)
                .ConfigureAwait(false);
            if (!getOpRes)
                return getOpRes;

            var existingEntity = getOpRes.Value;

            return DbContextSafeUsageVisitor.TryUse((dbSession) => dbSession.Remove(existingEntity));
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Performance",
            "U2U1009:Async or iterator methods should avoid state machine generation for early exits (throws or synchronous returns)", Justification = "<Pending>")]
        public async Task<OperationResult> TryUpdateAsync(TKey key,
            Action<TEntity> updateAction,
            CancellationToken cancellationToken = default)
        {
            var validateKeyOpRes = KeyEntityValidator.Validate(key);
            if (!validateKeyOpRes) return validateKeyOpRes;

            var getOpRes = await TryGetSingleAsync(key, cancellationToken).ConfigureAwait(false);
            if (!getOpRes)
                return getOpRes;

            var existingValue = getOpRes.Value;
            try
            {
                updateAction(existingValue);
                return OperationResult.Successful;
            }
            catch (Exception ex)
            {
                return ex.AsFailedOpRes();
            }
        }


    }
}
