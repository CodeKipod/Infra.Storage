using Roman.Ambinder.Storage.Impl.EntityFrameworkCore.Facilities.Common;
using Roman.Ambinder.Storage.Impl.EntityFrameworkCore.SingleKey.UnitOfWork;

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