﻿using Microsoft.EntityFrameworkCore;
using System;
using Roman.Ambinder.Storage.Impl.EntityFrameworkCore.Facilities.Impl.DbContextLifeCycle;
using Roman.Ambinder.Storage.Impl.EntityFrameworkCore.Facilities.Impl.DbContextLifeCycle.Providers;

namespace Roman.Ambinder.Storage.Impl.EntityFrameworkCore.Tests.CompositeKey.Helpers
{
    public class CompositeKeySingleInstancePeopleDbContextProvider : BaseDbContextProvider
    {
        private readonly Lazy<DbContext> _lazyDbContextProvider;

        public CompositeKeySingleInstancePeopleDbContextProvider()
             : base(new CallbackDbContextFactory(() => new CompositeKeyPeopleDbContext()),
                   disposeAfterUsage: false)
        {
            _lazyDbContextProvider = new Lazy<DbContext>(DbContextFactory.Create,
                isThreadSafe: true);
        }

        public override DbContext Get() => _lazyDbContextProvider.Value;
    }
}