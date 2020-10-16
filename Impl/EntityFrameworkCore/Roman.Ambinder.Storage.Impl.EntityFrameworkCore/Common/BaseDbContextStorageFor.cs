using Roman.Ambinder.Storage.Common.Interfaces;
using Roman.Ambinder.Storage.CommonImpl;
using Roman.Ambinder.Storage.EntityFrameworkCore.Facilities.Common;
using Roman.Ambinder.Storage.Impl.EntityFrameworkCore.Facilities.Common;
using Roman.Ambinder.Storage.Impl.EntityFrameworkCore.Facilities.Impl;
using System;

namespace Roman.Ambinder.Storage.Impl.EntityFrameworkCore.Common
{
    public abstract class BaseDbContextStorageFor<TKey, TEntity>
        where TEntity : class, new()
    {
        protected readonly IDbContextSafeUsageVisitor DbContextSafeUsageVisitor;
        protected readonly IKeyEntityValidatorFor<TKey, TEntity> KeyEntityValidator;
        protected readonly IPrimaryKeyExpressionBuilderFor<TKey, TEntity> PrimaryKeyExpressionBuilder;

        protected BaseDbContextStorageFor(
            IDbContextSafeUsageVisitor dbContextSafeUsageVisitor,
            IKeyEntityValidatorFor<TKey, TEntity> keyEntityValidator = null,
            IPrimaryKeyExpressionBuilderFor<TKey, TEntity> primaryKeyExpressionBuilder = null)
        {
            DbContextSafeUsageVisitor = dbContextSafeUsageVisitor??
                throw new ArgumentNullException(nameof(dbContextSafeUsageVisitor)); 

            KeyEntityValidator = keyEntityValidator ??
                new VoidKeyEntityValidatorFor<TKey, TEntity>();

            PrimaryKeyExpressionBuilder = primaryKeyExpressionBuilder ??
                new PrimaryKeyExpressionBuilder<TKey, TEntity>();
        }
    }
}
