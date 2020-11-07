using Roman.Ambinder.DataTypes.OperationResults;
using System.Threading;
using System.Threading.Tasks;

namespace Roman.Ambinder.Storage.Common.Interfaces.Common
{
    public interface IRepositoryRemoveOperationsFor<TKey, TEntity>
      where TEntity : class, new()
    {
        Task<OperationResultOf<TEntity>> TryRemoveAsync(TKey key,
          CancellationToken cancellationToken = default);
    }
}