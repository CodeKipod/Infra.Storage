using Roman.Ambinder.DataTypes.OperationResults;
using Roman.Ambinder.Storage.Common.Interfaces;

namespace Roman.Ambinder.Storage.CommonImpl
{
    public class VoidKeyNotNullEntityValidatorFor<TKey, TEntity> : IKeyEntityValidatorFor<TKey, TEntity>
        where TEntity : class
    {
        public OperationResult Validate(TKey key) => OperationResult.Successful;

        public OperationResult Validate(TEntity entity)
        {
            return entity!=null ? OperationResult.Successful : 
                new OperationResult("Entity cannot be null");
        }
    }
}