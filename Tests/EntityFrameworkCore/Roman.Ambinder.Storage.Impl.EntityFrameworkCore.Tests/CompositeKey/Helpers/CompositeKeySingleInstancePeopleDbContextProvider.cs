﻿using Microsoft.EntityFrameworkCore;
using Roman.Ambinder.Storage.Impl.EntityFrameworkCore.Facilities.Impl;
using System;

namespace Roman.Ambinder.Storage.Impl.EntityFrameworkCore.Tests.CompositeKey.Helpers
{
    public class CompositeKeySingleInstancePeopleDbContextProvider : BaseDbContextProvider
    {
        private readonly Lazy<DbContext> _lazyDbContextProvider;

        public CompositeKeySingleInstancePeopleDbContextProvider()
             : base(new CallbackDbContextFactory(() => new CompositeKeyPeopleDbContext()),
                   disposeAfterUsage: false)
        {
            _lazyDbContextProvider = new Lazy<DbContext>(_dbContextFactory.Create,
                isThreadSafe: true);
        }

        public override DbContext Get() => _lazyDbContextProvider.Value;
    }
}