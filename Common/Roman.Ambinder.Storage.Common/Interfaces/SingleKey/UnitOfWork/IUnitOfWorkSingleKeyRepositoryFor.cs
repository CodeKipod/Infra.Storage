using System;
using System.Threading;
using System.Threading.Tasks;
using Roman.Ambinder.DataTypes.OperationResults;
using Roman.Ambinder.Storage.Common.Interfaces.SingleKey.LocalChangesStore;

namespace Roman.Ambinder.Storage.Common.Interfaces.SingleKey.UnitOfWork
{
    public interface IUnitOfWorkSingleKeyRepositoryFor<in TKey, TEntity> : 
        IDisposable
        where TEntity : class, new()
    {
        Task<OperationResult> TryCommitChangesAsync(CancellationToken cancellationToken = default);

        ISingleKeyLocalChangesStoreFor<TKey, TEntity> LocalChangesRepository { get; }
    }
}
