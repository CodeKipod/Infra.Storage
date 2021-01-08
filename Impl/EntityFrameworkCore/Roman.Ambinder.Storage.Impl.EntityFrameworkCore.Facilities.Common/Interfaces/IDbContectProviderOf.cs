using Microsoft.EntityFrameworkCore;

namespace Roman.Ambinder.Storage.Impl.EntityFrameworkCore.Facilities.Common.Interfaces
{
    public interface IDbContectProviderOf<TDbContext> : IDbContextProvider
        where TDbContext : DbContext
    { }
}