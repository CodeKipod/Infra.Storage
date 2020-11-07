using Roman.Ambinder.Storage.Common.Interfaces.Common.UnitOfWork;

namespace Roman.Ambinder.Storage.Common.Interfaces.UnitOfWork
{
    public interface ISingleKeyUnitOfWorkRepositoryFor<TKey, TEntity> :
        IUnitOfWorkRepositoryFor<TKey, TEntity>
        where TEntity : class, new()
    {
    }
}