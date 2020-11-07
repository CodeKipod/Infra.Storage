using Roman.Ambinder.Storage.Common.Interfaces.Common;

namespace Roman.Ambinder.Storage.Common.Interfaces.SingleKey
{
    public interface ISingleKeyRepositoryFor<TKey, TEntity> :
       IRepositoryFor<TKey, TEntity>
        where TEntity : class, new()
    {

    }
}