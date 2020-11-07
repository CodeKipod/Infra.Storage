﻿using Microsoft.EntityFrameworkCore;
using Roman.Ambinder.DataTypes.OperationResults;
using Roman.Ambinder.Storage.Common.DataTypes;
using Roman.Ambinder.Storage.Common.Interfaces;
using Roman.Ambinder.Storage.Common.Interfaces.Common;
using Roman.Ambinder.Storage.EntityFrameworkCore.Facilities.Common;
using Roman.Ambinder.Storage.Impl.EntityFrameworkCore.Common;
using Roman.Ambinder.Storage.Impl.EntityFrameworkCore.Facilities.Common;
using Roman.Ambinder.Storage.Impl.EntityFrameworkCore.Facilities.Impl;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Roman.Ambinder.Storage.Impl.EntityFrameworkCore.CompositeKey
{
    public class EFCoreCompositeKeyReadonlyRepositoryFor<TEntity> :
        BaseDbContextStorageFor<object[], TEntity>,
        IRepositoryGetOperationsFor<object[], TEntity>
       where TEntity : class, new()
    {
        private readonly bool _trackChangesOnRetrievedEntities;

        public EFCoreCompositeKeyReadonlyRepositoryFor(
            bool trackChangesOnRetrievedEntities,
            IDbContextProvider dbContextProvider = null,
            IPrimaryKeyExpressionBuilder primaryKeyExpressionBuilder = null,
            IKeyEntityValidatorFor<object[], TEntity> keyEntityValidator = null)
            : base(new DbContextSafeUsageVisitor(dbContextProvider),
                 keyEntityValidator,
                 primaryKeyExpressionBuilder)
        {
            _trackChangesOnRetrievedEntities = trackChangesOnRetrievedEntities;
        }

        public Task<OperationResultOf<TEntity>> TryGetSingleAsync(object[] compositeKey,
            CancellationToken cancellation = default,
            params Expression<Func<TEntity, object>>[] toBeIncluded)
        {
            var validationOpRes = KeyEntityValidator.Validate(compositeKey);
            if (!validationOpRes)
                return Task.FromResult(validationOpRes.ErrorMessage.
                    AsFailedOpResOf<TEntity>());

            return DbContextSafeUsageVisitor.TryUseAsync(dbSession =>
                InternalTryGetSingleAsync(dbSession, compositeKey,
                    trackChanges: _trackChangesOnRetrievedEntities,
                    cancellation,
                    toBeIncluded));
        }

        protected async Task<OperationResultOf<TEntity>> InternalTryGetSingleAsync(
         DbContext dbSession,
         object[] keys,
         bool trackChanges,
         CancellationToken cancellation,
         params Expression<Func<TEntity, object>>[] toBeIncluded)
        {
            var query = trackChanges ?
                dbSession.Set<TEntity>().AppendIncludeExpressions(toBeIncluded) :
                dbSession.Set<TEntity>().AsNoTracking().AppendIncludeExpressions(toBeIncluded);

            var buildKeyPredicateOpRes = PrimaryKeyExpressionBuilder.TryBuildForMultitypeCompositeKey<TEntity>(dbSession, keys);
            if (!buildKeyPredicateOpRes)
                return buildKeyPredicateOpRes.ErrorMessage.AsFailedOpResOf<TEntity>();

            var filterExpression = buildKeyPredicateOpRes.Value;
            var foundEntity = await query.SingleOrDefaultAsync(filterExpression, cancellation)
                .ConfigureAwait(false);

            var success = foundEntity != null;

            return success
                ? foundEntity.AsSuccessfulOpRes()
                : $"Failed to find '{string.Join(",", keys)}' matching entity'".AsFailedOpResOf<TEntity>();
        }


        public Task<OperationResultOf<PagedItemsResultOf<TEntity>>> TryGetMultipleAsync(
            Expression<Func<TEntity, bool>> filter,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            CancellationToken cancellationToken = default,
            PagingParams pagingParams = null,
            params Expression<Func<TEntity, object>>[] toBeIncluded)
        {
            return DbContextSafeUsageVisitor.TryUseAsync(async dbSession =>
          {
              IQueryable<TEntity> query = null;
              if (_trackChangesOnRetrievedEntities)
              {
                  dbSession.Set<TEntity>()
                  .Where(filter)
                  .AppendIncludeExpressions(toBeIncluded);
              }
              else
              {
                  query = dbSession.Set<TEntity>()
                      .AsNoTracking()
                      .Where(filter)
                      .AppendIncludeExpressions(toBeIncluded);
              }

              if (orderBy != null)
                  query = orderBy(query);

              var pagedRes = await query.ToPagedResultsArrayAsync(pagingParams, cancellationToken)
                   .ConfigureAwait(false);

              var success = pagedRes != null && pagedRes.Items != null && pagedRes.Items.Count > 0;

              if (success)
              {
                  return pagedRes.AsSuccessfulOpRes();
              }

              return "Failed to find any filter matching entities"
                  .AsFailedOpResOf<PagedItemsResultOf<TEntity>>();
          });
        }
    }
}