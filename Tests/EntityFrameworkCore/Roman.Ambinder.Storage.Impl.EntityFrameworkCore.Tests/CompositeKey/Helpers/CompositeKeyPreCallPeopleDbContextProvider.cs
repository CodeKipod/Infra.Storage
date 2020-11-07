using Roman.Ambinder.Storage.Impl.EntityFrameworkCore.Facilities.Impl;

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