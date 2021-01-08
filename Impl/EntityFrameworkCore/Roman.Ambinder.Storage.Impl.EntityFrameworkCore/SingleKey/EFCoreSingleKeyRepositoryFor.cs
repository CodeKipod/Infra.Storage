using Microsoft.EntityFrameworkCore;
using Roman.Ambinder.Storage.Common.Interfaces;
using Roman.Ambinder.Storage.Common.Interfaces.SingleKey;
using Roman.Ambinder.Storage.Impl.EntityFrameworkCore.Common;
using Roman.Ambinder.Storage.Impl.EntityFrameworkCore.Facilities.Common;
using Roman.Ambinder.Storage.Impl.EntityFrameworkCore.Facilities.Common.Interfaces;
using Roman.Ambinder.Storage.Impl.EntityFrameworkCore.Facilities.Impl;
using Roman.Ambinder.Storage.Impl.EntityFrameworkCore.Facilities.Impl.DbContextLifeCycle.Providers;

namespace Roman.Ambinder.Storage.Impl.EntityFrameworkCore.SingleKey
{
    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    public class EFCoreSingleKeyRepositoryFor<TKey, TEntity> :
        BaseEFCoreRepositoryFor<TKey, TEntity>,
        ISingleKeyRepositoryFor<TKey, TEntity>
        where TEntity : class, new()
    {
        public EFCoreSingleKeyRepositoryFor(IDbContextProvider dbContextProvider,
            IPrimaryKeyExpressionBuilder primaryKeyExpressionBuilder = null,
            IKeyEntityValidatorFor<TKey, TEntity> keyEntityValidator = null)
            : base(dbContextProvider,
                  trackChangesOnRetrievedEntities: false,
                  saveAfterChange: true,
                  primaryKeyExpressionBuilder,
                  keyEntityValidator: keyEntityValidator)
        { }
    }

    public class EFCoreSingleKeyRepositoryFor<TKey, TEntity, TDbContext> :
      EFCoreSingleKeyRepositoryFor<TKey, TEntity>
      where TEntity : class, new()
      where TDbContext : DbContext
    {
        public EFCoreSingleKeyRepositoryFor(TDbContext dBContext)
            : base(new DependencyInjectionDbContextProviderOf<TDbContext>(dBContext),
                  primaryKeyExpressionBuilder: null,
                  keyEntityValidator: null)
        { }
    }
}