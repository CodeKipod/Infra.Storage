using Roman.Ambinder.DataTypes.OperationResults;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Roman.Ambinder.Storage.Common.Interfaces.Common.UnitOfWork
{
    public interface IUnitOfWorkRepositoryFor<TKey, TEntity> : IDisposable
        where TEntity : class, new()
    {
        Task<OperationResult> TryCommitChangesAsync(
            CancellationToken cancellationToken = default);

        IRepositoryFor<TKey, TEntity> Repository { get; }
    }
}