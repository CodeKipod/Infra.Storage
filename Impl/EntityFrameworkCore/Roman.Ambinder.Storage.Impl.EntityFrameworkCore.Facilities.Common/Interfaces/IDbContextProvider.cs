using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Roman.Ambinder.DataTypes.OperationResults;

namespace Roman.Ambinder.Storage.Impl.EntityFrameworkCore.Facilities.Common.Interfaces
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