using Roman.Ambinder.DataTypes.OperationResults;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Roman.Ambinder.Storage.Common.Interfaces.CompositeKey.RespositoryOperations
{
    public interface ICompositeKeyRepositoryUpdateOperationsFor<TKey, TEntity>
       where TEntity : class, new()
    {
        Task<OperationResultOf<TEntity>> TryUpdateAsync(object[] compositeKey,
          Action<TEntity> updateAction,
          CancellationToken cancellationToken = default);
    }
}
