using Roman.Ambinder.Storage.Impl.EntityFrameworkCore.Facilities.Impl;
using Roman.Ambinder.Storage.Impl.EntityFrameworkCore.Facilities.Impl.DbContextLifeCycle;
using Roman.Ambinder.Storage.Impl.EntityFrameworkCore.Facilities.Impl.DbContextLifeCycle.Providers;

namespace Roman.Ambinder.Storage.Impl.EntityFrameworkCore.Tests.SingleKey.Helpers
{
    public class PreCallPeopleDbContextProvider : PerCallDbContextProvider
    {
        public PreCallPeopleDbContextProvider()
            : base(new CallbackDbContextFactory(() => new PeopleDbContext()))
        {
        }
    }
}