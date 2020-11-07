using Roman.Ambinder.Storage.Common.Interfaces.Common.UnitOfWork;

namespace Roman.Ambinder.Storage.Common.Interfaces.UnitOfWork
{
    public interface ICompositeKeyUnitOfWorkRepositoryFor<TEntity> :
        IUnitOfWorkRepositoryFor<object[], TEntity>
        where TEntity : class, new()
    {
    }
}