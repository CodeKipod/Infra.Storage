using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Roman.Ambinder.DataTypes.OperationResults;
using Roman.Ambinder.Storage.Common.DataTypes;
using Roman.Ambinder.Storage.Common.Interfaces.Common.RepositoryOperations;

namespace Roman.Ambinder.Storage.Impl.MongoDb
{
    public class MongoDbRepositoryGetOpsFor<TKey,TEntity>: 
        IRepositoryGetOperationsFor<TKey,TEntity> 
        where TEntity : class, new()
    {
        public Task<OperationResultOf<TEntity>> TryGetSingleAsync(
            TKey key,
            CancellationToken cancellation = default,
            params Expression<Func<TEntity, object>>[] toBeIncluded)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResultOf<PagedItemsResultOf<TEntity>>> TryGetMultipleAsync(
            Expression<Func<TEntity, bool>> filter, 
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, 
            CancellationToken cancellationToken = default,
            PagingParams pagingParams = null,
            params Expression<Func<TEntity, object>>[] toBeIncluded)
        {
            throw new NotImplementedException();
        }
    }
}
