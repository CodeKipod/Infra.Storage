using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using NHibernate;
using NHibernate.Linq;
using Roman.Ambinder.DataTypes.OperationResults;
using Roman.Ambinder.Storage.Common.DataTypes;
using Roman.Ambinder.Storage.Common.Interfaces;
using Roman.Ambinder.Storage.Common.Interfaces.Common.RepositoryOperations;

namespace Roman.Ambinder.Storage.Impl.NHibernate.Common
{
    public class NHibernateReadonlyRepositoryFor<TKey, TEntity> :
        IRepositoryGetOperationsFor<TKey, TEntity>
        where TEntity : class, new()
    {
        protected readonly IKeyEntityValidatorFor<TKey, TEntity> KeyEntityValidator;
        protected readonly IStoreSessionSafeUsageVisitor<ISession> StoreSessionSafeUsageVisitor;

        public NHibernateReadonlyRepositoryFor(
            IStoreSessionSafeUsageVisitor<ISession> storeSessionSafeUsageVisitor, 
            IKeyEntityValidatorFor<TKey, TEntity> keyEntityValidator)
        {
            StoreSessionSafeUsageVisitor = storeSessionSafeUsageVisitor;
            KeyEntityValidator = keyEntityValidator;
        }

        public Task<OperationResultOf<PagedItemsResultOf<TEntity>>> TryGetMultipleAsync(
            Expression<Func<TEntity, bool>> filter,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            CancellationToken cancellationToken = default,
            PagingParams pagingParams = null,
            params Expression<Func<TEntity, object>>[] toBeIncluded)
        {
            return StoreSessionSafeUsageVisitor.TryUseAsync(async session =>
            {
                var query = session.Query<TEntity>()
                    .Where(filter);

                if (orderBy != null)
                    query = orderBy(query);

                var res = await query
                    .ToListAsync(cancellationToken)
                    .ConfigureAwait(false);

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

        public Task<OperationResultOf<TEntity>> TryGetSingleAsync(TKey key,
            CancellationToken cancellation = default,
            params Expression<Func<TEntity, object>>[] toBeIncluded)
        {
            var validationOpRes = KeyEntityValidator.Validate(key);
            if (!validationOpRes)
                return Task.FromResult(validationOpRes.ErrorMessage.
                    AsFailedOpResOf<TEntity>());

            return StoreSessionSafeUsageVisitor.TryUseAsync<TEntity>(async session =>
                {
                    var foundEntity = await session.GetAsync<TEntity>(key, cancellation);

                    var success = foundEntity != null;

                    return success
                        ? foundEntity.AsSuccessfulOpRes()
                        : $"Failed to find '{key}' matching entity'".AsFailedOpResOf<TEntity>();
                });
        }
    }
}
