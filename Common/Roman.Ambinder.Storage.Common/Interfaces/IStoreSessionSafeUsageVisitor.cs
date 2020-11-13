using Roman.Ambinder.DataTypes.OperationResults;
using System;
using System.Threading.Tasks;

namespace Roman.Ambinder.Storage.Common.Interfaces
{
    public interface IStoreSessionSafeUsageVisitor<TStoreSession> : IDisposable
        where TStoreSession : IDisposable
    {
        OperationResult TryUse(Action<TStoreSession> usage);

        Task<OperationResult> TryUseAsync(
            Func<TStoreSession, Task<OperationResult>> usage);

        Task<OperationResultOf<TResult>> TryUseAsync<TResult>(
            Func<TStoreSession, Task<OperationResultOf<TResult>>> usage);
    }
}
