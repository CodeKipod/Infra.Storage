using Roman.Ambinder.DataTypes.OperationResults;

namespace Roman.Ambinder.Storage.Common.Interfaces
{
    public interface IKeyProviderOf<TKey, in TValue>
    {
        OperationResultOf<TKey> TryGetKey(TValue value);
    }
}