using Microsoft.EntityFrameworkCore;
using Roman.Ambinder.Storage.Common.DataTypes;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace Roman.Ambinder.Storage.Impl.EntityFrameworkCore.Facilities.Common
{
    public static class QueryableExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IQueryable<TEntity> AppendIncludeExpressions<TEntity>(
          this IQueryable<TEntity> query,
          Expression<Func<TEntity, object>>[] toBeIncluded)
          where TEntity : class
        {
            if (toBeIncluded != null && toBeIncluded.Length > 0)
            {
                query = toBeIncluded.Aggregate(query,
                    (current, includeExpression) => current.Include(includeExpression));
            }

            return query;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async Task<PagedItemsResultOf<T>> ToPagedResultsArrayAsync<T>(
            this IQueryable<T> query,
            PagingParams pagingParams,
            CancellationToken cancellationToken = default)
        {
            PagedItemsResultOf<T> pagedResults;

            if (pagingParams == null)
            {
                var items = await query
                   .ToArrayAsync(cancellationToken)
                   .ConfigureAwait(false);

                pagedResults = new PagedItemsResultOf<T>(items);
            }
            else
            {
                var itemsToSkip = (pagingParams.CurrentPage - 1) * pagingParams.ItemsPerPage;
                //var countTask = query.CountAsync(cancellationToken);
                //var getPagedResultTask = query.Skip(itemsToSkip)
                //      .Take(pagingParams.ItemsPerPage)
                //      .ToArrayAsync(cancellationToken);

                //await Task.WhenAll(countTask, getPagedResultTask)
                //    .ConfigureAwait(false);

                var totalNumberOfItems = await query.CountAsync(cancellationToken);

                pagedResults = new PagedItemsResultOf<T>(
                    pagingParams.CurrentPage,
                    pagingParams.ItemsPerPage,
                    totalNumberOfItems);

                pagedResults.Items = await query.Skip(itemsToSkip)
                      .Take(pagingParams.ItemsPerPage)
                      .ToArrayAsync(cancellationToken);
            }

            return pagedResults;
        }
    }
}