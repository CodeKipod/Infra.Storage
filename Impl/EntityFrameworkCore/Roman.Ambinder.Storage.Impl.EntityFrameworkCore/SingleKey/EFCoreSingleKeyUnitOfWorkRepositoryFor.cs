using Microsoft.EntityFrameworkCore;
using Roman.Ambinder.Storage.Impl.EntityFrameworkCore.Common;
using Roman.Ambinder.Storage.Impl.EntityFrameworkCore.Facilities.Common.Interfaces;
using Roman.Ambinder.Storage.Impl.EntityFrameworkCore.Facilities.Impl.DbContextLifeCycle.Providers;

namespace Roman.Ambinder.Storage.Impl.EntityFrameworkCore.SingleKey
{
    public class EFCoreSingleKeyUnitOfWorkRepositoryFor<TKey, TEntity> :
         BaseEFCoreUnitOfWorkRepositoryFor<TKey, TEntity>
          where TEntity : class, new()
    {
        public EFCoreSingleKeyUnitOfWorkRepositoryFor(
            IDbContextProvider dbContextProvider)
            : base(dbContextProvider)
        { }
    }

    public class EFCoreSingleKeyUnitOfWorkRepositoryFor<TKey, TEntity, TDbContext> :
        EFCoreSingleKeyUnitOfWorkRepositoryFor<TKey, TEntity>
         where TEntity : class, new()
        where TDbContext : DbContext
    {
        public EFCoreSingleKeyUnitOfWorkRepositoryFor(
            TDbContext dbContext)
            : base(new DependencyInjectionDbContextProviderOf<TDbContext>(dbContext))
        { }
    }
}