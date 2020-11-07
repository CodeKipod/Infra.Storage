using Microsoft.EntityFrameworkCore;
using Roman.Ambinder.DataTypes.OperationResults;
using Roman.Ambinder.Storage.Impl.EntityFrameworkCore.Facilities.Common;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Roman.Ambinder.Storage.Impl.EntityFrameworkCore.Facilities.Impl
{
    public class DbContextSafeUsageVisitor : IDbContextSafeUsageVisitor
    {
        private int _wasDisposed;

        private readonly IDbContextProvider _dbContextProvider;

        public DbContextSafeUsageVisitor(IDbContextProvider dbContextProvider)
        {
            _dbContextProvider = dbContextProvider
                ?? throw new ArgumentNullException(nameof(dbContextProvider));
        }

        public async Task<OperationResultOf<TResult>> TryUseAsync<TResult>(
         Func<DbContext, Task<OperationResultOf<TResult>>> usage)
        {
            try
            {
                if (_dbContextProvider.DisposeAfterUsage)
                {
                    using var dbContext = _dbContextProvider.Get();
                    return await usage(dbContext)
                        .ConfigureAwait(false);
                }
                else
                {
                    return await usage(_dbContextProvider.Get())
                            .ConfigureAwait(false);
                }
            }
            catch (Exception ex)
            {
                return ex.AsFailedOpResOf<TResult>();
            }
        }

        public OperationResult TryUse(Action<DbContext> usage)
        {
            try
            {
                if (_dbContextProvider.DisposeAfterUsage)
                {
                    using var dbContext = _dbContextProvider.Get();
                    usage(dbContext);
                }
                else
                    usage(_dbContextProvider.Get());
                return OperationResult.Successful;
            }
            catch (Exception ex)
            {
                return ex.AsFailedOpRes();
            }
        }

        public async Task<OperationResult> TryUseAsync(
            Func<DbContext, Task<OperationResult>> usage)
        {
            try
            {
                if (_dbContextProvider.DisposeAfterUsage)
                {
                    using var dbContext = _dbContextProvider.Get();
                    return await usage(dbContext)
                        .ConfigureAwait(false);
                }
                else
                {
                    return await usage(_dbContextProvider.Get())
                            .ConfigureAwait(false);
                }
            }
            catch (Exception ex)
            {
                return ex.AsFailedOpRes();
            }
        }

        public void Dispose()
        {
            // No need to dispose - after every usage it was disposed
            if (_dbContextProvider.DisposeAfterUsage)
                return;

            // Already disposed
            if (Interlocked.Exchange(ref _wasDisposed, 1) == 1)
                return;

            try
            {
                var dbContext = _dbContextProvider.Get();
                dbContext?.Dispose();
            }
            catch { }
        }
    }
}