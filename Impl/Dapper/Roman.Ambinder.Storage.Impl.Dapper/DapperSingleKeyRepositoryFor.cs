using Roman.Ambinder.DataTypes.OperationResults;
using Roman.Ambinder.Storage.Common.Interfaces.SingleKey;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Roman.Ambinder.Storage.Impl.Dapper
{
    public class DapperSingleKeyRepositoryFor<TKey, TEntity> :
        ISingleKeyRepositoryFor<TKey, TEntity>
        where TEntity : class, new()
    {
        public DapperSingleKeyRepositoryFor()
        {
        }

        public Task<OperationResultOf<TEntity>> TryAddAsync(
            Action<TEntity> initAction = null,
            CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResultOf<TEntity>> TryAddAsync(TEntity newEntity,
            CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResultOf<TEntity>> TryAddOrUpdateAsync(TKey key,
            Action<TEntity> updateOrInitAction,
            CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResultOf<IReadOnlyCollection<TEntity>>> TryGetMultipleAsync(
            Expression<Func<TEntity, bool>> filter,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, 
            CancellationToken cancellationToken = default, 
            params Expression<Func<TEntity, object>>[] toBeIncluded)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResultOf<TEntity>> TryGetSingleAsync(TKey key,
            CancellationToken cancellation = default,
            params Expression<Func<TEntity, object>>[] toBeIncluded)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResultOf<TEntity>> TryRemoveAsync(TKey key,
            CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResultOf<TEntity>> TryUpdateAsync(TKey key, 
            Action<TEntity> updateAction, 
            CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
