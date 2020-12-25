using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Roman.Ambinder.DataTypes.OperationResults;

namespace Roman.Ambinder.Storage.Common.Interfaces.NotUsed
{
    public interface IQueryRepositoryFor<in TKey, TEntity>
        where TEntity : class, new()
    {
        Task<OperationResultOf<TEntity>> TryGetAsync(TKey key,
            CancellationToken cancellationToken);

        Task<OperationResultOf<IReadOnlyCollection<TEntity>>> TryGetMultipleAsync(
            Expression<Func<TEntity, bool>> filter,
            CancellationToken cancellationToken);
    }
}
