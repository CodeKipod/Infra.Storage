//using Roman.Ambinder.DataTypes.OperationResults;
//using Roman.Ambinder.Storage.Common.Interfaces.LocalChangesStore;
//using Roman.Ambinder.Storage.Common.Interfaces.NotUsed;
//using System;

//namespace Roman.Ambinder.Storage.Common.Interfaces.LocalStore.Impl
//{
//    public class ConcurrentDictionarySingleKeyLocalStoreFor<TKey, TEntity> :
//        ConcurrentDictionarySingleKeyReadonlyLocalStoreFor<TKey, TEntity>,
//        ISingleKeyLocalChangesStoreFor<TKey, TEntity>
//         where TEntity : class
//    {
//        public ConcurrentDictionarySingleKeyLocalStoreFor(
//            IKeyProviderOf<TKey, TEntity> keyProvider,
//            IKeyEntityValidatorFor<TKey, TEntity> keyEntityValidator)
//            : base(keyProvider, keyEntityValidator)
//        { }

//        public OperationResult TryAdd(TEntity newEntity)
//        {
//            var validateEntityOpRes = KeyEntityValidator.Validate(newEntity);
//            if (!validateEntityOpRes) return validateEntityOpRes;

//            var getKeyOpRes = KeyProvider.TryGetKey(newEntity);
//            if (!getKeyOpRes) return getKeyOpRes;

//            var key = getKeyOpRes.Value;
//            if (Store.TryAdd(key, newEntity))
//                return OperationResult.Successful;

//            return $"Failed to {nameof(TryAdd)} because {key} already exits"
//                .AsFailedOpRes();
//        }

//        public OperationResult TryRemove(TKey key)
//        {
//            var validateKeyOpRes = KeyEntityValidator.Validate(key);
//            if (!validateKeyOpRes) return validateKeyOpRes;

//            if (Store.TryRemove(key, out _))
//                return OperationResult.Successful;

//            return $"Failed to {nameof(TryRemove)} becasue did not key:{key}"
//                .AsFailedOpRes();
//        }

//        public OperationResult TryUpdate(TKey key, Action<TEntity> updateAction)
//        {
//            var validateKeyOpRes = KeyEntityValidator.Validate(key);
//            if (!validateKeyOpRes) return validateKeyOpRes;

//            var getOpRes = TryGetSingle(key);
//            if (!getOpRes)
//                return getOpRes;

//            var existingValue = getOpRes.Value;
//            try
//            {
//                updateAction(existingValue);
//                return OperationResult.Successful;
//            }
//            catch (Exception ex)
//            {
//               return ex.AsFailedOpRes();
//            }
//        }
//    }
//}
