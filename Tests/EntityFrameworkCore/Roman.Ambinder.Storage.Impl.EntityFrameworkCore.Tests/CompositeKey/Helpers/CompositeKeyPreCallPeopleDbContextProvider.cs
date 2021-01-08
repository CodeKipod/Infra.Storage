using Roman.Ambinder.Storage.Impl.EntityFrameworkCore.Facilities.Impl;
using Roman.Ambinder.Storage.Impl.EntityFrameworkCore.Facilities.Impl.DbContextLifeCycle;
using Roman.Ambinder.Storage.Impl.EntityFrameworkCore.Facilities.Impl.DbContextLifeCycle.Providers;

namespace Roman.Ambinder.Storage.Impl.EntityFrameworkCore.Tests.CompositeKey.Helpers
{
    public class CompositeKeyPreCallPeopleDbContextProvider : PerCallDbContextProvider
    {
        public CompositeKeyPreCallPeopleDbContextProvider()
            : base(new CallbackDbContextFactory(() => new CompositeKeyPeopleDbContext()))
        {
        }
    }
}