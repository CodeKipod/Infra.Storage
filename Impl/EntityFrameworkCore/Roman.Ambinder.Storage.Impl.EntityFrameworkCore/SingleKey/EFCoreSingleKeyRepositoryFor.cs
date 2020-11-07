﻿using Roman.Ambinder.Storage.Common.Interfaces;
using Roman.Ambinder.Storage.Common.Interfaces.SingleKey;
using Roman.Ambinder.Storage.EntityFrameworkCore.Facilities.Common;
using Roman.Ambinder.Storage.Impl.EntityFrameworkCore.Common;
using Roman.Ambinder.Storage.Impl.EntityFrameworkCore.Facilities.Common;

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
                  primaryKeyExpressionBuilder,
                  keyEntityValidator: keyEntityValidator)
        { }
    }
}