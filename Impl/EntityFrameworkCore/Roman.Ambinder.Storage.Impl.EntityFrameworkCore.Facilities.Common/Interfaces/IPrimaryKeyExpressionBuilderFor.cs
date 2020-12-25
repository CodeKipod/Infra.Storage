using System;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace Roman.Ambinder.Storage.Impl.EntityFrameworkCore.Facilities.Common.Interfaces
{
    public interface IPrimaryKeyExpressionBuilderFor<TKey, TEntity>
    {
        Expression<Func<TEntity, bool>> Build(DbContext dbContext, in TKey key);
    }
}