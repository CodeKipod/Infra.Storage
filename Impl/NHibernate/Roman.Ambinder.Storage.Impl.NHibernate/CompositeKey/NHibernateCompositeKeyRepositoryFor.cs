using NHibernate;
using Roman.Ambinder.Storage.Common.Interfaces;
using Roman.Ambinder.Storage.Impl.NHibernate.Common;

namespace Roman.Ambinder.Storage.Impl.NHibernate.CompositeKey
{
    public class NHibernateCompositeKeyRepositoryFor<TEntity> :
        NHibernateRepositoryFor<object[], TEntity>
        where TEntity : class, new()
    {
        public NHibernateCompositeKeyRepositoryFor(
          IStoreSessionSafeUsageVisitor<ISession> storeSessionSafeUsageVisitor,
          IKeyEntityValidatorFor<object[], TEntity> keyEntityValidator)
          : base(storeSessionSafeUsageVisitor, keyEntityValidator)
        {
        }
    }
}
