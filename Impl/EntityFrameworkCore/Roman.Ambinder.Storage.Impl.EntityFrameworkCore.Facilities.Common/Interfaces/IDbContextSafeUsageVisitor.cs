using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Roman.Ambinder.DataTypes.OperationResults;

namespace Roman.Ambinder.Storage.Impl.EntityFrameworkCore.Facilities.Common.Interfaces
{
    public interface IDbContextSafeUsageVisitor : IDisposable
    {
        OperationResult TryUse(Action<DbContext> usage);

        Task<OperationResult> TryUseAsync(
            Func<DbContext, Task<OperationResult>> usage);

        Task<OperationResultOf<TResult>> TryUseAsync<TResult>(Func<DbContext,
            Task<OperationResultOf<TResult>>> usage);
    }
}