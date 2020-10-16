using Microsoft.EntityFrameworkCore;
using Roman.Ambinder.DataTypes.OperationResults;
using System;
using System.Threading.Tasks;

namespace Roman.Ambinder.Storage.Impl.EntityFrameworkCore.Facilities.Common
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