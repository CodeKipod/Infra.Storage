using System;
using System.Threading;
using System.Threading.Tasks;
using Roman.Ambinder.DataTypes.OperationResults;

namespace Roman.Ambinder.Storage.Common.Interfaces.Common.RepositoryOperations
{
    public interface IRepositoryUpdateOperationsFor<in TKey, TEntity>
       where TEntity : class, new()
    {
        Task<OperationResultOf<TEntity>> TryUpdateAsync(TKey key,
          Action<TEntity> updateAction,
          CancellationToken cancellationToken = default);
    }
}