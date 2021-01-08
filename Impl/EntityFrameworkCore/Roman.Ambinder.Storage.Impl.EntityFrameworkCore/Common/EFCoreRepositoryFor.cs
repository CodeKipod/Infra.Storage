using Roman.Ambinder.Storage.Common.Interfaces;
using Roman.Ambinder.Storage.Impl.EntityFrameworkCore.Facilities.Common.Interfaces;

namespace Roman.Ambinder.Storage.Impl.EntityFrameworkCore.Common
{
    public class EFCoreRepositoryFor<TKey, TEntity> :
        BaseEFCoreRepositoryFor<TKey, TEntity>
         where TEntity : class, new()
    {
        public EFCoreRepositoryFor(
            IDbContextProvider dbContextProvider,
            IKeyEntityValidatorFor<TKey, TEntity> keyEntityValidator = null)
            : base(dbContextProvider,
                  trackChangesOnRetrievedEntities: true,
                  saveAfterChange: false,
                  keyEntityValidator: keyEntityValidator)
        { }
    }
}