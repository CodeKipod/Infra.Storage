using Microsoft.EntityFrameworkCore;
using Roman.Ambinder.DataTypes.OperationResults;
using System;
using System.Linq.Expressions;

namespace Roman.Ambinder.Storage.EntityFrameworkCore.Facilities.Common
{
    public interface IPrimaryKeyExpressionBuilder
    {
        OperationResultOf<Expression<Func<TEntity, bool>>> TryBuildForSingleKey<TKey, TEntity>(
            DbContext dbContext,
            in TKey key)
           where TEntity : class, new();

        OperationResultOf<Expression<Func<TEntity, bool>>> TryBuildForCompositeKey<TEntity>(DbContext dbContext,
            in object[] compostiteKeyParts)
          where TEntity : class, new();
    }
}