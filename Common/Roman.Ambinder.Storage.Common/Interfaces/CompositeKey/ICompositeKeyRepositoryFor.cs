using Roman.Ambinder.Storage.Common.Interfaces.Common;

namespace Roman.Ambinder.Storage.Common.Interfaces.CompositeKey
{
    public interface ICompositeKeyRepositoryFor<TEntity> :
      IRepositoryFor<object[], TEntity>
        where TEntity : class, new()
    { }
}