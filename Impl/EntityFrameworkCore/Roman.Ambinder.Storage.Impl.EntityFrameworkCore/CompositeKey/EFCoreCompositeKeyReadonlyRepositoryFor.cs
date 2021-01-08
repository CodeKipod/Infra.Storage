using Roman.Ambinder.Storage.Common.Interfaces;
using Roman.Ambinder.Storage.Common.Interfaces.Common.RepositoryOperations;
using Roman.Ambinder.Storage.Impl.EntityFrameworkCore.Common;
using Roman.Ambinder.Storage.Impl.EntityFrameworkCore.Facilities.Common.Interfaces;

namespace Roman.Ambinder.Storage.Impl.EntityFrameworkCore.CompositeKey
{
    public class EFCoreCompositeKeyReadonlyRepositoryFor<TEntity> :
        BaseEFCoreReadonlyRepositoryFor<object[], TEntity>,
        IRepositoryGetOperationsFor<object[], TEntity>
       where TEntity : class, new()
    {
        public EFCoreCompositeKeyReadonlyRepositoryFor(
            bool trackChangesOnRetrievedEntities,
            IDbContextProvider dbContextProvider = null,
            IPrimaryKeyExpressionBuilder primaryKeyExpressionBuilder = null,
            IKeyEntityValidatorFor<object[], TEntity> keyEntityValidator = null)
            : base(trackChangesOnRetrievedEntities: trackChangesOnRetrievedEntities,
                 dbContextProvider: dbContextProvider,
                 primaryKeyExpressionBuilder: primaryKeyExpressionBuilder,
                 keyEntityValidator: keyEntityValidator)
        { }
    }
}