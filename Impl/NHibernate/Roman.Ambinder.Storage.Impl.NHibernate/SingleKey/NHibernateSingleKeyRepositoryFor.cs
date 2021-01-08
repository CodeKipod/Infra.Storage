using NHibernate;
using Roman.Ambinder.Storage.Common.Interfaces;
using Roman.Ambinder.Storage.Impl.NHibernate.Common;

namespace Roman.Ambinder.Storage.Impl.NHibernate.SingleKey
{
    public class NHibernateSingleKeyRepositoryFor<TKey, TEntity> :
        NHibernateRepositoryFor<TKey, TEntity>
        where TEntity : class, new()
    {
        public NHibernateSingleKeyRepositoryFor(
          IStoreSessionSafeUsageVisitor<ISession> storeSessionSafeUsageVisitor,
          IKeyEntityValidatorFor<TKey, TEntity> keyEntityValidator)
          : base(storeSessionSafeUsageVisitor, keyEntityValidator)
        {

        }
    }
}
