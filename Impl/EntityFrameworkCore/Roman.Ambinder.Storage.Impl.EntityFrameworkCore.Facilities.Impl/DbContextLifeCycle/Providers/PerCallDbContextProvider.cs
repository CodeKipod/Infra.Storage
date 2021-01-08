using Microsoft.EntityFrameworkCore;
using Roman.Ambinder.Storage.Impl.EntityFrameworkCore.Facilities.Common.Interfaces;

namespace Roman.Ambinder.Storage.Impl.EntityFrameworkCore.Facilities.Impl.DbContextLifeCycle.Providers
{
    /// <summary>
    ///
    /// </summary>
    public class PerCallDbContextProvider : BaseDbContextProvider
    {
        public PerCallDbContextProvider(IDbContextFactory dbContextFactory)
            : base(dbContextFactory, disposeAfterUsage: true)
        { }

        public override DbContext Get() => _dbContextFactory.Create();
    }
}