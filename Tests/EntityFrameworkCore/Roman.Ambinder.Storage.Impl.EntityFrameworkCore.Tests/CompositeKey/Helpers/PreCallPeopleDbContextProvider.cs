using Roman.Ambinder.Storage.Impl.EntityFrameworkCore.Facilities.Impl;

namespace Roman.Ambinder.Storage.Impl.EntityFrameworkCore.Tests.CompositeKey.Helpers
{
    public class PreCallPeopleDbContextProvider : PerCallDbContextProvider
    {
        public PreCallPeopleDbContextProvider()
            : base(new CallbackDbContextFactory(() => new CompositeKeyPeopleDbContext()))
        {
        }
    }
}