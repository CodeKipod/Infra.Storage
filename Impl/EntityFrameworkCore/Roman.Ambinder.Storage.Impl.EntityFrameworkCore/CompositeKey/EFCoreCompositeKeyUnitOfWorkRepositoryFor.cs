using Microsoft.EntityFrameworkCore;
using Roman.Ambinder.Storage.Impl.EntityFrameworkCore.Common;
using Roman.Ambinder.Storage.Impl.EntityFrameworkCore.Facilities.Common.Interfaces;
using Roman.Ambinder.Storage.Impl.EntityFrameworkCore.Facilities.Impl.DbContextLifeCycle.Providers;

namespace Roman.Ambinder.Storage.Impl.EntityFrameworkCore.CompositeKey
{
    public class EFCoreCompositeKeyUnitOfWorkRepositoryFor<TEntity> :
         BaseEFCoreUnitOfWorkRepositoryFor<object[], TEntity>
          where TEntity : class, new()
    {
        public EFCoreCompositeKeyUnitOfWorkRepositoryFor(
            IDbContextProvider dbContextProvider)
            : base(dbContextProvider)
        { }
    }

    public class EFCoreCompositeKeyUnitOfWorkRepositoryFor<TEntity, TDbContext> :
        BaseEFCoreUnitOfWorkRepositoryFor<object[], TEntity>
         where TEntity : class, new()
        where TDbContext : DbContext
    {
        public EFCoreCompositeKeyUnitOfWorkRepositoryFor(
            TDbContext dbContext)
            : base(new DependencyInjectionDbContextProviderOf<TDbContext>(dbContext))
        { }
    }
}