using Roman.Ambinder.Storage.Impl.EntityFrameworkCore.Facilities.Common;
using Roman.Ambinder.Storage.Impl.EntityFrameworkCore.SingleKey.UnitOfWork;

namespace Roman.Ambinder.Storage.Impl.EntityFrameworkCore.SingleKey
{
    public class EFCoreSingleKeyUnitOfWorkRepositoryFor<TKey, TEntity> :
         BaseEFCoreUnitOfWorkRepositoryFor<TKey, TEntity>
          where TEntity : class, new()
    {
        public EFCoreSingleKeyUnitOfWorkRepositoryFor(
            IDbContextProvider dbContextProvider)
            : base(dbContextProvider)
        { }
    }
}
