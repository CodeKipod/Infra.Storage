using System.Threading;
using System.Threading.Tasks;
using Roman.Ambinder.DataTypes.OperationResults;

namespace Roman.Ambinder.Storage.Common.Interfaces.Common.RepositoryOperations
{
    public interface IRepositoryRemoveOperationsFor<TKey, TEntity>
      where TEntity : class, new()
    {
        Task<OperationResultOf<TEntity>> TryRemoveAsync(TKey key,
          CancellationToken cancellationToken = default);
    }
}