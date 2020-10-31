using System;
using System.Threading;
using System.Threading.Tasks;
using Roman.Ambinder.DataTypes.OperationResults;
using Roman.Ambinder.Storage.Common.Interfaces.CompositeKey.RespositoryOperations;
using Roman.Ambinder.Storage.Common.Interfaces.RespositoryOperations;

namespace Roman.Ambinder.Storage.Common.Interfaces.SingleKey
{
    public interface ICompositeKeyRepositoryFor<TKey, TEntity> :
        ICompositeKeyRepositoryGetOperationsFor<TKey, TEntity>,
        IRepositoryAddOperationsFor<TKey, TEntity>,
        ICompositeKeyRepositoryUpdateOperationsFor<TKey, TEntity>,
        ICompositeKeyRepositoryRemoveOperationsFor<TKey, TEntity>
        where TEntity : class, new()
    {
        Task<OperationResultOf<TEntity>> TryAddOrUpdateAsync(object[] compositeKey,
            Action<TEntity> updateOrInitAction,
            CancellationToken cancellationToken = default);
    }
}