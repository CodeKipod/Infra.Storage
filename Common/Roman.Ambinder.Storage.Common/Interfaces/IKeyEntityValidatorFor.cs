using Roman.Ambinder.DataTypes.OperationResults;

namespace Roman.Ambinder.Storage.Common.Interfaces
{
    public interface IKeyEntityValidatorFor<in TKey, in TValue>
        where TValue : class

    {
        OperationResult Validate(TKey key);

        OperationResult Validate(object[] compositeKey);

        OperationResult Validate(TValue entity);
    }

}