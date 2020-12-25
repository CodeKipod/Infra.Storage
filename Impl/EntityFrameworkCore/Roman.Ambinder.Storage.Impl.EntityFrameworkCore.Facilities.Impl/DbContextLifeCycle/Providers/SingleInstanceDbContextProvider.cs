using System;
using Microsoft.EntityFrameworkCore;
using Roman.Ambinder.Storage.Impl.EntityFrameworkCore.Facilities.Common.Interfaces;

namespace Roman.Ambinder.Storage.Impl.EntityFrameworkCore.Facilities.Impl.DbContextLifeCycle.Providers
{
    public class SingleInstanceDbContextProvider :
        BaseDbContextProvider
    {
        private readonly DbContext _dbContext;

        public SingleInstanceDbContextProvider(IDbContextFactory dbContextFactory)
            : base(dbContextFactory, disposeAfterUsage: false)
        {
            dbContextFactory = dbContextFactory ??
                throw new ArgumentNullException(nameof(dbContextFactory));

            _dbContext = dbContextFactory.Create() ??
                throw new ArgumentNullException(nameof(_dbContext));
        }

        public override DbContext Get() => _dbContext;
    }
}