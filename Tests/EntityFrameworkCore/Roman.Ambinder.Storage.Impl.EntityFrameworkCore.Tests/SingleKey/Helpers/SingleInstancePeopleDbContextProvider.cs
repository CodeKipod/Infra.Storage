using Microsoft.EntityFrameworkCore;
using Roman.Ambinder.Storage.Impl.EntityFrameworkCore.Facilities.Impl;
using System;

namespace Roman.Ambinder.Storage.Impl.EntityFrameworkCore.Tests.SingleKey.Helpers
{
    public class SingleInstancePeopleDbContextProvider : BaseDbContextProvider
    {
        private readonly Lazy<DbContext> _lazyDbContextProvider;

        public SingleInstancePeopleDbContextProvider()
             : base(new CallbackDbContextFactory(() => new PeopleDbContext()),
                   disposeAfterUsage: false)
        {
            _lazyDbContextProvider = new Lazy<DbContext>(
                _dbContextFactory.Create,
                isThreadSafe: true);
        }

        public override DbContext Get() => _lazyDbContextProvider.Value;
    }
}