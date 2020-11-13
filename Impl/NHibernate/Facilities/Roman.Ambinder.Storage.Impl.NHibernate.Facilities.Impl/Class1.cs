//using NHibernate;
//using Roman.Ambinder.DataTypes.OperationResults;
//using Roman.Ambinder.Storage.Impl.NHibernate.Facilities.Common;
//using System;
//using System.Linq.Expressions;

//namespace Roman.Ambinder.Storage.Impl.NHibernate.Facilities.Impl
//{
//    public sealed class PrimaryKeyExpressionBuilder : 
//        IPrimaryKeyExpressionBuilder
//    {
//        public OperationResultOf<Expression<Func<TEntity, bool>>> TryBuildForSingleKey<TKey, TEntity>(
//            ISession dbContext,
//            in TKey key)
//              where TEntity : class, new()
//        {
//            if (key is object[] keys)
//            {
//                return TryBuildForMultitypeCompositeKey<TEntity>(dbContext, keys);
//            }

//            try
//            {
//                var propertyName = dbContext
//                    .Model.FindEntityType(typeof(TEntity))
//                    .FindPrimaryKey().Properties
//                    .Select(x => x.Name)
//                    .Single();

//                var item = Expression.Parameter(typeof(TEntity), "entity");
//                var property = Expression.Property(item, propertyName);
//                var value = Expression.Constant(key);
//                var equals = Expression.Equal(property, value);
//                var filter = Expression.Lambda<Func<TEntity, bool>>(equals, item);

//                return filter.AsSuccessfulOpRes();
//            }
//            catch (Exception ex) { return ex.AsFailedOpResOf<Expression<Func<TEntity, bool>>>(); }
//        }

//        public OperationResultOf<Expression<Func<TEntity, bool>>> TryBuildForMultitypeCompositeKey<TEntity>(
//            ISession dbContext,
//            object[] compostiteKeyParts)
//            where TEntity : class, new()
//        {
//            try
//            {
//                var keyPropertieNames = dbContext
//                    .Connection.Container..FindEntityType(typeof(TEntity))
//                    .FindPrimaryKey().Properties
//                    .Select(x => x.Name)
//                    .ToArray();

//                if (compostiteKeyParts.Length != keyPropertieNames.Length)
//                    return $"Invalid number of keys.\nProvided {compostiteKeyParts.Length} keys, when {typeof(TEntity).Name} actually has a composed of {keyPropertieNames.Length} keys"
//                         .AsFailedOpResOf<Expression<Func<TEntity, bool>>>();

//                var parameter = Expression.Parameter(typeof(TEntity), "entity");

//                BinaryExpression combinedEqualityExpression = null;
//                for (int i = 0; i < compostiteKeyParts.Length; i++)
//                {
//                    var property = Expression.Property(parameter, keyPropertieNames[i]);
//                    var value = Expression.Constant(compostiteKeyParts[i]);
//                    var currentEqualityExpression = Expression.Equal(property, value);

//                    combinedEqualityExpression = combinedEqualityExpression != null ?
//                        Expression.AndAlso(combinedEqualityExpression, currentEqualityExpression) :
//                        currentEqualityExpression;
//                }

//                var filter = Expression.Lambda<Func<TEntity, bool>>(combinedEqualityExpression, parameter);

//                return filter.AsSuccessfulOpRes();
//            }
//            catch (Exception ex) { return ex.AsFailedOpResOf<Expression<Func<TEntity, bool>>>(); }
//        }
//    }
//}
//}
