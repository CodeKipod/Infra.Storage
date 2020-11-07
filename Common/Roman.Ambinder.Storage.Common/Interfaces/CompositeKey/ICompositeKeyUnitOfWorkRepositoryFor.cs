using Roman.Ambinder.Storage.Common.Interfaces.Common;

namespace Roman.Ambinder.Storage.Common.Interfaces
{
    public interface ICompositeKeyUnitOfWorkRepositoryFor<TEntity> :
        IUnitOfWorkRepositoryFor<object[], TEntity>
        where TEntity : class, new()
    {
    }
}