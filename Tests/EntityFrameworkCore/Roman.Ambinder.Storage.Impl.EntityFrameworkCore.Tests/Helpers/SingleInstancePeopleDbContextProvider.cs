using Roman.Ambinder.Storage.Impl.EntityFrameworkCore.Facilities.Impl;

namespace Roman.Ambinder.Storage.Impl.EntityFrameworkCore.Tests.Helpers
{
    public class SingleInstancePeopleDbContextProvider : PerCallDbContextProvider
    {
        public SingleInstancePeopleDbContextProvider()
             : base(new CallbackDbContextFactory(() => new PeopleDbContext()))
        {
        }
    }
}