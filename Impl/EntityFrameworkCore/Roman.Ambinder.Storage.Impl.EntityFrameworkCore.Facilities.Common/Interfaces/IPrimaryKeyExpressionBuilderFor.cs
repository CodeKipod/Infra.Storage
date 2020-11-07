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

        //OperationResultOf<Expression<Func<TEntity, bool>>> TryBuildForCompositeKey<TKey, TEntity>(
        //  DbContext dbContext,
        //  in TKey [] compostiteKeyParts)
        // where TEntity : class, new();

        OperationResultOf<Expression<Func<TEntity, bool>>> TryBuildForMultitypeCompositeKey<TEntity>(DbContext dbContext,
              object[] compositeKey)
          where TEntity : class, new();
    }
}