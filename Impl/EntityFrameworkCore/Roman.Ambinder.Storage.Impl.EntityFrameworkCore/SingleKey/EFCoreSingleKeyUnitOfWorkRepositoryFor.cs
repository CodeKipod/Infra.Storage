using Roman.Ambinder.Storage.Impl.EntityFrameworkCore.Common;
using Roman.Ambinder.Storage.Impl.EntityFrameworkCore.Facilities.Common;

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