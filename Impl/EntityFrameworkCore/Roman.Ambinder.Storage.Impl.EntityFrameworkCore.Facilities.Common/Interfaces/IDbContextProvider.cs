using Microsoft.EntityFrameworkCore;
using Roman.Ambinder.DataTypes.OperationResults;
using System;
using System.Threading.Tasks;

namespace Roman.Ambinder.Storage.Impl.EntityFrameworkCore.Facilities.Common
{
    public interface IDbContextProvider
    {

        DbContext Get();

        bool DisposeAfterUsage { get; }

        //Task<OperationResultOf<TResult>> TryUseAsync<TResult>(
        //    Func<DbContext, Task<OperationResultOf<TResult>>> usage);

        Task<OperationResult> TryMigrateAsync(bool recreated = false);
    }
}