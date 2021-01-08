using Roman.Ambinder.DataTypes.OperationResults;
using System;
using System.Threading;
using System.Threading.Tasks;
using Roman.Ambinder.Storage.Common.Interfaces.Common.RepositoryOperations;

namespace Roman.Ambinder.Storage.Common.Interfaces.Common
{
    public interface IRepositoryFor<TKey, TEntity> :
       IRepositoryGetOperationsFor<TKey, TEntity>,
       IRepositoryUpdateOperationsFor<TKey, TEntity>,
       IRepositoryRemoveOperationsFor<TKey, TEntity>,
       IRepositoryAddOperationsFor<TEntity>
       where TEntity : class, new()
    {
        Task<OperationResultOf<TEntity>> TryAddOrUpdateAsync(TKey key,
            Action<TEntity> updateOrInitAction,
            CancellationToken cancellationToken = default);
    }
}