using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;
using Roman.Ambinder.Storage.Impl.EntityFrameworkCore.Facilities.Common.Interfaces;

namespace Roman.Ambinder.Storage.Impl.EntityFrameworkCore.Facilities.Impl
{
    public sealed class PrimaryKeyExpressionBuilder<TKey, TEntity> :
        IPrimaryKeyExpressionBuilderFor<TKey, TEntity>
        where TEntity : class, new()
    {
        public Expression<Func<TEntity, bool>> Build(
            DbContext dbContext,
            in TKey id)
        {
            var propertyName = dbContext
                .Model.FindEntityType(typeof(TEntity))
                .FindPrimaryKey().Properties
                .Select(x => x.Name)
                .Single();

            var item = Expression.Parameter(typeof(TEntity), "entity");
            var property = Expression.Property(item, propertyName);
            var value = Expression.Constant(id);
            var equals = Expression.Equal(property, value);
            var filter = Expression.Lambda<Func<TEntity, bool>>(equals, item);

            return filter;
        }
    }
}