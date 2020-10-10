using Microsoft.EntityFrameworkCore;
using System;
using System.Linq.Expressions;

namespace Roman.Ambinder.Storage.EntityFrameworkCore.Facilities.Common
{
    public interface IPrimaryKeyExpressionBuilderFor<TKey, TEntity>
    {
        Expression<Func<TEntity, bool>> Build(DbContext dbContext, in TKey key);
    }
}