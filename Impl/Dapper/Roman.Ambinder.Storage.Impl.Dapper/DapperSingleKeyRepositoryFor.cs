using Dapper;
using Dapper.Contrib.Extensions;
using ExpressionExtensionSQL;
using Roman.Ambinder.DataTypes.OperationResults;
using Roman.Ambinder.Storage.Common.DataTypes;
using Roman.Ambinder.Storage.Common.Interfaces.Common;
using System;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Roman.Ambinder.Storage.Impl.Dapper
{
    public class DapperSingleKeyRepositoryFor<TKey, TEntity> :
        IRepositoryFor<TKey, TEntity>
        where TEntity : class, new()
    {
        private readonly IDbConnectionProvider _dbConnectionProvider;
        private readonly string _tableName;

        public DapperSingleKeyRepositoryFor(IDbConnectionProvider dbConnectionProvider,
            string tableName)
        {
            _dbConnectionProvider = dbConnectionProvider;
            _tableName = tableName;
        }

        private async Task<DbConnection> GetOpenedConnectionAsync()
        {
            var connection = _dbConnectionProvider.Get();
            await connection.OpenAsync().ConfigureAwait(false);
            
            return connection;
        }

        public Task<OperationResultOf<TEntity>> TryAddAsync(
            Action<TEntity> initAction = null,
            CancellationToken cancellationToken = default)
        {
            var newEntity = new TEntity();
            initAction?.Invoke(newEntity);

            return TryAddAsync(newEntity, cancellationToken);
        }

        public async Task<OperationResultOf<TEntity>> TryAddAsync(
            TEntity newEntity,
            CancellationToken cancellationToken = default)
        {
            using var connection = await GetOpenedConnectionAsync().ConfigureAwait(false);
            int numberOfChanges = await connection.InsertAsync(newEntity).ConfigureAwait(false);
            var success = numberOfChanges > 0;

            return success ?
                newEntity.AsSuccessfulOpRes() :
                "".AsFailedOpResOf<TEntity>();
        }

        public Task<OperationResultOf<TEntity>> TryAddOrUpdateAsync(
            TKey key,
            Action<TEntity> updateOrInitAction,
            CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<OperationResultOf<PagedItemsResultOf<TEntity>>> TryGetMultipleAsync(
            Expression<Func<TEntity, bool>> filter,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            CancellationToken cancellationToken = default,
            PagingParams pagingParams = null,
            params Expression<Func<TEntity, object>>[] toBeIncluded)
        {
            using var connection = await GetOpenedConnectionAsync().ConfigureAwait(false);
            var where = filter.ToSql();

            var sqlQuery = $@"SELECT * FROM [{_tableName}]
                            {{where}}";
            var result = await connection.QueryAsync<TEntity>(sqlQuery, filter).ConfigureAwait(false);
            return new PagedItemsResultOf<TEntity>(result.ToArray()).AsSuccessfulOpRes();
        }

        public async Task<OperationResultOf<TEntity>> TryGetSingleAsync(
            TKey key,
            CancellationToken cancellation = default,
            params Expression<Func<TEntity, object>>[] toBeIncluded)
        {
            using var connection = await GetOpenedConnectionAsync().ConfigureAwait(false);
            var foundEntity = await connection.GetAsync<TEntity>(key).ConfigureAwait(false);
            return foundEntity != null ? foundEntity.AsSuccessfulOpRes() :
                $"Failed to find {typeof(TEntity).Name} by {key}".AsFailedOpResOf<TEntity>();
        }

        public async Task<OperationResultOf<TEntity>> TryRemoveAsync(
            TKey key,
            CancellationToken cancellationToken = default)
        {
            var getOpRes = await TryGetSingleAsync(key, cancellationToken).ConfigureAwait(false);
            if (!getOpRes) return getOpRes;
            using var connection = await GetOpenedConnectionAsync().ConfigureAwait(false);
            var success = await connection.DeleteAsync<TEntity>(getOpRes.Value).ConfigureAwait(false);

            return success ? getOpRes :
                $"Failed to remove {typeof(TEntity).Name} by {key}".AsFailedOpResOf<TEntity>();
        }

        public async Task<OperationResultOf<TEntity>> TryUpdateAsync(
            TKey key,
            Action<TEntity> updateAction,
            CancellationToken cancellationToken = default)
        {
            var getOpRes = await TryGetSingleAsync(key, cancellationToken).ConfigureAwait(false);
            if (!getOpRes) return getOpRes;
            using var connection = await GetOpenedConnectionAsync().ConfigureAwait(false);

            var existingEntity = getOpRes.Value;
            updateAction(existingEntity);

            var success = await connection.UpdateAsync<TEntity>(existingEntity).ConfigureAwait(false);

            return success ? existingEntity.AsSuccessfulOpRes() :
                 $"Failed to update {typeof(TEntity).Name} with key:{key}".AsFailedOpResOf<TEntity>();
        }
    }
}
