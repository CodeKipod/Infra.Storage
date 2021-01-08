using Roman.Ambinder.Storage.Common.Interfaces.Common;

namespace Roman.Ambinder.Storage.Common.Interfaces.SingleKey
{
    public interface ISingleKeyUnitOfWorkRepositoryFor<TKey, TEntity> :
        IUnitOfWorkRepositoryFor<TKey, TEntity>
        where TEntity : class, new()
    {
    }
}