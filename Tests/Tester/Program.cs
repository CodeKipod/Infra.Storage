using System;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace Tester
{
    class Program
    {
        static void Main(string[] args)
        {
            //var filter = Build<int, MyEntity>(2);


        }

        /*
         *  public Expression<Func<TEntity, bool>> Build(
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
         *
         */

        //public static Expression<Func<TEntity, DbContext, bool>> Build<TKey, TEntity>(in TKey key)
        //{
        //    Expression<Func<DbContext, string>> getKeyNameLambda = (dbContext) =>
        //       dbContext.Model.FindEntityType(typeof(TEntity))
        //           .FindPrimaryKey().Properties
        //           .Select(x => x.Name)
        //           .Single();

        //    var contextParameter = Expression.Parameter(typeof(DbContext), "context");
        //    InvocationExpression callInner = Expression.Invoke(
        //        Expression.Constant(getKeyNameLambda),
        //        contextParameter);

        //    var item = Expression.Parameter(typeof(TEntity), "entity");
        //    var property = Expression.Property(item, callInner.c);
        //    var value = Expression.Constant(key);
        //    var equals = Expression.Equal(property, value);
        //    var filter = Expression.Lambda<Func<TEntity, DbContext, bool>>(equals, item);

        //    return filter;
        //}
    }

    public class MyEntity
    {
        public int Id { get; set; }
    }
}
