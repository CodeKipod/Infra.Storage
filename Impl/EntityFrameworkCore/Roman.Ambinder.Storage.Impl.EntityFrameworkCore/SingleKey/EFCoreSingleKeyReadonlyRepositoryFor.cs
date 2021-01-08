using Roman.Ambinder.Storage.Common.Interfaces;
using Roman.Ambinder.Storage.Common.Interfaces.Common.RepositoryOperations;
using Roman.Ambinder.Storage.Impl.EntityFrameworkCore.Common;
using Roman.Ambinder.Storage.Impl.EntityFrameworkCore.Facilities.Common.Interfaces;

namespace Roman.Ambinder.Storage.Impl.EntityFrameworkCore.SingleKey
{
    public class EFCoreSingleKeyReadonlyRepositoryFor<TKey, TEntity> :
        BaseEFCoreReadonlyRepositoryFor<TKey, TEntity>,
        IRepositoryGetOperationsFor<TKey, TEntity>
       where TEntity : class, new()
    {

        public EFCoreSingleKeyReadonlyRepositoryFor(
            bool trackChangesOnRetrievedEntities,
            IDbContextProvider dbContextProvider = null,
            IPrimaryKeyExpressionBuilder primaryKeyExpressionBuilder = null,
            IKeyEntityValidatorFor<TKey, TEntity> keyEntityValidator = null)
            : base(trackChangesOnRetrievedEntities: trackChangesOnRetrievedEntities,
                  dbContextProvider,
                  primaryKeyExpressionBuilder,
                  keyEntityValidator)
        {
        }
    }
}