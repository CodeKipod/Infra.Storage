using Roman.Ambinder.Storage.Common.Interfaces.NotUsed;
using System.Collections.Concurrent;

namespace Roman.Ambinder.Storage.Common.Interfaces.LocalStore.Impl
{
    public abstract class BaseConcurrentDictionarySingleKeyLocalStoreFor<TKey, TEntity>
        where TEntity : class
    {
        protected readonly ConcurrentDictionary<TKey, TEntity> Store =
         new ConcurrentDictionary<TKey, TEntity>();

        protected readonly IKeyProviderOf<TKey, TEntity> KeyProvider;
        protected readonly IKeyEntityValidatorFor<TKey, TEntity> KeyEntityValidator;

        protected BaseConcurrentDictionarySingleKeyLocalStoreFor(            
            IKeyProviderOf<TKey, TEntity> keyProvider,
            IKeyEntityValidatorFor<TKey, TEntity> keyEntityValidator)
        {
            KeyProvider = keyProvider;
            KeyEntityValidator = keyEntityValidator;

        }
    }
}
