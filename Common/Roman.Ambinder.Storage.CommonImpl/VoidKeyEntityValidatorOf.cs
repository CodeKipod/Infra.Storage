using Roman.Ambinder.DataTypes.OperationResults;
using Roman.Ambinder.Storage.Common.Interfaces;

namespace Roman.Ambinder.Storage.CommonImpl
{
    public class VoidKeyEntityValidatorOf<TKey, TEntity> : IKeyEntityValidatorFor<TKey, TEntity>
        where TEntity : class
    {
        public OperationResult Validate(TKey key) => OperationResult.Successful;

        public OperationResult Validate(TEntity entity) => OperationResult.Successful;
    }
}