using Roman.Ambinder.DataTypes.OperationResults;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Roman.Ambinder.Storage.Common.Interfaces.RespositoryOperations
{
    public interface IRepositoryAddOperationsFor<TKey, TEntity>
        where TEntity : class, new()
    {
        Task<OperationResultOf<TEntity>> TryAddAsync(
           Action<TEntity> initAction = null,
           CancellationToken cancellationToken = default);

        Task<OperationResultOf<TEntity>> TryAddAsync(TEntity newEntity,
            CancellationToken cancellationToken = default);
    }
}
