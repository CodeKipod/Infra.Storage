using Roman.Ambinder.DataTypes.OperationResults;
using Roman.Ambinder.Storage.Common.Interfaces.SingleKey;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Roman.Ambinder.Storage.Impl.InMemory
{
    public class InMemorySingleKeyReadonlyRepositoryFor<TKey, TEntity> :
        ISingleKeyReadonlyRepositoryFor<TKey, TEntity>
        where TEntity : class, new()
    {
        protected readonly ConcurrentDictionary<TKey, TEntity> Store =
            new ConcurrentDictionary<TKey, TEntity>();

        public Task<OperationResultOf<TEntity>> TryGetSingleAsync(TKey key,
          CancellationToken cancellationToken = default,
          params Expression<Func<TEntity, object>>[] toBeIncluded)
        {
            return Task.FromResult(Store.TryGetValue(key, out var entity) ?
                entity.AsSuccessfulOpRes() :
                $"No {key} matching result was found".AsFailedOpResOf<TEntity>());
        }

        public Task<OperationResultOf<IReadOnlyCollection<TEntity>>> TryGetMultipleAsync(
          Expression<Func<TEntity, bool>> filter,
          Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
          CancellationToken cancellationToken = default,
          params Expression<Func<TEntity, object>>[] toBeIncluded)
        {
            var query = Store.Values.AsQueryable<TEntity>().Where(filter); 
            if (orderBy != null)
                query = orderBy(query);

            IReadOnlyCollection<TEntity> results = query.ToArray();

            var opRes = results.Count > 0
                ? results.AsSuccessfulOpRes()
                : "No matching results were found".AsFailedOpResOf<IReadOnlyCollection<TEntity>>();

            return Task.FromResult(opRes);
        }

    }
}
