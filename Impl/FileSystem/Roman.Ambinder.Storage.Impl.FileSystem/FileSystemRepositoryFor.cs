//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq.Expressions;
//using System.Threading.Tasks;
//using Roman.Ambinder.DataTypes.OperationResults;
//using Roman.Ambinder.Storage.Common.Interfaces;

//namespace Roman.Ambinder.Storage.FileSystem
//{
//    public class FileSystemQueryRepositoryFor<TKey, TEntity>
//        : IQueryRepositoryFor<TKey, TEntity>
//    {

//        public Task<OperationResultOf<bool>> TryCheckExistsAsync(TKey key)
//        {
//            string filePath = key.ToString();
//            return Task.FromResult(File.Exists(filePath).AsSuccessfulOpRes());
//        }

//        public Task<OperationResultOf<TEntity>> TryGetAsync(TKey key)
//        {
//            throw new NotImplementedException();
//        }

//        public Task<OperationResultOf<IReadOnlyCollection<TEntity>>> TryGetMultipleAsync(
//            Expression<Func<TEntity, bool>> filter)
//        {
//            throw new NotImplementedException();
//        }

//        public Task<OperationResultOf<bool>> TryCheckExistsAsync(
//            Expression<Func<TEntity, bool>> filter)
//        {
//            throw new NotImplementedException();
//        }
//    }
//}
