using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Roman.Ambinder.DataTypes.OperationResults;

namespace Roman.Ambinder.Storage.Common.Interfaces.SingleKey
{
    public interface ISingleKeyReadonlyRepositoryFor<TKey, TEntity>  
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
        Task<OperationResultOf<IReadOnlyCollection<TEntity>>> TryGetMultipleAsync(
            Expression<Func<TEntity, bool>> filter,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            CancellationToken cancellationToken = default,
            params Expression<Func<TEntity, object>>[] toBeIncluded);
    }
}