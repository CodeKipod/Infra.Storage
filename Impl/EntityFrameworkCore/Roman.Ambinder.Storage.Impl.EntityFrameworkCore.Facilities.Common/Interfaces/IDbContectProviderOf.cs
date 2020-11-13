using Microsoft.EntityFrameworkCore;

namespace Roman.Ambinder.Storage.Impl.EntityFrameworkCore.Facilities.Common
{
    public interface IDbContectProviderOf<TDbContext> : IDbContextProvider
        where TDbContext : DbContext
    { }
}