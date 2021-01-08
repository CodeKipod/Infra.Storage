using System;
using Microsoft.EntityFrameworkCore;
using Roman.Ambinder.Storage.Impl.EntityFrameworkCore.Facilities.Common.Interfaces;

namespace Roman.Ambinder.Storage.Impl.EntityFrameworkCore.Facilities.Impl.DbContextLifeCycle
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