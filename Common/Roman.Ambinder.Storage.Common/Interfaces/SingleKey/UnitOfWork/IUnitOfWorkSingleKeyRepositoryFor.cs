using Roman.Ambinder.DataTypes.OperationResults;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Roman.Ambinder.Storage.Common.Interfaces.UnitOfWork
{
    public interface IUnitOfWorkSingleKeyRepositoryFor<TKey, TEntity> : IDisposable
        where TEntity : class, new()
    {
        Task<OperationResult> TryCommitChangesAsync(CancellationToken cancellationToken = default);

        ISingleKeyLocalChangesStoreFor<TKey, TEntity> LocalChangesReposiotry { get; }
    }
}
