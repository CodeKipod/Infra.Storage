using Roman.Ambinder.DataTypes.OperationResults;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Roman.Ambinder.Storage.Common.Interfaces.Common
{
    public interface IRepositoryUpdateOperationsFor<TKey, TEntity>
       where TEntity : class, new()
    {
        Task<OperationResultOf<TEntity>> TryUpdateAsync(TKey key,
          Action<TEntity> updateAction,
          CancellationToken cancellationToken = default);
    }
}
