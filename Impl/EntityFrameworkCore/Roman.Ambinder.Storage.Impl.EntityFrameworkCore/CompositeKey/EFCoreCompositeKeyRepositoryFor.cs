﻿using Microsoft.EntityFrameworkCore;
using Roman.Ambinder.Storage.Common.Interfaces;
using Roman.Ambinder.Storage.Common.Interfaces.CompositeKey;
using Roman.Ambinder.Storage.Impl.EntityFrameworkCore.Common;
using Roman.Ambinder.Storage.Impl.EntityFrameworkCore.Facilities.Common.Interfaces;
using Roman.Ambinder.Storage.Impl.EntityFrameworkCore.Facilities.Impl.DbContextLifeCycle.Providers;

namespace Roman.Ambinder.Storage.Impl.EntityFrameworkCore.CompositeKey
{
    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    public class EFCoreCompositeKeyRepositoryFor<TEntity> :
        BaseEFCoreRepositoryFor<object[], TEntity>,
        ICompositeKeyRepositoryFor<TEntity>
        where TEntity : class, new()
    {
        public EFCoreCompositeKeyRepositoryFor(IDbContextProvider dbContextProvider,
            IPrimaryKeyExpressionBuilder primaryKeyExpressionBuilder = null,
            IKeyEntityValidatorFor<object[], TEntity> keyEntityValidator = null)
            : base(dbContextProvider,
                  trackChangesOnRetrievedEntities: false,
                  saveAfterChange: true,
                  primaryKeyExpressionBuilder,
                  keyEntityValidator: keyEntityValidator)
        { }
    }

    public class EFCoreCompositeKeyRepositoryFor<TEntity, TDbContext> :
       EFCoreCompositeKeyRepositoryFor<TEntity>,
       ICompositeKeyRepositoryFor<TEntity>
       where TEntity : class, new()
       where TDbContext : DbContext
    {
        public EFCoreCompositeKeyRepositoryFor(TDbContext dbContext)
            : base(new DependencyInjectionDbContextProviderOf<TDbContext>(dbContext),
                  primaryKeyExpressionBuilder: null,
                  keyEntityValidator: null)
        { }
    }
}