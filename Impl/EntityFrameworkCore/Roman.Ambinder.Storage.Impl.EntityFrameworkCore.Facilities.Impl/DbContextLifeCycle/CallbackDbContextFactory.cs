using Microsoft.EntityFrameworkCore;
using Roman.Ambinder.Storage.Impl.EntityFrameworkCore.Facilities.Common;
using System;

namespace Roman.Ambinder.Storage.Impl.EntityFrameworkCore.Facilities.Impl
{
    public class CallbackDbContextFactory : IDbContextFactory
    {
        private readonly Func<DbContext> _factoryCallback;

        public CallbackDbContextFactory(Func<DbContext> factoryCallback)
        {
            _factoryCallback = factoryCallback;
        }

        public DbContext Create() => _factoryCallback();
    }
}