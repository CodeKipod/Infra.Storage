using Roman.Ambinder.Storage.Impl.EntityFrameworkCore.Common;
using Roman.Ambinder.Storage.Impl.EntityFrameworkCore.Facilities.Common;

namespace Roman.Ambinder.Storage.Impl.EntityFrameworkCore.CompositeKey
{
    public class EFCoreCompositeKeyUnitOfWorkRepositoryFor<TEntity> :
         BaseEFCoreUnitOfWorkRepositoryFor<object[], TEntity>
          where TEntity : class, new()
    {
        public EFCoreCompositeKeyUnitOfWorkRepositoryFor(
            IDbContextProvider dbContextProvider)
            : base(dbContextProvider)
        { }
    }
}