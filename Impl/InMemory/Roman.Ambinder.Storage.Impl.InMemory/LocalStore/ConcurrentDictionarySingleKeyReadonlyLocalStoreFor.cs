//using Roman.Ambinder.DataTypes.OperationResults;
//using Roman.Ambinder.Storage.Common.Interfaces.NotUsed;
//using System;
//using System.Collections.Generic;
//using System.Linq;

//namespace Roman.Ambinder.Storage.Common.Interfaces.LocalStore.Impl
//{
//    public class ConcurrentDictionarySingleKeyReadonlyLocalStoreFor<TKey, TEntity> :
//         BaseConcurrentDictionarySingleKeyLocalStoreFor<TKey, TEntity>,
//         ISingleKeyReadonlyLocalStoreFor<TKey, TEntity>
//        where TEntity : class
//    {
//        protected ConcurrentDictionarySingleKeyReadonlyLocalStoreFor(
//           IKeyProviderOf<TKey, TEntity> keyProvider,
//           IKeyEntityValidatorFor<TKey, TEntity> keyValueValidator)
//            : base(keyProvider, keyValueValidator)
//        { }

//        public OperationResultOf<IReadOnlyCollection<TEntity>> TryGetMany(
//            Predicate<TEntity> filter)
//        {
//            IReadOnlyCollection<TEntity> results = Store.Values
//                .Where(e => filter(e))
//                .ToArray();

//            var success = results.Count > 0;
//            return success ? results.AsSuccessfulOpRes() :
//                $"Failed to {nameof(TryGetMany)}"
//                .AsFailedOpResOf<IReadOnlyCollection<TEntity>>();
//        }

//        public OperationResultOf<TEntity> TryGetSingle(
//            TKey key)
//        {
//            var validateKeyOpRes = KeyEntityValidator.Validate(key);
//            if (!validateKeyOpRes)
//                return validateKeyOpRes.ErrorMessage.AsFailedOpResOf<TEntity>();

//            var success = Store.TryGetValue(key, out var foundEntity);
//            if (success)
//                foundEntity.AsSuccessfulOpRes();

//            return $"Failed to {nameof(TryGetSingle)}(key:{key})"
//                .AsFailedOpResOf<TEntity>();
//        }
//    }
//}
