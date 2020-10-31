using Roman.Ambinder.DataTypes.OperationResults;
using System.Threading;
using System.Threading.Tasks;

namespace Roman.Ambinder.Storage.Common.Interfaces.SingleKey.RespositoryOperations
{

    public interface ISingleKeyRepositoryRemoveOperationsFor<TKey, TEntity>
      where TEntity : class, new()
    {
        Task<OperationResultOf<TEntity>> TryRemoveAsync(TKey key,
          CancellationToken cancellationToken = default);
    }
}
