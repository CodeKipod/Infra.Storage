using Roman.Ambinder.DataTypes.OperationResults;
using System.Threading;
using System.Threading.Tasks;

namespace Roman.Ambinder.Storage.Common.Interfaces.CompositeKey.RespositoryOperations
{

    public interface ICompositeKeyRepositoryRemoveOperationsFor<TKey, TEntity>
      where TEntity : class, new()
    {
        Task<OperationResultOf<TEntity>> TryRemoveAsync(object[] compositeKey,
          CancellationToken cancellationToken = default);
    }
}
