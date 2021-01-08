using Roman.Ambinder.DataTypes.OperationResults;
using System;
using System.Linq.Expressions;

namespace Roman.Ambinder.Storage.Common.Interfaces
{

    public interface IPrimaryKeyExpressionBuilderFor<in TStoreSession>
    {
        OperationResultOf<Expression<Func<TEntity, bool>>> TryBuildForSingleKey<TKey, TEntity>(
            TStoreSession dbContext,
            in TKey key)
           where TEntity : class, new();


        OperationResultOf<Expression<Func<TEntity, bool>>> TryBuildForMultitypeCompositeKey<TEntity>(
            TStoreSession dbContext,
              object[] compositeKey)
          where TEntity : class, new();
    }
}
