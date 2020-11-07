using Roman.Ambinder.DataTypes.OperationResults;
using Roman.Ambinder.Storage.Common.DataTypes;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Roman.Ambinder.Storage.Common.Interfaces.Common
{
    public interface IRepositoryGetOperationsFor<TKey, TEntity>
      where TEntity : class, new()
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="key"></param>
        /// <param name="cancellation"></param>
        /// <param name="toBeIncluded"></param>
        /// <returns></returns>
        Task<OperationResultOf<TEntity>> TryGetSingleAsync(TKey key,
            CancellationToken cancellation = default,
            params Expression<Func<TEntity, object>>[] toBeIncluded);

        /// <summary>
        ///
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="cancellationToken"></param>
        /// <param name="toBeIncluded"></param>
        /// <returns></returns>
        Task<OperationResultOf<PagedItemsResultOf<TEntity>>> TryGetMultipleAsync(
            Expression<Func<TEntity, bool>> filter,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            CancellationToken cancellationToken = default,
            PagingParams pagingParams = null,
            params Expression<Func<TEntity, object>>[] toBeIncluded);
    }
}