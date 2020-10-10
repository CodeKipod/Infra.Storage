using Roman.Ambinder.Storage.Impl.EntityFrameworkCore.Facilities.Impl;

namespace Roman.Ambinder.Storage.Impl.EntityFrameworkCore.Tests.Helpers
{
    public class PreCallPeopleDbContextProvider : PerCallDbContextProvider
    {
        public PreCallPeopleDbContextProvider()
            : base(new CallbackDbContextFactory(() => new PeopleDbContext()))
        {
        }
    }
}