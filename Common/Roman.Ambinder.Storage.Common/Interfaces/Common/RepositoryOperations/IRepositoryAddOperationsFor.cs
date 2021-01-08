using System;
using System.Threading;
using System.Threading.Tasks;
using Roman.Ambinder.DataTypes.OperationResults;

namespace Roman.Ambinder.Storage.Common.Interfaces.Common.RepositoryOperations
{
    public interface IRepositoryAddOperationsFor<TEntity>
        where TEntity : class, new()
    {
        Task<OperationResultOf<TEntity>> TryAddAsync(
           Action<TEntity> initAction = null,
           CancellationToken cancellationToken = default);

        Task<OperationResultOf<TEntity>> TryAddAsync(TEntity newEntity,
            CancellationToken cancellationToken = default);
    }
}